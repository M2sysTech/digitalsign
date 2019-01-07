namespace Veros.Paperless.Model.Servicos.Recepcionar
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Data;
    using Entidades;
    using Framework;
    using Framework.IO;
    using Repositorios;

    public class ImportaImagensCapturaCorrespondencia : IImportaImagensCapturaCorrespondencia
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILoteRepositorio loteRepositorio;
        private readonly IFileSystem fileSystem;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly ISalvarPaginasServico salvarPaginasServico;
        private readonly ITagRepositorio tagRepositorio;
        private readonly IProcessoRepositorio processoRepositorio;
        private bool deveApagarImagens;
        
        public ImportaImagensCapturaCorrespondencia(
            IUnitOfWork unitOfWork, 
            ILoteRepositorio loteRepositorio, 
            IFileSystem fileSystem, 
            IDocumentoRepositorio documentoRepositorio,
            ISalvarPaginasServico salvarPaginasServico, 
            ITagRepositorio tagRepositorio, 
            IProcessoRepositorio processoRepositorio)
        {
            this.unitOfWork = unitOfWork;
            this.loteRepositorio = loteRepositorio;
            this.fileSystem = fileSystem;
            this.documentoRepositorio = documentoRepositorio;
            this.salvarPaginasServico = salvarPaginasServico;
            this.tagRepositorio = tagRepositorio;
            this.processoRepositorio = processoRepositorio;
        }

        public void Executar()
        {
            var lotesPendentes = this.unitOfWork.Obter(() =>
                this.loteRepositorio.ObterPendentesDeImportacao());

            this.deveApagarImagens = this.unitOfWork.Obter(() => 
                this.tagRepositorio.ObterValorPorNome("RECEPCAO_DEL_IMG", "TRUE").ToBoolean());

            foreach (var loteAtual in lotesPendentes)
            {
                var lote = loteAtual;

                try
                {
                    var diretorioImagens = Path.Combine(Contexto.PastaArquivosDossie, lote.Id.ToString());
                    
                    this.ImportarDiretorio(diretorioImagens, lote);

                    this.unitOfWork.Transacionar(() =>
                    { 
                        this.loteRepositorio.AlterarParaRecepcaoFinalizada(lote.Id);
                        this.processoRepositorio.AlterarStatusPorLote(lote.Id, ProcessoStatus.AguardandoTransmissao);
                        
                        var processo = this.processoRepositorio.ObterPorLote(lote.Id);

                        this.processoRepositorio.LimparResponsavelEHoraInicio(processo.Id);
                    });

                    this.ApagarImagens(lote.Id, diretorioImagens);
                }
                catch (Exception exception)
                {
                    Log.Application.Error(string.Format("Erro ao importar lote #{0}", lote.Id), exception);
                }
            }
        }

        private void ApagarImagens(int loteId, string diretorioImagens)
        {
            if (this.deveApagarImagens)
            {
                try
                {
                    Directory.Delete(diretorioImagens, true);
                }
                catch (Exception exception)
                {
                    Log.Application.Error(string.Format("Erro ao tentar excluir pasta de imagens do lote [{0}]", loteId), exception);
                }
            }
            else
            {
                try
                {
                    var novoDiretorio = Path.Combine(Contexto.PastaArquivosDossie, string.Format("{0}_{1}", DateTime.Now.ToString("yyMMddhhmmss"), loteId));

                    Directory.Move(diretorioImagens, novoDiretorio);
                }
                catch (Exception exception)
                {
                    Log.Application.Error(string.Format("Erro ao renomear pasta de imagens do lote [{0}]", loteId), exception);
                }
            }
        }

        private void ImportarDiretorio(string imagensDoLote, Lote lote)
        {
            Log.Application.DebugFormat("Inserindo paginas do lote [{0}]", lote.Id);
            var imagens = this.fileSystem.GetFiles(imagensDoLote, "*.*");

            var documentoLista = this.unitOfWork.Obter(() => this.documentoRepositorio
                .ObterTodosPorLote(lote.Id).Where(x => x.Status != DocumentoStatus.Excluido));

            foreach (var imagem in imagens)
            {
                var tipoDocumentoId = this.ObterTipoDocumento(imagem);
                var documentoAtual = documentoLista.LastOrDefault(x => x.TipoDocumento.Id == tipoDocumentoId);

                if (documentoAtual != null)
                {
                    this.unitOfWork.Transacionar(() => this.salvarPaginasServico.Executar(imagem, documentoAtual, documentoAtual.Paginas.Count + 1));
                }
            }
        }

        private int ObterTipoDocumento(string imagem)
        {
            ////271_00083851143_1_0000000006F
            //// 62_00083851143_1_4708227F
            var nomeArquivo = Path.GetFileNameWithoutExtension(imagem);

            var pattern = new Regex(@"(?<tipoDocumento>\d+)_[0-9]{1,100}_[0-9]{1,15}_[0-9]{1}_[0-9]{3,10}(F|V)");
            var match = pattern.Match(nomeArquivo);
            var tipoDocumentoId = match.Groups["tipoDocumento"].Value;

            return tipoDocumentoId.ToInt();
        }
    }
}