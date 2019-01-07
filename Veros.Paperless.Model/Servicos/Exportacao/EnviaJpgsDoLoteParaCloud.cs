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

    public class EnviaJpgsDoLoteParaCloud : IEnviaJpgsDoLoteParaCloud
    {
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IPostaArquivoAmazonS3 postaArquivoAmazonS3;
        private readonly IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico;
        private readonly ILoteRepositorio loteRepositorio;
        private readonly IGravaLogDaPaginaServico gravaLogDaPaginaServico;
        private readonly IFileSystem fileSystem;
        private readonly IUnitOfWork unitOfWork;

        public EnviaJpgsDoLoteParaCloud(
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
            var lote = this.unitOfWork.Obter(() => this.loteRepositorio.ObterPorId(loteId));

            if (lote.CloudOk == false)
            {
                ////Apenas pra envitar erros de status e fase
                throw new Exception("Lote " + lote.Id + " ainda não foi enviado para cloud e não poderá enviar os jpgs agora.");
            }

            if (lote.JpegsEnviadosParaCloud)
            {
                ////Apenas pra envitar erros de status e fase
                throw new Exception("Lote " + lote.Id + " já teve seus jpegs enviados.");
            }

            var paginas = this.unitOfWork.Obter(() => this.paginaRepositorio.ObterJpegsDoLote(loteId));
            var paginasNaoEnviadas = paginas.Where(x => x.CloudOk == false);

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

            foreach (var pagina in paginasNaoEnviadas)
            {
                this.unitOfWork.Transacionar(() =>
                {
                    var inicio = DateTime.Now;
                    var arquivoLocal = arquivosBaixados[pagina.Id];

                    if (this.fileSystem.Exists(arquivoLocal) == false)
                    {
                        Log.Application.Error("Não foi possível baixar pagina " + pagina.Id + " do filetransfer. Não existe no fileTransfer " + pagina.DataCenter);

                        this.gravaLogDaPaginaServico.Executar(
                            LogPagina.AcaoPostadoNoCloud,
                            pagina.Id,
                            pagina.Documento.Id,
                            "Não Postado no Cloud. Não existia jpeg no filetransfer");

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
                            "Jpeg Postado no Cloud. Levou " + tempo.TotalMilliseconds + "ms");
                    }
                });
            }

            this.unitOfWork.Transacionar(() => this.loteRepositorio.MarcarComoJpegsEnviadosParaCloud(loteId));

            foreach (var arquivosBaixado in arquivosBaixados)
            {
                Log.Application.Info("Apagando jpeg do cache . " + arquivosBaixado.Value);
                this.fileSystem.DeleteFile(arquivosBaixado.Value);
            }
        }
    }
}