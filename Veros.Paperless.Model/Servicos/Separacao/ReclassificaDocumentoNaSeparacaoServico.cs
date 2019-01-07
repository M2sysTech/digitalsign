namespace Veros.Paperless.Model.Servicos.Separacao
{
    using Entidades;
    using Repositorios;
    using ViewModel;

    public class ReclassificaDocumentoNaSeparacaoServico : IReclassificaDocumentoNaSeparacaoServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly ITipoDocumentoRepositorio tipoDocumentoRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;

        public ReclassificaDocumentoNaSeparacaoServico(
            IDocumentoRepositorio documentoRepositorio, 
            ITipoDocumentoRepositorio tipoDocumentoRepositorio, 
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.tipoDocumentoRepositorio = tipoDocumentoRepositorio;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
        }

        public void Executar(AcaoDeSeparacao acao, LoteParaSeparacaoViewModel loteParaSeparacao)
        {
            var tipoDocumento = this.tipoDocumentoRepositorio.ObterPorId(acao.TipoDocumentoId);

            var documento = this.documentoRepositorio.ObterPorId(acao.NovoDocumentoId);
            this.Reclassificar(documento, tipoDocumento);

            var documentosFilhos = this.documentoRepositorio.ObterFilhos(documento.Lote.Id, documento.Id);
            foreach (var documentoFilho in documentosFilhos)
            {
                this.Reclassificar(documentoFilho, tipoDocumento);
            }
        }

        private void Reclassificar(Documento documento, TipoDocumento tipoDocumento)
        {
            if (documento.DocumentoPaiId == 0)
            {
                return;
            }

            documento.Reclassificado = true;
            documento.TipoDocumento = tipoDocumento;

            this.documentoRepositorio.Salvar(documento);

            this.gravaLogDoDocumentoServico.Executar(LogDocumento.AcaoDocumentoReclassificado,
                    documento.Id,
                    "Documento reclassificado na separação - Tipo: " + tipoDocumento.Description);
        }
    }
}
