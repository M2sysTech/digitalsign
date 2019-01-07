namespace Veros.Paperless.Model.ViewModel
{
    public class DetalheProcessamentoQualidadeCefViewModel
    {
        public int TipoDeAmostra { get; set; }

        public int TotalSelecionado { get; set; }

        public int TotalAprovado { get; set; }

        public int TotalMarcado { get; set; }

        public int TotalAguardando
        {
            get
            {
                return this.TotalSelecionado - (this.TotalMarcado + this.TotalAprovado);
            }
        }

        public int TotalEmReprocessamento { get; set; }
    }
}
