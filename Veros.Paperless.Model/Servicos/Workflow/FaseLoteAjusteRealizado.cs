namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System.Linq;
    using Entidades;

    public class FaseLoteAjusteRealizado : FaseDeWorkflow<Lote, LoteStatus>
    {
        public FaseLoteAjusteRealizado()
        {
            this.FaseEstaAtiva = x => x.ValidacaoAtivo;
            this.StatusDaFase = LoteStatus.AjustesConcluidos;
            this.StatusSeFaseEstiverInativa = LoteStatus.PreparacaoAjustesConcluidos;
        }

        protected override void ProcessarFase(Lote lote)
        {
            var processo = lote.Processos.First();

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
