namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Entidades;

    public interface IFasesDeProcesso
    {
        IFaseDeWorkflow<Processo, ProcessoStatus>[] Obter();
    }
}