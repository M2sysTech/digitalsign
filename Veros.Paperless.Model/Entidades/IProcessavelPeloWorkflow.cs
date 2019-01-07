namespace Veros.Paperless.Model.Entidades
{
    using Veros.Framework;

    public interface IProcessavelPeloWorkflow<TStatus> where TStatus : EnumerationString<TStatus>
    {
        TStatus Status
        {
            get;
            set;
        }
    }
}