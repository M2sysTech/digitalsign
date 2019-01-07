namespace Veros.Paperless.Infra.Consultas
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Rajada
    {
        public string Arquivo { get; set; }

        public DateTime DataRecebimento { get; set; }

        public int TotalDeContas { get; set; }

        public IList<IntervaloDeRajada> Intervalos { get; set; }

        public double TempoTotal()
        {
            if (this.Intervalos.Count == 0)
            {
                return 0;
            }
            
            var maiorData = this.Intervalos.Max(x => x.Hora);

            return maiorData.Subtract(this.DataRecebimento).TotalHours;
        }
    }
}