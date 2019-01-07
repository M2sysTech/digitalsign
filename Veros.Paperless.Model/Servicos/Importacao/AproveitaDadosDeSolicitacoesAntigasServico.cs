namespace Veros.Paperless.Model.Servicos.Importacao
{
    using System.Linq;
    using Repositorios;
    using Veros.Paperless.Model.Entidades;

    public class AproveitaDadosDeSolicitacoesAntigasServico : IAproveitaDadosDeSolicitacoesAntigasServico
    {
        private readonly IProcessoRepositorio processoRepositorio;

        public AproveitaDadosDeSolicitacoesAntigasServico(IProcessoRepositorio processoRepositorio)
        {
            this.processoRepositorio = processoRepositorio;
        }

        public void Executar(Processo processo)
        {
            var processoAntigo = this.processoRepositorio.ObterUltimoPorIdentificacao(processo.Lote.Identificacao);

            if (processoAntigo == null)
            {
                return;
            }

            foreach (var documento in processo.Documentos)
            {
                this.AtualizarDocumento(documento, processoAntigo.
                    Documentos.FirstOrDefault(x => x.TipoDocumento.Id == documento.TipoDocumento.Id));
            }
        }

        private void AtualizarDocumento(Documento documento, Documento documentoAntigo)
        {
            if (documentoAntigo == null)
            {
                return;
            }

            foreach (var indexacao in documento.Indexacao)
            {
                this.AtualizarIndexacao(indexacao, documentoAntigo.
                    Indexacao.FirstOrDefault(x => x.Campo.Id == indexacao.Campo.Id));
            }
        }

        private void AtualizarIndexacao(Indexacao indexacao, Indexacao indexacaoAntiga)
        {
            if (indexacaoAntiga == null || indexacaoAntiga.ValorFinalValido() == false || string.IsNullOrEmpty(indexacaoAntiga.ValorFinal))
            {
                return;
            }

            indexacao.PrimeiroValor = indexacaoAntiga.PrimeiroValor;
            indexacao.UsuarioPrimeiroValor = Usuario.CodigoDoUsuarioSistema;

            indexacao.ValorFinal = indexacaoAntiga.ValorFinal;
            indexacao.ValorUtilizadoParaValorFinal = indexacaoAntiga.ValorUtilizadoParaValorFinal;
            indexacao.ValorRecuperado = true;
        }
    }
}
