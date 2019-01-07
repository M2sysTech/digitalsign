namespace Veros.Paperless.Model.Servicos.Indexacoes
{
    using Veros.Paperless.Model.Entidades;

    public class FixaIndexacaoDocumentoIdentificacaoServico : IFixaIndexacaoDocumentoIdentificacaoServico
    {
        private readonly FixarCampoPaiDocumentoIdentificacao fixarCampoPaiDocumentoIdentificacao;

        public FixaIndexacaoDocumentoIdentificacaoServico(
            FixarCampoPaiDocumentoIdentificacao fixarCampoPaiDocumentoIdentificacao)
        {
            this.fixarCampoPaiDocumentoIdentificacao = fixarCampoPaiDocumentoIdentificacao;
        }

        public void Executar(Documento documento)
        {
            if (this.fixarCampoPaiDocumentoIdentificacao.PodeComplementarIndexacao(documento))
            {
                var indexacao = this.fixarCampoPaiDocumentoIdentificacao.Indexacao;
                indexacao.PrimeiroValor = indexacao.SegundoValor;

                this.fixarCampoPaiDocumentoIdentificacao.SalvarIndexacao(indexacao);
            }
        }
    }
}