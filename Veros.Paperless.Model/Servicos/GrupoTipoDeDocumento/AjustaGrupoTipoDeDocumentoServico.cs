namespace Veros.Paperless.Model.Servicos.GrupoTipoDeDocumento
{
    using System.Linq;
    using Entidades;
    using Repositorios;

    public class AjustaGrupoTipoDeDocumentoServico : IAjustaGrupoTipoDeDocumentoServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;

        public AjustaGrupoTipoDeDocumentoServico(IDocumentoRepositorio documentoRepositorio, 
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
        }

        public void Executar(Lote lote)
        {
            var documentos = this.documentoRepositorio.ObterPorLoteComTipo(lote);

            foreach (var documento in documentos.Where(x => x.TipoDocumento.GrupoId > 0))
            {
                var tipoAnteriorId = documento.TipoDocumento.Id;
                var novoTipoId = documento.TipoDocumento.GrupoId;

                if (tipoAnteriorId == novoTipoId)
                {
                    continue;
                }

                this.documentoRepositorio.AlterarTipo(documento.Id, novoTipoId);

                this.gravaLogDoDocumentoServico.Executar(LogDocumento.AcaoDocumentoGenerico,
                    documento.Id,
                    string.Format("Tipo Documento ajustado de [{0}] para [{1}]", tipoAnteriorId, novoTipoId));
            }
        }
    }
}
