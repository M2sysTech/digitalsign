namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Entidades;

    public class FaseLoteExportadoParaCloud : FaseDeWorkflow<Lote, LoteStatus>
    {
        public FaseLoteExportadoParaCloud()
        {
            this.FaseEstaAtiva = x => x.ValidacaoAtivo;
            this.StatusDaFase = LoteStatus.ExportadoParaCloud;
            this.StatusSeFaseEstiverInativa = LoteStatus.SetaControleQualidadeCef;
        }

        protected override void ProcessarFase(Lote lote)
        {
            lote.Status = LoteStatus.SetaControleQualidadeCef;            
        }
    }
}