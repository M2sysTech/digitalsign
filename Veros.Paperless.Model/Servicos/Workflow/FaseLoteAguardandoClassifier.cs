namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System.Linq;
    using Veros.Paperless.Model.Repositorios;
    using Entidades;

    public class FaseLoteAguardandoClassifier : FaseDeWorkflow<Lote, LoteStatus>
    {
        public FaseLoteAguardandoClassifier()
        {
            this.StatusDaFase = LoteStatus.AguardandoClassifier;
            this.StatusSeFaseEstiverInativa = LoteStatus.ClassifierExecutado;
            this.FaseEstaAtiva = x => x.ClassifierAtivo;
        }

        protected override void ProcessarFase(Lote lote)
        {
            foreach (var processo in lote.Processos)
            {
                foreach (var documento in processo.Documentos.Where(x => x.Status != DocumentoStatus.Excluido))
                {
                    if (documento.Status == DocumentoStatus.AguardandoClassifier)
                    {
                        return;
                    }
                }
            }

            lote.Status = LoteStatus.SetaIdentificacao;
        }
    }
}