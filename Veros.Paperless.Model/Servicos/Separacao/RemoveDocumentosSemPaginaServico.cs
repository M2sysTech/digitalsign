namespace Veros.Paperless.Model.Servicos.Separacao
{
    using System.Linq;
    using Entidades;
    using Repositorios;
    using ViewModel;

    public class RemoveDocumentosSemPaginaServico : IRemoveDocumentosSemPaginaServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;

        public RemoveDocumentosSemPaginaServico(
            IDocumentoRepositorio documentoRepositorio, 
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
        }

        public void Executar(LoteParaSeparacaoViewModel loteParaSeparacao)
        {
            var paginas = loteParaSeparacao.ObterPaginas();
            var listaDocumentacaoGeral = this.documentoRepositorio.ObterDocumentosDoLotePorTipo(loteParaSeparacao.LoteId, TipoDocumento.CodigoDocumentoGeral).Select(x => x.Id).ToList();

            foreach (var documento in loteParaSeparacao.Documentos)
            {
                if (paginas.Any(x => x.DocumentoAtualId() == documento.Id))
                {
                    if (documento.Manipulado && listaDocumentacaoGeral.Any(x => x == documento.Id) == false)
                    {
                        this.documentoRepositorio.AlterarRecognitionService(documento.Id, DocumentoStatus.TransmissaoOk, false);
                    }

                    continue;
                }

                this.Excluir(documento.Id);

                var documentosFilhos = this.documentoRepositorio.ObterFilhos(loteParaSeparacao.LoteId, documento.Id);

                foreach (var documentoFilho in documentosFilhos)
                {
                    this.Excluir(documentoFilho.Id);
                }
            }
        }

        private void Excluir(int documentoId)
        {
            this.documentoRepositorio.AlterarStatus(documentoId, DocumentoStatus.Excluido);
            this.gravaLogDoDocumentoServico.Executar(LogDocumento.AcaoExcluidoNaClassificacao, documentoId, "Documento excluído na separação");
        }
    }
}
