namespace Veros.Paperless.Model.Servicos.Importacao
{
    using System.Linq;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class GravaFlagDeFichaVirtualServico : IGravaFlagDeFichaVirtualServico
    {
        private readonly IIndexacaoFabrica indexacaoFabrica;
        private readonly IIndexacaoRepositorio indexacaoRepositorio;
        private readonly IDocumentoRepositorio documentoRepositorio;

        public GravaFlagDeFichaVirtualServico(
            IIndexacaoFabrica indexacaoFabrica,
            IIndexacaoRepositorio indexacaoRepositorio,
            IDocumentoRepositorio documentoRepositorio)
        {
            this.indexacaoFabrica = indexacaoFabrica;
            this.indexacaoRepositorio = indexacaoRepositorio;
            this.documentoRepositorio = documentoRepositorio;
        }

        public void Gravar(int loteId, bool possuiFichaReal)
        {
            var fichaVirtual = possuiFichaReal ? "N" : "S";

            var fichas = this.documentoRepositorio.ObterDocumentosDoLotePorTipo(loteId, TipoDocumento.CodigoFichaDeCadastro);

            if (fichas == null || fichas.Any() == false)
            {
                return;
            }

            var ficha = fichas.FirstOrDefault();

            var indexacao = ficha.Indexacao.FirstOrDefault(x => x.Campo.Id == Campo.CampoFichaVirtual);

            if (indexacao == null)
            {
                indexacao = this.indexacaoFabrica.Criar(ficha, Campo.CampoFichaVirtual, fichaVirtual);
            }

            this.documentoRepositorio.GravarComoFichaVirtual(ficha.Id);

            this.indexacaoRepositorio.Salvar(indexacao);
        }
    }
}
