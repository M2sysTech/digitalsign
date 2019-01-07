namespace Veros.Paperless.Model.Servicos.Detalhe
{
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;
    using Veros.Framework.Servicos;
    using Veros.Paperless.Model.Repositorios;

    public class LiberaAprovacaoServico : ILiberaAprovacaoServico
    {
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly IRegraVioladaRepositorio regraVioladaRepositorio;
        private readonly IGravaLogDoProcessoServico gravaLogProcessoServico;
        private readonly IEstatisticaAprovacaoRepositorio estatisticaAprovacaoRepositorio;
        private readonly ISessaoDoUsuario userSession;
        private readonly EstatisticaAprovacaoServico estatisticaAprovacaoServico;
        private readonly IGravaLogDoLoteServico gravaLogLoteServico;

        public LiberaAprovacaoServico(
            IProcessoRepositorio processoRepositorio,
            IRegraVioladaRepositorio regraVioladaRepositorio,
            IGravaLogDoProcessoServico gravaLogProcessoServico,
            IEstatisticaAprovacaoRepositorio estatisticaAprovacaoRepositorio,
            ISessaoDoUsuario userSession, 
            EstatisticaAprovacaoServico estatisticaAprovacaoServico, 
            IGravaLogDoLoteServico gravaLogLoteServico)
        {
            this.processoRepositorio = processoRepositorio;
            this.regraVioladaRepositorio = regraVioladaRepositorio;
            this.gravaLogProcessoServico = gravaLogProcessoServico;
            this.estatisticaAprovacaoRepositorio = estatisticaAprovacaoRepositorio;
            this.userSession = userSession;
            this.estatisticaAprovacaoServico = estatisticaAprovacaoServico;
            this.gravaLogLoteServico = gravaLogLoteServico;
        }

        public void Executar(
            Processo processo, 
            IList<RegraViolada> regrasVioladasAprovadas)
        {
            if (this.processoRepositorio.EstaAguardandoAprovacao(processo.Id) == false)
            {
                return;
            }

            this.GravaLogDeRegrasDesmarcadas(regrasVioladasAprovadas, processo.Id);

            this.AlterarRegrasVioladas(regrasVioladasAprovadas);

            this.processoRepositorio.GravarAprovacao(processo.Id);

            this.gravaLogProcessoServico.Executar(
                LogProcesso.AcaoLiberarNaAprovacao,
                processo,
                "Processo liberado na fase de aprovação");
            
            this.estatisticaAprovacaoServico.IncrementarLiberadasParaHoje(this.userSession.UsuarioAtual, processo.Id);
        }

        private void GravaLogDeRegrasDesmarcadas(IEnumerable<RegraViolada> regrasVioladasAprovadas, int processoId)
        {
            foreach (var regraViolada in regrasVioladasAprovadas)
            {
                if (regraViolada.Status == RegraVioladaStatus.Marcada || regraViolada.Status == RegraVioladaStatus.Pendente)
                {
                    this.gravaLogLoteServico.Executar(LogLote.AcaoLoteLiberadoNaAprovacao, this.processoRepositorio.ObterLotePorProcessoId(processoId).Id, string.Format("LIBER. - Regra [{0}]: {1} - Fase: [Aprovação ]", regraViolada.Regra.Identificador, regraViolada.Regra.Descricao));
                }
            }
        }

        private void AlterarRegrasVioladas(IEnumerable<RegraViolada> regrasVioladasAprovadas)
        {
            foreach (var regraViolada in regrasVioladasAprovadas)
            {
                this.regraVioladaRepositorio.AlterarStatus(
                    regraViolada.Id,
                    RegraVioladaStatus.Aprovada);
            }
        }
    }
}