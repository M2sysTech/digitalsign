namespace Veros.Paperless.Model.Servicos.Detalhe
{
    using Aprovacao;
    using Veros.Paperless.Model.Entidades;
    using Veros.Framework.Servicos;
    using Veros.Paperless.Model.Repositorios;

    public class EncerraAprovacaoServico : IEncerraAprovacaoServico
    {
        private readonly IGravaLogDoProcessoServico gravaLogProcessoServico;
        private readonly EstatisticaAprovacaoServico estatisticaAprovacaoServico;
        private readonly ISessaoDoUsuario userSession;
        private readonly IProcessoRepositorio processoRepositorio;

        public EncerraAprovacaoServico(
            IGravaLogDoProcessoServico gravaLogProcessoServico,
            EstatisticaAprovacaoServico estatisticaAprovacaoServico,
            ISessaoDoUsuario userSession,
            IProcessoRepositorio processoRepositorio)
        {
            this.gravaLogProcessoServico = gravaLogProcessoServico;
            this.estatisticaAprovacaoServico = estatisticaAprovacaoServico;
            this.userSession = userSession;
            this.processoRepositorio = processoRepositorio;
        }

        public void Executar(Processo processo)
        {
            this.processoRepositorio.LimparResponsavelEHoraInicio(processo.Id);
            this.gravaLogProcessoServico.Executar(LogProcesso.AcaoEncerrarNaAprovacao, processo, "Usuário encerrou o tratamento do processo");
            this.estatisticaAprovacaoServico.IncrementarAbandonadasParaHoje(this.userSession.UsuarioAtual.Id);
        }
    }
}