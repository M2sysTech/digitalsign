namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System;
    using DefineLoteCef;
    using Entidades;
    using Framework;

    public class FaseLoteSetaQualidadeCef : FaseDeWorkflow<Lote, LoteStatus>
    {
        private readonly ILogLoteServico logLoteServico;
        private readonly IDevePassarPelaQualidadeCef devePassarPelaQualidadeCef;
        private readonly IDefineLoteCefServico defineLoteCefServico;

        public FaseLoteSetaQualidadeCef(
            ILogLoteServico logLoteServico, 
            IDevePassarPelaQualidadeCef devePassarPelaQualidadeCef, 
            IDefineLoteCefServico defineLoteCefServico)
        {
            this.logLoteServico = logLoteServico;
            this.devePassarPelaQualidadeCef = devePassarPelaQualidadeCef;
            this.defineLoteCefServico = defineLoteCefServico;
            this.FaseEstaAtiva = x => x.ValidacaoAtivo;
            this.StatusDaFase = LoteStatus.SetaControleQualidadeCef;
            this.StatusSeFaseEstiverInativa = LoteStatus.Faturamento;
        }

        protected override void ProcessarFase(Lote lote)
        {
            this.defineLoteCefServico.Executar(lote);

            if (lote.LoteCef == null)
            {
                Log.Application.ErrorFormat("Erro ao definir lote CEF para o lote #" + lote.Id);
                return;
            }

            if (Contexto.QualidadePorcentagemCef == 0)
            {
                lote.QualidadeM2sys = 0;
                lote.DataAguardandoAprovacao = DateTime.Now;
                lote.Status = LoteStatus.ControleQualidadeCefRealizado;
                return;
            }

            //// se lote ja foi setado anteriormente, voltou pra ajuste e esta aqui de novo, não conta nada, só joga pra cef
            if (lote.QualidadeCef >= 1)
            {
                this.AlteraStatusParaQualiCef(lote);
                return;
            }

            if (this.devePassarPelaQualidadeCef.Validar(lote))
            {
                //// ao resetar, ja soma a qtd de dossies que NÃO foram selecionados anteriormente, por causa da marca de Qualidade M2
                Contexto.ContagemDossieQualidadeCef[lote.LoteCef.Id] = 1 + Contexto.OverheadDossieQualidadeCef[lote.LoteCef.Id];
                Contexto.OverheadDossieQualidadeCef[lote.LoteCef.Id] = 0;

                lote.QualidadeCef = 1;
                this.AlteraStatusParaQualiCef(lote);
            }
            else
            {
                Contexto.ContagemDossieQualidadeCef[lote.LoteCef.Id]++;
                lote.DataAguardandoAprovacao = DateTime.Now;
                lote.Status = LoteStatus.ControleQualidadeCefRealizado;
            }
        }

        private void AlteraStatusParaQualiCef(Lote lote)
        {
            this.logLoteServico.Gravar(lote.Id, LogLote.AcaoSetaQualidadeM2, "Setado para Qualidade CEF (" + Contexto.QualidadePorcentagemCef + "%)");
            lote.DataAguardandoAprovacao = DateTime.Now;
            lote.Status = LoteStatus.AguardandoControleQualidadeCef;
            lote.ResultadoQualidadeCef = string.Empty;

            foreach (var processo in lote.Processos)
            {
                processo.UsuarioResponsavel = null;
                processo.HoraInicio = null;
                processo.HoraInicioAjuste = null;
                processo.Status = ProcessoStatus.AguardandoControleQualidadeCef;
            }
        }
    }
}
