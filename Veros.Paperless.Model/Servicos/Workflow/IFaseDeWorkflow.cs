namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System;
    using Entidades;
    using Framework;

    public interface IFaseDeWorkflow<T, TStatus> 
        where T : IProcessavelPeloWorkflow<TStatus>
        where TStatus : EnumerationString<TStatus>
    {
        TStatus StatusDaFase
        {
            get;
        }

        TStatus StatusSeFaseEstiverInativa
        {
            get;
        }

        Func<ConfiguracaoDeFases, bool> FaseEstaAtiva
        {
            get;
        }

        void Processar(T item, ConfiguracaoDeFases configuracaoDeFases, bool force = false);
    }
}