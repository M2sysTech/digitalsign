namespace Veros.Paperless.Model.Servicos.Batimento.Experimental.BatimentoPorDocumento
{
    public interface IBatimentoIndexacaoMapeada
    {
        bool EstaBatido(IIndexacaoMapeada indexacaoMapeada);

        IBatimentoIndexacaoMapeada ProximaTentativa(
            IBatimentoIndexacaoMapeada batimentoIndexacaoMapeada);
    }
}