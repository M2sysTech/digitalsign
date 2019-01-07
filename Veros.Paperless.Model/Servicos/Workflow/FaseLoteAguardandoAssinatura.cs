namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Entidades;
    using Repositorios;

    public class FaseLoteAguardandoAssinatura : FaseDeWorkflow<Lote, LoteStatus>
    {
        private readonly IDocumentoRepositorio documentoRepositorio;

        public FaseLoteAguardandoAssinatura(IDocumentoRepositorio documentoRepositorio)
        {
            this.FaseEstaAtiva = x => x.AssinaturaDigitalAtivo;
            this.StatusDaFase = LoteStatus.AguardandoAssinatura;
            this.StatusSeFaseEstiverInativa = LoteStatus.AssinaturaFinalizada;

            this.documentoRepositorio = documentoRepositorio;
        }

        protected override void ProcessarFase(Lote lote)
        {
            var todosPdfsAssinados = this.documentoRepositorio.TodasOsPdfsEstaoAssinados(lote.Id);

            if (todosPdfsAssinados)
            {
                lote.Status = LoteStatus.AssinaturaFinalizada;
            }
        }
    }
}