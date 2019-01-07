namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System;
    using System.Threading;
    using Entidades;
    using GrupoTipoDeDocumento;

    public class FaseLoteSetaQualidadeM2Sys : FaseDeWorkflow<Lote, LoteStatus>
    {
        private readonly ILogLoteServico logLoteServico;
        private readonly IAjustaGrupoTipoDeDocumentoServico ajustaGrupoTipoDeDocumentoServico;

        public FaseLoteSetaQualidadeM2Sys(ILogLoteServico logLoteServico, 
            IAjustaGrupoTipoDeDocumentoServico ajustaGrupoTipoDeDocumentoServico)
        {
            this.logLoteServico = logLoteServico;
            this.ajustaGrupoTipoDeDocumentoServico = ajustaGrupoTipoDeDocumentoServico;
            this.FaseEstaAtiva = x => x.ValidacaoAtivo;
            this.StatusDaFase = LoteStatus.SetaControleQualidadeM2;
            this.StatusSeFaseEstiverInativa = LoteStatus.SetaControleQualidadeCef;
        }

        protected override void ProcessarFase(Lote lote)
        {
            this.ajustaGrupoTipoDeDocumentoServico.Executar(lote);

            if (Contexto.QualidadePorcentagemm2Sys == 0)
            {
                lote.QualidadeM2sys = 0;
                lote.Status = LoteStatus.AguardandoGeracaoTermos;
                return;
            }

            var selecionarEmCada = Math.Truncate(100.0 / Contexto.QualidadePorcentagemm2Sys);

            var contagemLocal = Contexto.ContagemDossieQualidadeM2sys;
            if (contagemLocal >= selecionarEmCada)
            {
                Interlocked.Exchange(ref Contexto.ContagemDossieQualidadeM2sys, 1);
                lote.QualidadeM2sys = 1;
                this.logLoteServico.Gravar(lote.Id, LogLote.AcaoSetaQualidadeM2, "Setado para Qualidade M2sys (" + Contexto.QualidadePorcentagemm2Sys + "%)");
                lote.Status = LoteStatus.AguardandoControleQualidadeM2;

                foreach (var processo in lote.Processos)
                {
                    processo.Status = ProcessoStatus.AguardandoControleQualidadeM2;
                    processo.HoraInicio = null;
                    processo.HoraInicioAjuste = null;
                }
            }
            else
            {
                Interlocked.Increment(ref Contexto.ContagemDossieQualidadeM2sys);
                lote.Status = LoteStatus.AguardandoGeracaoTermos;
            }
        }
    }
}
