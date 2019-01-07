namespace Veros.Paperless.Model.Servicos.Documentos
{
    using Campos;
    using Veros.Paperless.Model.Entidades;

    public class ObtemDetalhamentoDoDocumentoCampoServico : IObtemDetalhamentoDoDocumentoCampoServico
    {
        private readonly IObtemCampoFormatadoServico obtemCampoFormatadoServico;
        private readonly IObtemOpcoesDeValorDeIndexacaoServico obtemOpcoesDeValorDeIndexacaoServico;

        public ObtemDetalhamentoDoDocumentoCampoServico(
            IObtemCampoFormatadoServico obtemCampoFormatadoServico, 
            IObtemOpcoesDeValorDeIndexacaoServico obtemOpcoesDeValorDeIndexacaoServico)
        {
            this.obtemCampoFormatadoServico = obtemCampoFormatadoServico;
            this.obtemOpcoesDeValorDeIndexacaoServico = obtemOpcoesDeValorDeIndexacaoServico;
        }

        public DetalhamentoDoDocumentoCampo Obter(Indexacao indexacao)
        {
            return new DetalhamentoDoDocumentoCampo
            {
                IndexacaoId = indexacao.Id,
                Campo = indexacao.Campo,
                Valor1 = indexacao.PrimeiroValor,
                Valor2 = indexacao.SegundoValor,
                ValorOk = indexacao.ValorFinal,
                Valor1Formatado = this.obtemCampoFormatadoServico.Obter(indexacao.Campo, indexacao.PrimeiroValor),
                Valor2Formatado = this.obtemCampoFormatadoServico.Obter(indexacao.Campo, indexacao.SegundoValor),
                ValorFormatado = this.obtemCampoFormatadoServico.Obter(indexacao.Campo, indexacao.ObterValor()),
                DominioDeValores = this.obtemOpcoesDeValorDeIndexacaoServico.Obter(indexacao)
            };
        }
    }
}