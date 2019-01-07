namespace Veros.Paperless.Model.Entidades
{
    using Veros.Framework.Modelo;

    public class ConfiguracaoDeFases : EntidadeUnica
    {
        public virtual bool AssinaturaDigitalAtivo
        {
            get;
            set;
        }

        public virtual bool IdentificacaoAtivo
        {
            get;
            set;
        }

        public virtual bool MontagemAtivo
        {
            get;
            set;
        }

        public virtual bool FormalisticaAtiva
        {
            get;
            set;
        }

        public virtual bool ValidacaoAtivo
        {
            get;
            set;
        }

        public virtual bool DigitacaoAtivo
        {
            get;
            set;
        }

        public virtual bool RetornoAtivo
        {
            get;
            set;
        }

        public virtual bool ExportacaoAtiva
        {
            get;
            set;
        }

        public virtual bool AprovacaoAtivo
        {
            get;
            set;
        }

        public virtual bool ProvaZeroAtivo
        {
            get;
            set;
        }

        public virtual bool RemessaAtivo
        {
            get;
            set;
        }

        public virtual bool ReconhecimentoAtivo
        {
            get;
            set;
        }

        public virtual bool EnvioAtivo
        {
            get;
            set;
        }

        public virtual bool ConsultaAtivo
        {
            get;
            set;
        }

        public virtual bool ClassifierAtivo
        {
            get;
            set;
        }

        public virtual bool TermoDeAtuacao
        {
            get;
            set;
        }

        public virtual bool TriagemPreOcr
        {
            get;
            set;
        }
    }
}