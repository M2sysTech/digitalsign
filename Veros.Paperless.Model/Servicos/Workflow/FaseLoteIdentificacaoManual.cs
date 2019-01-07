namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System.Linq;
    using Entidades;

    public class FaseLoteIdentificacaoManual : FaseDeWorkflow<Lote, LoteStatus>
    {
        public FaseLoteIdentificacaoManual()
        {
            this.StatusDaFase = LoteStatus.AguardandoIdentificacaoManual;
            this.StatusSeFaseEstiverInativa = LoteStatus.SetaReconhecimento;
            this.FaseEstaAtiva = x => x.IdentificacaoAtivo;
        }

        protected override void ProcessarFase(Lote lote)
        {
            foreach (var processo in lote.Processos)
            {
                foreach (var documento in processo.Documentos.Where(x => x.Status != DocumentoStatus.Excluido))
                {
                    if (documento.TipoDocumento.Id == TipoDocumento.CodigoNaoIdentificado)
                    {
                        return;
                    }
                }
            }

            lote.Status = LoteStatus.SetaReconhecimento;
        }
    }
}