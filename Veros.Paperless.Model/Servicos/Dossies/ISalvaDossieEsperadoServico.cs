namespace Veros.Paperless.Model.Servicos.Dossies
{
    using Entidades;

    public interface ISalvaDossieEsperadoServico
    {
        DossieEsperado Executar(DossieEsperado dossieEsperado);
    }
}