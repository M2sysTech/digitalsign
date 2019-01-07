namespace Veros.Paperless.Model.Servicos.Exportacao
{
    public interface IEnviaJpgsDoLoteParaCloud
    {
        void Executar(int loteId);
    }
}