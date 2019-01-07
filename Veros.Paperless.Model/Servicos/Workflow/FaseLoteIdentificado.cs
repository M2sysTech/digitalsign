namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Entidades;

    public class FaseLoteIdentificado : FaseDeWorkflow<Lote, LoteStatus>
    {
        public FaseLoteIdentificado()
        {
            this.FaseEstaAtiva = x => x.FormalisticaAtiva;
            this.StatusDaFase = LoteStatus.Identificado;
            this.StatusSeFaseEstiverInativa = LoteStatus.SetaControleQualidadeM2;
        }

        protected override void ProcessarFase(Lote lote)
        {
            lote.Status = LoteStatus.SetaControleQualidadeM2;                    
        }
    }
}