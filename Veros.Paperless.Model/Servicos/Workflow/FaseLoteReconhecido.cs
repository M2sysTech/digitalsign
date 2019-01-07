namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System;
    using System.Linq;
    using Entidades;

    public class FaseLoteReconhecido : FaseDeWorkflow<Lote, LoteStatus>
    {
        public FaseLoteReconhecido()
        {
            this.FaseEstaAtiva = x => x.ReconhecimentoAtivo;
            this.StatusDaFase = LoteStatus.ReconhecimentoExecutado;
            this.StatusSeFaseEstiverInativa = LoteStatus.SetaIdentificacao;
        }

        protected override void ProcessarFase(Lote lote)
        {
            lote.Status = LoteStatus.SetaIdentificacao;
            lote.DataFimIcr = DateTime.Now;

            ////this.BaterIndexacaoDocumentos(lote);
            ////lote.Status = LoteStatus.SetaIdentificacao;
            ////lote.DataFimIcr = DateTime.Now;
        }
        
        //// OBS: a verifica��o abaixo passou a ser feita na fase de SetaIdentifica��o, onde isso faz mais sentido. 
        ////private bool ExisteDocumentoNaoIdentificado(Lote lote)
        ////{
        ////    if (lote.Processos.Any())
        ////    {
        ////        if (lote.Processos.First().Documentos.Any(x => x.TipoDocumento.Id == TipoDocumento.CodigoNaoIdentificado))
        ////        {
        ////            return true;
        ////        }
        ////    }

        ////    return false;
        ////}
    }
}
