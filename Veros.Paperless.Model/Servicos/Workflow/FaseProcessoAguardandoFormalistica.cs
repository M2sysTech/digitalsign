namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Entidades;
    using Repositorios;

    public class FaseProcessoAguardandoFormalistica : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        private readonly IRegraVioladaRepositorio regraVioladaRepositorio;
        private readonly IValidaRegraDeFaces validaRegraDeFaces;

        public FaseProcessoAguardandoFormalistica(
            IRegraVioladaRepositorio regraVioladaRepositorio,
            IValidaRegraDeFaces validaRegraDeFaces)
        {
            this.regraVioladaRepositorio = regraVioladaRepositorio;
            this.validaRegraDeFaces = validaRegraDeFaces;
            this.FaseEstaAtiva = x => x.FormalisticaAtiva;
            this.StatusDaFase = ProcessoStatus.AguardandoFormalistica;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.FormalisticaRealizada;
        }

        protected override void ProcessarFase(Processo processo)
        {
            this.validaRegraDeFaces.Validar(processo);

            ////if (processo.PossuiDocumentoDoTipo(TipoDocumento.CodigoNaoIdentificado))
            ////{
            ////    processo.Status = ProcessoStatus.AguardandoMontagem;
            ////    processo.Lote.Status = LoteStatus.AguardandoIdentificacao;
            ////    return;
            ////}

            if (this.regraVioladaRepositorio.ExisteRegraVioladaSemTratamento(
                processo.Id, Regra.FaseFormalistica) == false)
            {
                processo.Status = ProcessoStatus.FormalisticaRealizada;
            }
        }
    }
}