namespace Veros.Paperless.Model.Servicos.Validacao
{
    using System;
    using System.Globalization;

    public class CodigoDeBarras
    {
        public const string CapaSeparadora = "M2SYS SEPARADOR 01";

        public CodigoDeBarras(string codigo)
        {
            this.Valido = false;

            if (string.IsNullOrEmpty(codigo) || codigo.Length < 40)
            {
                return;
            }

            this.Codigo = codigo;
            this.CarregarDataDeVencimento(codigo);
            this.CarregarNossoNumero(codigo);
            this.CarregarValor(codigo);

            this.Valido = true;
        }

        public string Codigo
        {
            get;
            private set;
        }

        public DateTime DataDeVencimento
        {
            get;
            private set;
        }

        public string NossoNumero
        {
            get;
            private set;
        }

        public double Valor
        {
            get;
            private set;
        }

        public string DataDeVencimentoFormatada
        {
            get
            {
                return this.DataDeVencimento.ToString("ddMMyyyy");
            }
        }

        public bool Valido
        {
            get;
            private set;
        }

        public string ValorSemVirgula
        {
            get;
            private set;
        }

        public bool DataDeVencimentoValida()
        {
            DateTime dataMinima;
            DateTime.TryParseExact("1997/10/07", "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataMinima);

            return this.DataDeVencimento > dataMinima;
        }

        private void CarregarDataDeVencimento(string codigoDeBarras)
        {
            var parteDoCodigo = codigoDeBarras.Substring(33, 4);

            double quantidadeDeDias;
            double.TryParse(parteDoCodigo, out quantidadeDeDias);

            DateTime dataDeVencimento;
            DateTime.TryParseExact("1997/10/07", "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataDeVencimento);

            this.DataDeVencimento = dataDeVencimento.AddDays(quantidadeDeDias);
        }

        private void CarregarNossoNumero(string codigo)
        {
            this.NossoNumero = codigo.Substring(4, 5) + codigo.Substring(10, 10) + codigo.Substring(21, 10);
        }

        private void CarregarValor(string codigo)
        {
            var parteDoCodigoDeBarras = codigo.Substring(37);

            var valorComVirgula = string.Format("{0},{1}", 
                parteDoCodigoDeBarras.Substring(0, parteDoCodigoDeBarras.Length - 2), 
                parteDoCodigoDeBarras.Substring(parteDoCodigoDeBarras.Length - 2));

            double valor;
            double.TryParse(valorComVirgula, out valor);
            this.Valor = valor;

            double valorSemVirgula;
            double.TryParse(parteDoCodigoDeBarras, out valorSemVirgula);
            this.ValorSemVirgula = valorSemVirgula.ToString();
        }
    }
}
