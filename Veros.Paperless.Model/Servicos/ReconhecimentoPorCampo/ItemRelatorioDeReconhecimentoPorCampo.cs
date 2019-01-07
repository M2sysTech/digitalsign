namespace Veros.Paperless.Model.Servicos.ReconhecimentoPorCampo
{
    public class ItemRelatorioDeReconhecimentoPorCampo
    {
        public int CampoId { get; set; }

        public string CampoNome { get; set; }

        public long PercentualAcertos { get; set; }

        public long PercentualFalsoPositivo { get; set; }

        public long PercentualBatimento { get; set; }
    }
}