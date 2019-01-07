namespace Veros.Paperless.Model.Servicos.Separacao
{
    using System.Linq;
    using Entidades;
    using Importacao;
    using Repositorios;
    using ViewModel;

    public class CriaDocumentoNaSeparacaoServico : ICriaDocumentoNaSeparacaoServico
    {
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;
        private readonly IGravaLogDaPaginaServico gravaLogDaPaginaServico;
        private readonly IDocumentoFabrica documentoFabrica;
        private readonly IDocumentoRepositorio documentoRepositorio;

        public CriaDocumentoNaSeparacaoServico(
            IPaginaRepositorio paginaRepositorio,
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico,
            IGravaLogDaPaginaServico gravaLogDaPaginaServico,
            IDocumentoFabrica documentoFabrica,
            IDocumentoRepositorio documentoRepositorio)
        {
            this.paginaRepositorio = paginaRepositorio;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
            this.gravaLogDaPaginaServico = gravaLogDaPaginaServico;
            this.documentoFabrica = documentoFabrica;
            this.documentoRepositorio = documentoRepositorio;
        }

        public void Executar(AcaoDeSeparacao acao, LoteParaSeparacaoViewModel loteParaSeparacao)
        {
            var documento = this.CriarDocumento(acao, loteParaSeparacao);
            loteParaSeparacao.Documentos.Add(DocumentoParaSeparacaoViewModel.Criar(documento));
            loteParaSeparacao.DocumentosNovos.Add(documento);

            this.SalvarPaginas(documento, acao, loteParaSeparacao);
        }

        private void SalvarPaginas(Documento documento, AcaoDeSeparacao acao, LoteParaSeparacaoViewModel loteParaSeparacao)
        {
            var listaDocumentacaoGeral = this.documentoRepositorio.ObterDocumentosDoLotePorTipo(documento.Lote.Id, TipoDocumento.CodigoDocumentoGeral).Select(x => x.Id).ToList();

            foreach (var paginaId in acao.Paginas)
            {
                var pagina = this.paginaRepositorio.ObterPorId(paginaId);
                var documentoOriginal = this.documentoRepositorio.ObterPorId(pagina.Documento.Id);

                //// manda refazer o PDF do documento original, se for diferente de 27 
                if (listaDocumentacaoGeral.Any(x => x == documentoOriginal.Id) == false)
                {
                    loteParaSeparacao.MarcaDocumentoManipulado(documentoOriginal.Id);
                }

                var paginaParaSeparacaoAtual = loteParaSeparacao.ObterPagina(paginaId).Status;
                var paginaEstavaExcluida = paginaParaSeparacaoAtual == null || 
                    loteParaSeparacao.ObterPagina(paginaId).Status == PaginaStatus.StatusExcluida;

                if ((documentoOriginal.TipoDocumento.Id == TipoDocumento.CodigoDocumentoGeral && 
                    documentoOriginal.Status == DocumentoStatus.Excluido) ||
                    paginaEstavaExcluida)
                {
                    pagina.Status = PaginaStatus.StatusExcluida;
                }

                pagina.Documento = documento;
                this.paginaRepositorio.Salvar(pagina);

                documento.Paginas.Add(pagina);

                this.documentoRepositorio.AlterarMarca(documentoOriginal.Id, Documento.MarcaDeAlteradoNaSeparacao);

                this.gravaLogDaPaginaServico.Executar(LogPagina.AcaoDocumentoCriadoNaSeparacao,
                    pagina.Id,
                    pagina.Documento.Id,
                    string.Format("Página movida para documento #{0}, documento anterior era #{1}", documento.Id, documentoOriginal.Id));

                loteParaSeparacao.ObterPagina(paginaId).NovoDocumentoId = documento.Id;
            }
        }

        private Documento CriarDocumento(AcaoDeSeparacao acao, LoteParaSeparacaoViewModel loteParaSeparacao)
        {
            var processo = new Processo
            {
                Id = loteParaSeparacao.ProcessoId,
                Lote = new Lote { Id = loteParaSeparacao.LoteId }
            };

            var tipoDocumento = new TipoDocumento { Id = acao.TipoDocumentoId };

            var documento = this.documentoFabrica.Criar(processo, tipoDocumento, "1");
            documento.Ordem = acao.NovoDocumentoOrdem;
            documento.Marca = Documento.MarcaDeCriadoNaSeparacao;
            documento.Versao = "0";
            ////documento.Status = ????
            this.documentoRepositorio.Salvar(documento);

            this.gravaLogDoDocumentoServico.Executar(
                LogDocumento.AcaoDocumentoCriadoNaSeparacao,
                documento.Id,
                "Documento criado na separação");

            return documento;
        }
    }
}
