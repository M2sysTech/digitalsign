namespace Veros.Paperless.Model.Servicos.Exportacao
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Entidades;
    using Framework;
    using Framework.IO;
    using Framework.Threads;
    using Repositorios;

    public class EnviaPdfsDoLoteParaCloud : IEnviaPdfsDoLoteParaCloud
    {
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IPostaArquivoAmazonS3 postaArquivoAmazonS3;
        private readonly IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico;
        private readonly ILoteRepositorio loteRepositorio;
        private readonly IGravaLogDaPaginaServico gravaLogDaPaginaServico;
        private readonly IFileSystem fileSystem;
        private readonly IUnitOfWork unitOfWork;

        public EnviaPdfsDoLoteParaCloud(
            IPaginaRepositorio paginaRepositorio, 
            IPostaArquivoAmazonS3 postaArquivoAmazonS3, 
            IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico, 
            ILoteRepositorio loteRepositorio, 
            IGravaLogDaPaginaServico gravaLogDaPaginaServico, 
            IFileSystem fileSystem, 
            IUnitOfWork unitOfWork)
        {
            this.paginaRepositorio = paginaRepositorio;
            this.postaArquivoAmazonS3 = postaArquivoAmazonS3;
            this.baixaArquivoFileTransferServico = baixaArquivoFileTransferServico;
            this.loteRepositorio = loteRepositorio;
            this.gravaLogDaPaginaServico = gravaLogDaPaginaServico;
            this.fileSystem = fileSystem;
            this.unitOfWork = unitOfWork;
        }

        public void Executar(int loteId)
        {
            var paginas = this.unitOfWork.Obter(() => this.paginaRepositorio.ObterPdfsDoLote(loteId));
            var paginasNaoEnviadas = paginas.Where(x => x.CloudOk == false);

            Log.Application.InfoFormat("Lote {0} tem {1} paginas para envio", loteId, paginasNaoEnviadas.Count());
            
            var inicioLote = DateTime.Now;
            var arquivosBaixados = new Dictionary<int, string>();

            Parallel.ForEach(paginasNaoEnviadas, Paralelizar.Em(Aplicacao.Nucleos), pagina =>
            {
                var arquivoLocal = string.Empty;

                try
                {
                    arquivoLocal = this.unitOfWork.Obter(() => this.baixaArquivoFileTransferServico
                        .BaixarArquivo(pagina.Id, pagina.TipoArquivo, dataCenterId: pagina.DataCenter));
                }
                catch (FileNotFoundException)
                {
                    arquivoLocal = @"c:\naoexiste.ddd";
                }
                finally
                {
                    arquivosBaixados.Add(pagina.Id, arquivoLocal);
                }
            });

            int i = 0;
            foreach (var pagina in paginasNaoEnviadas)
            {
                this.unitOfWork.Transacionar(() =>
                {
                    i++;
                    var inicio = DateTime.Now;

                    var arquivoLocal = arquivosBaixados[pagina.Id];

                    if (this.fileSystem.Exists(arquivoLocal) == false)
                    {
                        Log.Application.Error("Não foi possível baixar pagina " + pagina.Id + " do filetransfer. Não existe no fileTransfer " + pagina.DataCenter);

                        this.gravaLogDaPaginaServico.Executar(
                            LogPagina.AcaoPostadoNoCloud,
                            pagina.Id,
                            pagina.Documento.Id,
                            "Não Postado no Cloud. Não existia pdf no filetransfer");

                        this.paginaRepositorio.MarcarComoEnviadaCloud(pagina.Id);

                        var fim = DateTime.Now;
                        var tempo = fim.Subtract(inicio);

                        Log.Application.InfoFormat("Processamento desta pagina {0} com erro em {1} ms", pagina.Id, tempo.TotalMilliseconds);
                    }

                    if (this.fileSystem.Exists(arquivoLocal))
                    {
                        Log.Application.InfoFormat("Enviando Pagina {0}", pagina.Id);

                        this.postaArquivoAmazonS3.PostarPagina(pagina, arquivoLocal);
                        this.paginaRepositorio.MarcarComoEnviadaCloud(pagina.Id);

                        var fim = DateTime.Now;
                        var tempo = fim.Subtract(inicio);

                        Log.Application.InfoFormat("Pagina {0} enviada em {1} ms", pagina.Id, tempo.TotalMilliseconds);

                        this.gravaLogDaPaginaServico.Executar(
                            LogPagina.AcaoPostadoNoCloud,
                            pagina.Id,
                            pagina.Documento.Id,
                            "Pdf Postado no Cloud. Levou " + tempo.TotalMilliseconds + "ms");

                        Log.Application.DebugFormat("Pagina {0} Enviada", i);
                    }
                });
            }

            var fimLote = DateTime.Now;
            var tempoLote = fimLote.Subtract(inicioLote);

            Log.Application.InfoFormat("Lote {0} marcado como enviado. Feito em {1}s", loteId, tempoLote.TotalSeconds);
            this.unitOfWork.Transacionar(() => this.loteRepositorio.MarcarComoEnviadoParaCloud(loteId));

            foreach (var arquivosBaixado in arquivosBaixados)
            {
                Log.Application.Info("Apagando do cache . " + arquivosBaixado.Value);
                this.fileSystem.DeleteFile(arquivosBaixado.Value);
            }
        }
    }
}