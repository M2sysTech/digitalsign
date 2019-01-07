namespace Veros.Paperless.Model.Servicos.EngineDeRegras
{
    using Entidades;

    public interface ISalvaRegraViolada
    {
        void Salvar(Processo processo, RegraViolada regraViolada, string faseAtual);
    }
}