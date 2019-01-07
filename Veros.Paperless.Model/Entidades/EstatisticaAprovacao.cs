namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework.Modelo;

    public class EstatisticaAprovacao : Entidade
    {
        public EstatisticaAprovacao()
        {
            this.DataRegistro = DateTime.Today;
        }

        public virtual Usuario Usuario { get; set; }

        public virtual DateTime DataRegistro { get; set; }

        public virtual int TotalDevolvidas { get; set; }

        public virtual int TotalLiberadas { get; set; }

        public virtual int TotalFraudes { get; set; }

        public virtual int TotalAbandonadas { get; set; }

        public virtual int TotalDevolvidasComFraude { get; set; }

        public virtual int TotalLiberadasComFraude { get; set; }

        public virtual int Producao
        {
            get
            {
                return this.TotalDevolvidas + this.TotalLiberadas + this.TotalDevolvidasComFraude + this.TotalLiberadasComFraude;
            }
        }

        public virtual int TotalDoDia
        {
            get
            {
                return this.TotalDevolvidas + this.TotalLiberadas + this.TotalDevolvidasComFraude + this.TotalLiberadasComFraude;
            }
        }

        public virtual int SomaTotalFraudes()
        {
            return this.TotalDevolvidasComFraude + this.TotalLiberadasComFraude;
        }

        public virtual int TotalDoDiaComAbandonos()
        {
            return this.TotalDevolvidas + this.TotalLiberadas + this.TotalAbandonadas;
        }

        public virtual int TotalFraudeDoDia()
        {
            return this.TotalLiberadasComFraude + this.TotalDevolvidasComFraude;
        }

        public virtual int TotalLiberadasDoDia()
        {
            return this.TotalLiberadasComFraude + this.TotalLiberadas;
        }

        public virtual int TotalDevolvidasDoDia()
        {
            return this.TotalDevolvidasComFraude + this.TotalDevolvidas;
        }
    }
}