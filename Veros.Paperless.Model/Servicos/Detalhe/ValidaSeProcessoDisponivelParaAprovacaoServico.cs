namespace Veros.Paperless.Model.Servicos.Aprovacao
{
    using System;
    using Entidades;
    using Framework.Servicos;

    public class ValidaSeProcessoDisponivelParaAprovacaoServico : IValidaSeProcessoDisponivelParaAprovacaoServico
    {
        private readonly ISessaoDoUsuario userSession;

        public ValidaSeProcessoDisponivelParaAprovacaoServico(
            ISessaoDoUsuario userSession)
        {
            this.userSession = userSession;
        }

        public bool Validar(Processo processo)
        {
            return this.ValidarStatus(processo)
                && (this.LiberadoPorHorario(processo) || this.LiberadoPorUsuario(processo));
        }

        public bool ValidarStatus(Processo processo)
        {
            return processo.Status == ProcessoStatus.AguardandoAprovacao || processo.Status == ProcessoStatus.AguardandoAprovacaoEspecial;
        }

        private bool LiberadoPorHorario(Processo processo)
        {
            return processo.HoraInicio == null || processo.HoraInicio < DateTime.Now.AddMinutes(-5);
        }

        private bool LiberadoPorUsuario(Processo processo)
        {
            return processo.UsuarioResponsavel == null || processo.UsuarioResponsavel == this.userSession.UsuarioAtual;
        }
    }
}