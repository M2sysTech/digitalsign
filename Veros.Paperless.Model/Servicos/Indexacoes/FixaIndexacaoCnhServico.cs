namespace Veros.Paperless.Model.Servicos.Indexacoes
{
    using Veros.Paperless.Model.Entidades;

    public class FixaIndexacaoCnhServico : IFixaIndexacaoCnhServico
    {
        private readonly FixarCampoEmissorCnh fixarCampoEmissorCnh;
        private readonly FixarCampoPaiCnh fixarCampoPaiCnh;

        public FixaIndexacaoCnhServico(
            FixarCampoEmissorCnh fixarCampoEmissorCnh, 
            FixarCampoPaiCnh fixarCampoPaiCnh)
        {
            this.fixarCampoEmissorCnh = fixarCampoEmissorCnh;
            this.fixarCampoPaiCnh = fixarCampoPaiCnh;
        }

        public void Executar(Documento documento)
        {
            if (this.fixarCampoEmissorCnh.PodeComplementarIndexacao(documento))
            {
                var indexacao = this.fixarCampoEmissorCnh.Indexacao;
                indexacao.PrimeiroValor = "DETRAN";

                this.fixarCampoEmissorCnh.SalvarIndexacao(indexacao);
            }

            if (this.fixarCampoPaiCnh.PodeComplementarIndexacao(documento))
            {
                var indexacao = this.fixarCampoPaiCnh.Indexacao;
                indexacao.PrimeiroValor = indexacao.SegundoValor;

                this.fixarCampoPaiCnh.SalvarIndexacao(indexacao);
            }
        }
    }
}