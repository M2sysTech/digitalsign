namespace Veros.Paperless.Model.Servicos.Correspondencia
{
    using System.Collections.Generic;
    using Entidades;
    using Repositorios;

    public class MarcarEnviadosServicos : IMarcarEnviadosServicos
    {
        private readonly IEmailPendenteRepositorio emailPendenteRepositorio;

        private readonly List<TipoNotificacao> listaMarcarNotificacaoPorLoteETipo = new List<TipoNotificacao>
        {
        };

        private readonly List<TipoNotificacao> listaMarcarNotificacaoComoEnviado = new List<TipoNotificacao>
        {
            TipoNotificacao.AtendimentoConcluido
        };

        public MarcarEnviadosServicos(IEmailPendenteRepositorio emailPendenteRepositorio)
        {
            this.emailPendenteRepositorio = emailPendenteRepositorio;
        }

        public void ExecutarParaEmail(EmailPendente emailPendente)
        {
            if (emailPendente.Status != EmailPendenteStatus.Verificado)
            {
                return;
            }

            if (this.listaMarcarNotificacaoPorLoteETipo.Contains(emailPendente.TipoNotificacao))
            {
                this.emailPendenteRepositorio.AtualizaEnviadosPorLoteETipo(emailPendente.Lote.Id, emailPendente.TipoNotificacao);
            }
            else if (this.listaMarcarNotificacaoComoEnviado.Contains(emailPendente.TipoNotificacao))
            {
                this.emailPendenteRepositorio.MarcarComoEnviado(emailPendente);
            }
        }
    }
}
