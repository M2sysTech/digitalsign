namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IEmailPendenteRepositorio : IRepositorio<EmailPendente>
    {
        IList<EmailPendente> ObterPendentesEnvio();

        IList<EmailPendente> ObterPorProcessoETipo(int processoId, TipoNotificacao tipoNotificacao);

        IList<EmailPendente> ObterPorLoteETipo(int loteId, TipoNotificacao tipoNotificacao);

        void AtualizaEnviadosPorLoteETipo(int loteId, TipoNotificacao tipoNotificacao);

        void MarcarComoEnviado(EmailPendente emailPendente);

        void ReagendarEnvio(EmailPendente emailPendente, int reagendarEm);

        void ReagendarEnvioPorLote(EmailPendente emailPendente, int reagendarEm);

        void MudarStatusParaEnviarRegulacao(int loteId);

        void MudarStatusParaEnviarCadastro(int loteId);

        void MudarStatusParaVerificado(int id);

        void AlterarStatusPorId(int id, string status);

        void AlterarTipoNotificacao(EmailPendente emailPendente, TipoNotificacao tipoNotificacao);

        void AlterarTipoNotificacaoPorLote(EmailPendente emailPendente, TipoNotificacao tipoNotificacao);

        void AlterarContatoDoRemetente(int lote, string enderecoEmail);

        void CancelarEnvioMensagem(int processoId, TipoNotificacao tipoNotificacao);
    }
}