namespace Veros.Paperless.Model.Servicos.EngineDeRegras
{
    using Veros.Paperless.Model.Entidades;

    public interface IExecutorDeRegra
    {
        bool ExistemRegrasAtendidas(string faseAtual, Processo processo);
    }
}