namespace Veros.Paperless.Model.Servicos.Batimento.Experimental.BatimentoPorDocumento
{
    using Comparacao;
    using Framework;

    public class QualquerDocumentoBatimento : ModuloBatimentoIndexacaoMapeada
    {
        public QualquerDocumentoBatimento(ICriadorDeComparador criadorDeComparador) : 
            base(criadorDeComparador)
        {
        }

        public override bool EstaBatido(IIndexacaoMapeada indexacaoMapeada)
        {
            var comparador = this.criadorDeComparador.Cria(indexacaoMapeada.Indexacao.Campo.TipoCampo);
            
            var batido = comparador.SaoIguais(
                indexacaoMapeada.ObterValorParaBatimento(),
                indexacaoMapeada.ValorReconhecido.Value);

            if (batido == false)
            {
                if (this.proximaTentativa.NaoTemConteudo() == false)
                {
                    batido = this.proximaTentativa.EstaBatido(indexacaoMapeada);
                }
            }

            return batido;
        }
    }
}