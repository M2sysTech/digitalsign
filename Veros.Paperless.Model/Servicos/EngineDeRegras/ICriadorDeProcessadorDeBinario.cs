namespace Veros.Paperless.Model.Servicos.EngineDeRegras
{
    using Entidades;

    public interface ICriadorDeProcessadorDeBinario
    {
        IProcessadorDeBinario Obter(Binario binario);
    }
}