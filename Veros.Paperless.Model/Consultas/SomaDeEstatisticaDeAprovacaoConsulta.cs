namespace Veros.Paperless.Model.Consultas
{
    using System;

    public class SomaDeEstatisticaDeAprovacaoConsulta
    {
        public int TotalDevolvidas { get; set; }

        public int TotalLiberadas { get; set; }

        public int TotalFraudes { get; set; }

        public int TotalAbandonadas { get; set; }

        public DateTime DataRegistro { get; set; }

        public int TotalDevolvidasComFraude { get; set; }

        public int TotalLiberadasComFraude { get; set; }

        public int TotalDoDia
        {
            get
            {
                return this.TotalDevolvidas + this.TotalLiberadas + this.TotalDevolvidasComFraude + this.TotalLiberadasComFraude;
            }
        }

        public int TotalDoDiaComAbandonos()
        {
            return this.TotalDevolvidas + this.TotalLiberadas + this.TotalAbandonadas;
        }
            
        public int TotalFraudeDoDia()
        {
            return this.TotalLiberadasComFraude + this.TotalDevolvidasComFraude;
        }

        public int TotalLiberadasDoDia()
        {
            return this.TotalLiberadasComFraude + this.TotalLiberadas;
        }

        public int TotalDevolvidasDoDia()
        {
            return this.TotalDevolvidasComFraude + this.TotalDevolvidas;
        }
    }
}
