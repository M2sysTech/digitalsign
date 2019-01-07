namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework.Modelo;

    public class ExpurgoConfig : EntidadeUnica
    {
        public virtual int QuantidadeDeMovimentosParaNaoExpurgar
        {
            get;
            set;
        }

        public virtual int IntervaloDeDiasBk
        {
            get;
            set;
        }

        public virtual int IntervaloDeDiasHistorico
        {
            get;
            set;
        }

        public virtual int IntervaloDeDiasParaExclusaoHistorico
        {
            get;
            set;
        }

        public virtual int IntervaloDeDiasParaDesfragmentar
        {
            get;
            set;
        }

        public virtual bool DeveApagarImagens
        {
            get;
            set;
        }

        public virtual int HoraParaRodar
        {
            get;
            set;
        }

        public virtual DateTime? UltimoExpurgoBk
        {
            get;
            set;
        }

        public virtual DateTime UltimoExpurgo
        {
            get;
            set;
        }

        public virtual DateTime UltimaDesfragmentacao
        {
            get;
            set;
        }

        public virtual int HoraDesfragmentacao
        {
            get;
            set;
        }

        public virtual bool DeveExpurgar()
        {
            return this.UltimoExpurgo < DateTime.Today && this.HoraParaRodar <= DateTime.Now.Hour;
        }

        public virtual bool DeveMoverParaBk()
        {
            return this.UltimoExpurgoBk.GetValueOrDefault().Date < DateTime.Today && this.HoraParaRodar <= DateTime.Now.Hour;
        }
        
        public virtual bool DeveExpurgarImagens()
        {
            return this.HoraParaRodar <= DateTime.Now.Hour;
        }
        
        public virtual void AtualizarExpurgoBk()
        {
             this.UltimoExpurgoBk = DateTime.Now;
        }

        public virtual void AtualizarUltimoExpurgo()
        {
            this.UltimoExpurgo = DateTime.Now;
        }

        public virtual bool DeveDesfragmentar()
        {
            if (this.UltimaDesfragmentacao.Date.AddDays(this.IntervaloDeDiasParaDesfragmentar) >= DateTime.Today && DateTime.Now.Hour == this.HoraDesfragmentacao)
            {
                return true;
            }

            return false;
        }
    }
}
