namespace Veros.Paperless.Model.Servicos.Captura
{
    using System.Linq;
    using Entidades;
    using Repositorios;

    public class PrecisaSolicitarFichaServico : IPrecisaSolicitarFichaServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;

        public PrecisaSolicitarFichaServico(IDocumentoRepositorio documentoRepositorio)
        {
            this.documentoRepositorio = documentoRepositorio;
        }

        public bool Executar(int loteId)
        {
            var documentos = this.documentoRepositorio.ObterDocumentosDoLotePorTipo(loteId, TipoDocumento.CodigoFichaDeCadastro);

            if (documentos == null || documentos.Any() == false)
            {
                return true;
            }

            if (this.CampoFichaVirtualEhSim(documentos.FirstOrDefault()))
            {
                return true;
            }

            return false;
        }

        private bool CampoFichaVirtualEhSim(Documento ficha)
        {
            var indexacao = ficha.Indexacao.FirstOrDefault(x => x.Campo.Id == Campo.CampoFichaVirtual);

            if (indexacao == null)
            {
                return true;
            }

            return indexacao.ObterValor() == "S";
        }
    }
}
