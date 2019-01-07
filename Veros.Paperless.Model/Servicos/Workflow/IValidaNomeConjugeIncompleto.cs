namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Entidades;

    public interface IValidaNomeConjugeIncompleto
    {
        bool Validar(Processo processo);
    }
}
