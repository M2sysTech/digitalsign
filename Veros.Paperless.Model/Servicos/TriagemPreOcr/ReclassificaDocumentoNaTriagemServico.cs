namespace Veros.Paperless.Model.Servicos.TriagemPreOcr
{
    using System.Linq;
    using Entidades;
    using Repositorios;
    using ViewModel;

    public class ReclassificaDocumentoNaTriagemServico : IReclassificaDocumentoNaTriagemServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;

        public ReclassificaDocumentoNaTriagemServico(IDocumentoRepositorio documentoRepositorio, 
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
        }

        public void Executar(AcaoDeTriagemPreOcr acao, LoteTriagemViewModel lote)
        {
            this.Reclassificar(acao.NovoDocumentoId, acao.TipoDocumentoId, lote);

            this.ClassificarFilhos(acao.NovoDocumentoId, acao.TipoDocumentoId, lote);
        }

        private void ClassificarFilhos(int documentoId, int tipoDocumentoId, LoteTriagemViewModel lote)
        {
            var documentos = this.documentoRepositorio.ObterPorLoteComTipo(new Lote { Id = lote.LoteId });

            foreach (var documento in documentos.Where(x => x.DocumentoPaiId == documentoId))
            {
                this.Reclassificar(documento.Id, tipoDocumentoId, lote);
            }
        }

        private void Reclassificar(int documentoId, int tipoDocumentoId, LoteTriagemViewModel lote)
        {
            this.documentoRepositorio.AlterarTipo(documentoId, tipoDocumentoId);

            this.gravaLogDoDocumentoServico.Executar(LogDocumento.AcaoDocumentoReclassificadoNaTriagem,
                documentoId,
                string.Format("Documento reclassificado - Tipo: {0}. [{1}]", tipoDocumentoId, lote.Fase));
        }
    }
}
