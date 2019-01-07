namespace Veros.Paperless.Model.Servicos.Indexacoes
{
    using System.Linq;
    using Entidades;
    using Framework.Modelo;
    using Veros.Paperless.Model.Repositorios;

    public class ValidaAlteracaoDeValorFinalServico : IValidaAlteracaoDeValorFinalServico
    {
        private readonly IIndexacaoRepositorio indexacaoRepositorio;

        public ValidaAlteracaoDeValorFinalServico(
            IIndexacaoRepositorio indexacaoRepositorio)
        {
            this.indexacaoRepositorio = indexacaoRepositorio;
        }
        
        public bool Validar(Indexacao indexacao, string valor)
        {
            if (string.IsNullOrWhiteSpace(valor) && indexacao.Campo.Obrigatorio)
            {
                throw new RegraDeNegocioException("Campo de preenchimento obrigatório.");
            }

            if (indexacao.Campo.ReferenciaArquivo == Campo.ReferenciaDeArquivoEstadoCivilDoParticipante)
            {
                return this.ValidarEstadoCivil(indexacao, valor);
            }

            ////if (indexacao.Campo.ReferenciaArquivo == Campo.ReferenciaDeArquivoNomeConjuge)
            ////{
            ////    return this.ValidarNomeConjuge(indexacao, valor);
            ////}

            return true;
        }

        private bool ValidarNomeConjuge(Indexacao indexacaoNomeConjuge, string valor)
        {
            var estadoCivil = this.ObterValorPorReferenciaArquivo(indexacaoNomeConjuge, Campo.ReferenciaDeArquivoEstadoCivilDoParticipante);

            if (DominioCampo.EstadoCivilObrigaConjuge(estadoCivil) && string.IsNullOrWhiteSpace(valor))
            {
                throw new RegraDeNegocioException("O estado civil obriga o preenchimento de conjuge.");
            }

            if (DominioCampo.EstadoCivilProibeConjuge(estadoCivil) && string.IsNullOrWhiteSpace(valor) == false)
            {
                throw new RegraDeNegocioException("O estado civil proíbe o preenchimento de conjuge.");
            }

            return true;
        }

        private bool ValidarEstadoCivil(Indexacao indexacaoEstadoCivil, string valor)
        {
            if (DominioCampo.EstadoCivilObrigaConjuge(valor) && string.IsNullOrEmpty(this.ObterValorPorReferenciaArquivo(indexacaoEstadoCivil, Campo.ReferenciaDeArquivoNomeConjuge)))
            {
                throw new RegraDeNegocioException("O nome do conjuge é necessário.");
            }

            if (DominioCampo.EstadoCivilProibeConjuge(valor) && string.IsNullOrEmpty(this.ObterValorPorReferenciaArquivo(indexacaoEstadoCivil, Campo.ReferenciaDeArquivoNomeConjuge)) == false)
            {
                throw new RegraDeNegocioException("O nome do conjuge deve ser retirado.");
            }

            return true;
        }

        private string ObterValorPorReferenciaArquivo(Indexacao indexacao, string referenciaArquivo)
        {
            var indexacaoConjuge = this.indexacaoRepositorio.ObterPorReferenciaDeArquivo(
                indexacao.Documento.Id,
                referenciaArquivo);

            if (indexacaoConjuge.FirstOrDefault() != null)
            {
                return indexacaoConjuge.FirstOrDefault().ObterValor().Trim();
            }

            return string.Empty;
        }
    }
}