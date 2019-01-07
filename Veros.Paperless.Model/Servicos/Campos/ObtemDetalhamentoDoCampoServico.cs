namespace Veros.Paperless.Model.Servicos.Campos
{
    using Documentos;
    using Veros.Paperless.Model.Repositorios;

    public class ObtemDetalhamentoDoCampoServico : IObtemDetalhamentoDoCampoServico
    {
        private readonly IIndexacaoRepositorio indexacaoRepositorio;
        private readonly IObtemDetalhamentoDoDocumentoCampoServico obtemDetalhamentoDoDocumentoCampoServico;

        public ObtemDetalhamentoDoCampoServico(
            IIndexacaoRepositorio indexacaoRepositorio,
            IObtemDetalhamentoDoDocumentoCampoServico obtemDetalhamentoDoDocumentoCampoServico)
        {
            this.indexacaoRepositorio = indexacaoRepositorio;
            this.obtemDetalhamentoDoDocumentoCampoServico = obtemDetalhamentoDoDocumentoCampoServico;
        }

        public DetalhamentoDoDocumentoCampo Obter(int indexacaoId)
        {
            return this.obtemDetalhamentoDoDocumentoCampoServico.Obter(this.indexacaoRepositorio.ObterPorId(indexacaoId));
        }
    }
}