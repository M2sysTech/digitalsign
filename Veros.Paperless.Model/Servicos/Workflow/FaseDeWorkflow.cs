namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System;
    using Entidades;
    using Framework;
    using FrameworkLocal;

    public abstract class FaseDeWorkflow<T, TStatus> : IFaseDeWorkflow<T, TStatus>
        where T : IProcessavelPeloWorkflow<TStatus>
        where TStatus : EnumerationString<TStatus>
    {
        public bool ExecutouFase
        {
            get;
            protected set;
        }

        public TStatus StatusDaFase
        {
            get;
            protected set;
        }

        public TStatus StatusSeFaseEstiverInativa
        {
            get;
            protected set;
        }

        public Func<ConfiguracaoDeFases, bool> FaseEstaAtiva
        {
            get;
            protected set;
        }

        public void Processar(T item, ConfiguracaoDeFases configuracaoDeFases, bool force = false)
        {
            Throw.SeForNulo(this.StatusDaFase, "this.StatusDaFase deve ser setado no construtor da Fase");
            Throw.SeForNulo(this.StatusSeFaseEstiverInativa, "this.StatusSeFaseEstiverInativa deve ser setado no construtor da Fase");
            Throw.SeForNulo(this.FaseEstaAtiva, "this.FaseEstaAtiva deve ser setado no construtor da Fase");

            if (item.Status != this.StatusDaFase && force == false)
            {
                return;
            }

            Log.Application.DebugFormat("Processando {0} com status {1}", item, this.StatusDaFase);

            if (this.FaseEstaAtiva(configuracaoDeFases) == false && force == false)
            {
                Log.Application.DebugFormat("Fase {0} está inativa", this.StatusDaFase);
                item.Status = this.StatusSeFaseEstiverInativa;
            }
            else
            {
                this.ProcessarFase(item);
                this.ExecutouFase = true;
            }

            Log.Application.DebugFormat("{0} processado. Novo status {1}", item, item.Status);
        }

        protected abstract void ProcessarFase(T item);
    }
}