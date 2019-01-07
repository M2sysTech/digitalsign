namespace Veros.Paperless.Model.ViewModel
{
    public class ProdutividadeOperadorViewModel
    {
        public string Matricula { get; set; }

        public string Nome { get; set; }

        public int QuantidadeCapturaRecaptura { get; set; }

        public int QuantidadePreparo { get; set; }

        public int QuantidadeTriagem { get; set; }
    
        public int QuantidadeFormalistica { get; set; }

        public int QuantidadeAjuste { get; set; }

        public int QuantidadeQualidadeM2 { get; set; }

        public int QuantidadeAjusteReparacao { get; set; }

        public int TotalAjuste
        {
            get
            {
                return this.QuantidadeAjuste + this.QuantidadeAjusteReparacao;
            }
        }
    }
}
