namespace Veros.Paperless.Model.Servicos.Indexacoes
{
    using System.Linq;
    using Entidades;
    using Repositorios;

    public abstract class FixarCampo
    {
        private readonly IIndexacaoRepositorio indexacaoRepositorio;

        protected FixarCampo(IIndexacaoRepositorio indexacaoRepositorio)
        {
            this.indexacaoRepositorio = indexacaoRepositorio;
        }

        public Indexacao Indexacao
        {
            get;
            protected set;
        }

        public abstract bool PodeComplementarIndexacao(Documento documento);

        public void SalvarIndexacao(Indexacao indexacao)
        {
            indexacao.OcrComplementou = true;
            indexacao.ValorFinal = string.Empty;

            this.indexacaoRepositorio.Salvar(indexacao);
        }

        protected Indexacao ObterIndexacao(Documento documento, string campoReferenciaArquivo)
        {
            var indexacao = documento
               .Indexacao
               .FirstOrDefault(x => x.Campo.ReferenciaArquivo == campoReferenciaArquivo);

            return indexacao;
        }
    }
}