namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Entidades;
    using System.Linq;

    public class FaseLoteSetaReconhecimento : FaseDeWorkflow<Lote, LoteStatus>
    {
        public FaseLoteSetaReconhecimento()
        {
            this.StatusDaFase = LoteStatus.SetaReconhecimento;
            this.StatusSeFaseEstiverInativa = LoteStatus.AguardandoSeparacaoClassifier;
            this.FaseEstaAtiva = x => x.ReconhecimentoAtivo;
        }

        protected override void ProcessarFase(Lote lote)
        {
            lote.Status = LoteStatus.AguardandoReconhecimento;
            
            foreach (var processo in lote.Processos)
            {
                var documentosAtivos = processo.Documentos.Where(x => x.Status != DocumentoStatus.Excluido);
                foreach (var documento in documentosAtivos)
                {
                    if (documento.Virtual)
                    {
                        documento.AlterarStatusDasPaginas(
                           PaginaStatus.StatusReconhecimentoExecutado);

                        continue;
                    }

                    ////Se eh a primeira vez aqui..... 
                    if (documento.Status == DocumentoStatus.TransmissaoOk)
                    {
                        documento.SetarParaOcrNasPaginasQueAindaNaoFizeram(
                            PaginaStatus.StatusParaReconhecimento);
                    }
                    else
                    {
                        if (documento.Marca == "M" || documento.Marca == "A")
                        {
                            documento.SetarParaOcrNasPaginasQueAindaNaoFizeram(
                                PaginaStatus.StatusParaReconhecimento);
                        }
                        else
                        {
                            documento.AlterarStatusDasPaginas(
                            PaginaStatus.StatusReconhecimentoExecutado);
                        }
                    }
                }
            }
        }
    }
}