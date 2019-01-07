namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using Data.Hibernate;
    using Framework.Servicos;
    using Model.Entidades;
    using Model.Repositorios;
    using NHibernate.Transform;

    public class EmailPendenteRepositorio : Repositorio<EmailPendente>, IEmailPendenteRepositorio
    {
        private ISessaoDoUsuario sessaoDoUsuario;

        public EmailPendenteRepositorio(ISessaoDoUsuario sessaoDoUsuario)
        {
            this.sessaoDoUsuario = sessaoDoUsuario;
        }

        public IList<EmailPendente> ObterPendentesEnvio()
        {
            return this.Session.QueryOver<EmailPendente>()
                .Fetch(x => x.Lote).Eager
                .Fetch(x => x.Processo).Eager
                .Where(x => x.Status == EmailPendenteStatus.NaoEnviado)
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
        }

        public IList<EmailPendente> ObterPorProcessoETipo(int processoId, TipoNotificacao tipoNotificacao)
        {
            return this.Session.QueryOver<EmailPendente>()
                .Where(x => x.Processo.Id  == processoId)
                .And(x => x.TipoNotificacao == tipoNotificacao)
                .List();
        }

        public IList<EmailPendente> ObterPorLoteETipo(int loteId, TipoNotificacao tipoNotificacao)
        {
            return this.Session.QueryOver<EmailPendente>()
                .Where(x => x.Lote.Id  == loteId)
                .And(x => x.TipoNotificacao == tipoNotificacao)
                .And(x => x.Status == EmailPendenteStatus.NaoEnviado)
                .List();
        }

        public void AtualizaEnviadosPorLoteETipo(int loteId, TipoNotificacao tipoNotificacao)
        {
            this.Session
             .CreateQuery("update EmailPendente set Status = :status where Lote = :id and TipoNotificacao = :tipo")
             .SetParameter("id", loteId)
             .SetParameter("status", EmailPendenteStatus.Enviado)
             .SetParameter("tipo", tipoNotificacao)
             .ExecuteUpdate();
        }

        public void MarcarComoEnviado(EmailPendente emailPendente)
        {
            this.Session
             .CreateQuery("update EmailPendente set Status = :status where Id = :id")
             .SetParameter("id", emailPendente.Id)
             .SetParameter("status", EmailPendenteStatus.Enviado)
             .ExecuteUpdate();
        }

        public void ReagendarEnvio(EmailPendente emailPendente, int reagendarEm)
        {
            this.Session
              .CreateQuery("update EmailPendente set EnviaEm = :enviaEm where Id = :id")
              .SetParameter("id", emailPendente.Id)
              .SetParameter("enviaEm", reagendarEm)
              .ExecuteUpdate();
        }

        public void ReagendarEnvioPorLote(EmailPendente emailPendente, int reagendarEm)
        {
            this.Session
              .CreateQuery("update EmailPendente set EnviaEm = :enviaEm where Lote = :id and TipoNotificacao = :tipoAtual")
              .SetParameter("id", emailPendente.Lote.Id)
              .SetParameter("enviaEm", reagendarEm)
              .SetParameter("tipoAtual", emailPendente.TipoNotificacao)
              .ExecuteUpdate();
        }

        public void MudarStatusParaEnviarRegulacao(int loteId)
        {
            this.Session
              .CreateQuery(@"update EmailPendente 
                            set Status = :statusNovo 
                            where Lote = :loteId 
                            and Status = :statusAtual")

              .SetParameter("statusNovo", EmailPendenteStatus.NaoEnviado)
              .SetParameter("loteId", loteId)
              .SetParameter("statusAtual", EmailPendenteStatus.AguardandoTramitarRegulacao)
              .ExecuteUpdate();
        }

        public void MudarStatusParaEnviarCadastro(int loteId)
        {
            this.Session
              .CreateQuery(@"update EmailPendente 
                            set Status = :statusNovo 
                            where Lote = :loteId 
                            and Status = :statusAtual")

              .SetParameter("statusNovo", EmailPendenteStatus.NaoEnviado)
              .SetParameter("loteId", loteId)
              .SetParameter("statusAtual", EmailPendenteStatus.AguardandoTramitarCadastro)
              .ExecuteUpdate();
        }

        public void MudarStatusParaVerificado(int id)
        {
            this.Session
              .CreateQuery(@"update EmailPendente 
                            set Status = :statusNovo 
                            where Id = :id")
              .SetParameter("statusNovo", EmailPendenteStatus.Verificado)
              .SetParameter("id", id)
              .ExecuteUpdate();
        }

        public void AlterarStatusPorId(int id, string status)
        {
            this.Session
              .CreateQuery(@"update EmailPendente 
                            set Status = :statusNovo 
                            where Id = :id")
              .SetParameter("statusNovo", status)
              .SetParameter("id", id)
              .ExecuteUpdate();
        }

        public void AlterarTipoNotificacao(EmailPendente emailPendente, TipoNotificacao tipoNotificacao)
        {
            this.Session
              .CreateQuery(@"update EmailPendente set TipoNotificacao = :tipo where Id = :id")
              .SetParameter("tipo", tipoNotificacao)
              .SetParameter("id", emailPendente.Id)
              .ExecuteUpdate();
        }

        public void AlterarTipoNotificacaoPorLote(EmailPendente emailPendente, TipoNotificacao tipoNotificacao)
        {
            this.Session
              .CreateQuery(@"update EmailPendente set TipoNotificacao = :tipo where Lote = :id and TipoNotificacao = :tipoAtual")
              .SetParameter("tipo", tipoNotificacao)
              .SetParameter("id", emailPendente.Lote.Id)
              .SetParameter("tipoAtual", emailPendente.TipoNotificacao)
              .ExecuteUpdate();
        }

        public void AlterarContatoDoRemetente(int lote, string enderecoEmail)
        {
            this.Session
             .CreateQuery("update EmailPendente set Para = :para where Lote = :id and Status != 1")
             .SetParameter("id", lote)
             .SetParameter("para", enderecoEmail)
             .ExecuteUpdate();
        }

        public void CancelarEnvioMensagem(int processoId, TipoNotificacao tipoNotificacao)
        {
            this.Session
             .CreateQuery("update EmailPendente set Status = :StatusEmail where Processo = :id and TipoNotificacao = :tipoNotificacao")
             .SetParameter("StatusEmail", EmailPendenteStatus.Cancelado)
             .SetParameter("id", processoId)
             .SetParameter("tipoNotificacao", tipoNotificacao)
             .ExecuteUpdate();
        }
    }
}