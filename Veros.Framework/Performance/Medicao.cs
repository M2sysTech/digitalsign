namespace Veros.Framework.Performance
{
    using System.Diagnostics;

    public class Medicao
    {
        private readonly Stopwatch sw;

        public Medicao()
        {
            this.sw = Stopwatch.StartNew();
        }

        public long MiliSegundos
        {
            get
            {
                return this.sw.ElapsedMilliseconds;
            }
        }

        public decimal Segundos
        {
            get
            {
                return (decimal) this.sw.ElapsedMilliseconds / 1000;
            }
        }

        public string MostrarSegundos()
        {
            return this.Segundos.ToString("#0.00") + "s";
        }

        public override string ToString()
        {
            return this.MiliSegundos.ToString() + "ms";
        }
        
        public decimal ItensPorSegundo(int itens)
        {
            if (itens == 0 || this.MiliSegundos == 0)
            {
                return 0;
            }

            return itens / this.Segundos;
        }
    }
}