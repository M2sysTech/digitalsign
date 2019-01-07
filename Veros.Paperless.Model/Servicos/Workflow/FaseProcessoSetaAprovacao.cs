namespace Veros.Paperless.Model.Servicos.Workflow
{
    using EngineDeRegras;
    using Entidades;
    using Repositorios;

    public class FaseProcessoSetaAprovacao : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        private readonly IRegraVioladaRepositorio regraVioladaRepositorio;
        private readonly IExecutorDeRegra executorDeRegra;
        private readonly IValidaRegraDeListaNegra validaRegraDeListaNegra;

        public FaseProcessoSetaAprovacao(
            IRegraVioladaRepositorio regraVioladaRepositorio, 
            IExecutorDeRegra executorDeRegra,
            IValidaRegraDeListaNegra validaRegraDeListaNegra)
        {
            this.regraVioladaRepositorio = regraVioladaRepositorio;
            this.executorDeRegra = executorDeRegra;
            this.validaRegraDeListaNegra = validaRegraDeListaNegra;

            this.FaseEstaAtiva = x => x.AprovacaoAtivo;
            this.StatusDaFase = ProcessoStatus.SetaAprovacao;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.Aprovado;
        }

        protected override void ProcessarFase(Processo processo)
        {
            this.validaRegraDeListaNegra.Validar(processo);

            this.executorDeRegra.ExistemRegrasAtendidas(Regra.FaseAprovacao, processo);
            this.regraVioladaRepositorio.ExisteRegraVioladaSemTratamento(processo.Id, Regra.FaseAprovacao);

            ////Não deve parar na aprovação
            processo.Status = ProcessoStatus.Aprovado;
            processo.AlterarStatusDosDocumentos(DocumentoStatus.Aprovado);
        }   
    }
}