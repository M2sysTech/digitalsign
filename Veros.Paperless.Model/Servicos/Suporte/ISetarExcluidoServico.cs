namespace Veros.Paperless.Model.Servicos.Suporte
{
    public interface ISetarExcluidoServico
    {
        void Executar(int documentoId, string motivo);
    }
}
