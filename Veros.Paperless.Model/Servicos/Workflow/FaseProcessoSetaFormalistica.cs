namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System;
    using System.Linq;
    using EngineDeRegras;
    using Entidades;
    using Repositorios;

    public class FaseProcessoSetaFormalistica : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        private readonly IExecutorDeRegra executorDeRegra;
        private readonly IRegraVioladaRepositorio regraVioladaRepositorio;
        private readonly IRegraRepositorio regraRepositorio;
        private readonly IComparaBioRepositorio comparaBioRepositorio;

        public FaseProcessoSetaFormalistica(
            IExecutorDeRegra executorDeRegra,
            IRegraVioladaRepositorio regraVioladaRepositorio,
            IRegraRepositorio regraRepositorio,
            IComparaBioRepositorio comparaBioRepositorio)
        {
            this.executorDeRegra = executorDeRegra;
            this.regraVioladaRepositorio = regraVioladaRepositorio;
            this.regraRepositorio = regraRepositorio;
            this.comparaBioRepositorio = comparaBioRepositorio;

            this.FaseEstaAtiva = x => x.FormalisticaAtiva;
            this.StatusDaFase = ProcessoStatus.SetaFormalistica;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.FormalisticaRealizada;
        }

        protected override void ProcessarFase(Processo processo)
        {
            var existeRegraViolada = this.CriarRegraDeVerificacacaoDeFace(processo);

            if (this.executorDeRegra.ExistemRegrasAtendidas(Regra.FaseFormalistica, processo))
            {
                existeRegraViolada = true;
            }

            if (existeRegraViolada)
            {
                processo.Status = ProcessoStatus.AguardandoFormalistica;
                processo.AlterarStatusDosDocumentos(DocumentoStatus.AguardandoFormalistica);
            }
            else
            {
                processo.Status = ProcessoStatus.FormalisticaRealizada;
            }
        }

        private bool CriarRegraDeVerificacacaoDeFace(Processo processo)
        {
            if (processo.RegrasVioladas.Any(x => x.Id == Regra.CodigoRegraVerificacaoDeFace))
            {
                return true;
            }

            var regra = this.regraRepositorio.ObterPorId(Regra.CodigoRegraVerificacaoDeFace);

            if (regra == null || regra.Ativada != "S")
            {
                return false;
            }

            var comparacoesPendentes = this.comparaBioRepositorio.ObterPendentesPorProcesso(processo.Id);

            if (comparacoesPendentes.Any() == false)
            {
                return false;
            }

            var regraViolada = new RegraViolada
            {
                Processo = processo,
                Regra = regra,
                Status = RegraVioladaStatus.Pendente
            };

            this.regraVioladaRepositorio.Salvar(regraViolada);
            return true;
        }
    }
}