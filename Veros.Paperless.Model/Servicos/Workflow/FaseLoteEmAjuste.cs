namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System.Linq;
    using Entidades;

    public class FaseLoteEmAjustes : FaseDeWorkflow<Lote, LoteStatus>
    {
        public FaseLoteEmAjustes()
        {
            this.FaseEstaAtiva = x => x.ValidacaoAtivo;
            this.StatusDaFase = LoteStatus.AguardandoAjustes;
            this.StatusSeFaseEstiverInativa = LoteStatus.PreparacaoAjustesConcluidos;
        }

        protected override void ProcessarFase(Lote lote)
        {
            var processo = lote.Processos.FirstOrDefault();
            
            if (processo == null || processo.Documentos.Any(x => x.Status == DocumentoStatus.TelaAjuste))
            {
                return;
            }

            if (processo.Documentos.Any(x => 
                x.TipoDocumento.Id == TipoDocumento.CodigoEmAjuste && x.Status != DocumentoStatus.Excluido) == false)
            {
                this.EnviarParaProximaFase(processo);
            }
        }

        private void EnviarParaProximaFase(Processo processo)
        {
            processo.HoraInicio = null;

            if (this.ExisteDocumentoNaoIdentificado(processo))
            {
                processo.Status = ProcessoStatus.AguardandoIdentificacao;
                processo.Lote.Status = LoteStatus.AguardandoIdentificacao;

                return;
            }

            processo.Status = ProcessoStatus.Montado;
            processo.Lote.Status = LoteStatus.SetaControleQualidadeM2;    
        }

        private bool ExisteDocumentoNaoIdentificado(Processo processo)
        {
            var documentosAtivos = processo.Documentos.Where(x => x.Status != DocumentoStatus.Excluido);

            return documentosAtivos.Any(
                x => x.TipoDocumento.Id == TipoDocumento.CodigoNaoIdentificado || 
                    x.TipoDocumento.Id == TipoDocumento.CodigoAguardandoNovoTipo);
        }
    }
}
