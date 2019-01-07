namespace Veros.Paperless.Model.Servicos.Indexacoes
{
    using Veros.Paperless.Model.Entidades;

    public class FixaIndexacaoComprovanteResidenciaServico : IFixaIndexacaoComprovanteResidenciaServico
    {
        private readonly FixarCampoComplementoResidencia fixarCampoComplementoResidencia;
        private readonly FixaCampoNumeroResidencia fixarCampoNumeroResidencia;

        public FixaIndexacaoComprovanteResidenciaServico(
            FixarCampoComplementoResidencia fixarCampoComplementoResidencia, 
            FixaCampoNumeroResidencia fixarCampoNumeroResidencia)
        {
            this.fixarCampoComplementoResidencia = fixarCampoComplementoResidencia;
            this.fixarCampoNumeroResidencia = fixarCampoNumeroResidencia;
        }

        public void Executar(Documento documento)
        {
            if (this.fixarCampoComplementoResidencia.PodeComplementarIndexacao(documento))
            {
                var indexacao = this.fixarCampoComplementoResidencia.Indexacao;

                indexacao.PrimeiroValor = string.IsNullOrEmpty(indexacao.SegundoValor) ? 
                    string.Empty : 
                    indexacao.SegundoValor.Trim();

                this.fixarCampoComplementoResidencia.SalvarIndexacao(indexacao);
            }

            if (this.fixarCampoNumeroResidencia.PodeComplementarIndexacao(documento))
            {
                var indexacao = this.fixarCampoNumeroResidencia.Indexacao;

                indexacao.PrimeiroValor = string.IsNullOrEmpty(indexacao.SegundoValor) ?
                    string.Empty :
                    indexacao.SegundoValor.Trim();

                this.fixarCampoNumeroResidencia.SalvarIndexacao(indexacao);
            }
        }
    }
}