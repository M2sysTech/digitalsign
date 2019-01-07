namespace Veros.Paperless.Model.Servicos.Batimento.Experimental.BatimentoPorDocumento
{
    using Comparacao;

    public abstract class ModuloBatimentoIndexacaoMapeada : IBatimentoIndexacaoMapeada
    {
        protected readonly ICriadorDeComparador criadorDeComparador;
        protected IBatimentoIndexacaoMapeada proximaTentativa;

        protected ModuloBatimentoIndexacaoMapeada(ICriadorDeComparador criadorDeComparador)
        {
            this.criadorDeComparador = criadorDeComparador;
        }

        public abstract bool EstaBatido(IIndexacaoMapeada indexacaoMapeada);
        
        public IBatimentoIndexacaoMapeada ProximaTentativa(IBatimentoIndexacaoMapeada batimentoIndexacaoMapeada)
        {
            this.proximaTentativa = batimentoIndexacaoMapeada;
            return batimentoIndexacaoMapeada;
        }
    }
}