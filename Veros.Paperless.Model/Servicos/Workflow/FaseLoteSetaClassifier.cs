namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Entidades;
    using System.Linq;
    using Veros.Paperless.Model.Repositorios;

    public class FaseLoteSetaClassifier : FaseDeWorkflow<Lote, LoteStatus>
    {
        private readonly IDocumentoRepositorio documentoRepositorio;

        public FaseLoteSetaClassifier(IDocumentoRepositorio documentoRepositorio)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.StatusDaFase = LoteStatus.SetaClassifier;
            this.StatusSeFaseEstiverInativa = LoteStatus.ClassifierExecutado;
            this.FaseEstaAtiva = x => x.ClassifierAtivo;
        }

        protected override void ProcessarFase(Lote lote)
        {
            lote.Status = LoteStatus.AguardandoClassifier;

            foreach (var processo in lote.Processos)
            {
                foreach (var documento in processo.Documentos.Where(x => x.Status != DocumentoStatus.Excluido))
                {
                    this.documentoRepositorio.AtualizaStatusDocumento(documento.Id, DocumentoStatus.AguardandoClassifier);
                    documento.Status = DocumentoStatus.AguardandoClassifier;
                }

                ////foreach (var documento in processo.Documentos.Where(x => x.Status != DocumentoStatus.Excluido))
                ////{
                ////    //// soh vai processar Classifier dos tipos de documentos permitidos. O resto deve pular essa fase. 
                ////    if (TipoDocumento.TiposDocsPermitidosOcr().Any(x => x == documento.TipoDocumento.Id))
                ////    {
                ////        this.documentoRepositorio.AtualizaStatusDocumento(documento.Id, DocumentoStatus.AguardandoClassifier);
                ////        documento.Status = DocumentoStatus.AguardandoClassifier;
                ////    }
                ////    else
                ////    {
                ////        this.documentoRepositorio.AtualizaStatusDocumento(documento.Id, DocumentoStatus.DoneClassifier);
                ////        documento.Status = DocumentoStatus.DoneClassifier;
                ////    }
                ////}
            }
        }
    }
}