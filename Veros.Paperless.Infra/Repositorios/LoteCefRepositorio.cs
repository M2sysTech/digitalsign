namespace Veros.Paperless.Infra.Repositorios
{
    using System;
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class LoteCefRepositorio : Repositorio<LoteCef>, ILoteCefRepositorio
    {
        public IList<LoteCef> ObterAbertos(ConfiguracaoDeLoteCef configuracao)
        {
            return this.Session.QueryOver<LoteCef>()
                .Where(x => x.Status == LoteCefStatus.Aberto)
                .List();
        }

        public IList<LoteCef> ObterAbertos()
        {
            return this.Session.QueryOver<LoteCef>()
                .Where(x => x.Status == LoteCefStatus.Aberto)
                .List();
        }

        public IList<LoteCef> ObterAguardandoNovaAmostra()
        {
            return this.Session.QueryOver<LoteCef>()
                .Where(x => x.Status == LoteCefStatus.ReprovadoAguardandoNovaAmostra)
                .List();
        }

        public IList<LoteCef> ObterTodosMenosAbertos()
        {
            return this.Session.QueryOver<LoteCef>()
                .Where(x => x.Status != LoteCefStatus.Aberto)
                .Where(x => x.DataFim != null)
                .List();
        }

        public void Finalizar(LoteCef loteCef)
        {
            this.Session
              .CreateQuery("update LoteCef set Status = :status, DataFim = :dataFim, Visivel = :visivel where Id = :id")
              .SetParameter("status", LoteCefStatus.Fechado)
              .SetParameter("dataFim", DateTime.Now)
              .SetParameter("visivel", true)
              .SetParameter("id", loteCef.Id)
              .ExecuteUpdate();
        }

        public void Finalizar(int loteCefId, int quantidade)
        {
            this.Session
              .CreateQuery(@"
update LoteCef set 
    Status = :status, 
    DataFim = :dataFim, 
    Visivel = :visivel,
    QuantidadeDeLotes = :quantidade
where Id = :id")
              .SetParameter("status", LoteCefStatus.Fechado)
              .SetParameter("dataFim", DateTime.Now)
              .SetParameter("visivel", true)
              .SetParameter("quantidade", quantidade)
              .SetParameter("id", loteCefId)
              .ExecuteUpdate();
        }

        public void RedisponibilizarParaQualicef(int loteCefId)
        {
            this.Session
              .CreateQuery(@"
update LoteCef set 
    Status = :status,
    DataAprovacao = null
where Id = :id")
              .SetParameter("status", LoteCefStatus.Fechado)
              .SetParameter("id", loteCefId)
              .ExecuteUpdate();
        }

        public void AdicionarLote(LoteCef loteCef)
        {
            this.Session
             .CreateQuery("update LoteCef set QuantidadeDeLotes = QuantidadeDeLotes + 1 where Id = :id")
             .SetParameter("id", loteCef.Id)
             .ExecuteUpdate();
        }

        public void AlterarQuantidade(int loteCefId, int quantidade)
        {
            this.Session
             .CreateQuery("update LoteCef set QuantidadeDeLotes = :quantidade where Id = :id")
             .SetParameter("quantidade", quantidade)
             .SetParameter("id", loteCefId)
             .ExecuteUpdate();
        }

        public void AlterarStatus(int loteCefId, LoteCefStatus status)
        {
            this.Session
             .CreateQuery("update LoteCef set Status = :status where Id = :id")
             .SetParameter("status", status)
             .SetParameter("id", loteCefId)
             .ExecuteUpdate();
        }

        public void Aprovar(int loteCefId, int usuarioId)
        {
            this.Session
             .CreateQuery("update LoteCef set Status = :status, UsuarioAprovou = :usuarioAprovou, DataAprovacao = :dataAprovacao where Id = :id")
             .SetParameter("status", LoteCefStatus.AprovadoNaQualidade)
             .SetParameter("usuarioAprovou", new Usuario { Id = usuarioId })
             .SetParameter("dataAprovacao", DateTime.Now)
             .SetParameter("id", loteCefId)
             .ExecuteUpdate();
        }

        public void Recusar(int loteCefId, int usuarioId)
        {
            this.Session
             .CreateQuery("update LoteCef set Status = :status, UsuarioAprovou = :usuarioAprovou, DataAprovacao = :dataAprovacao where Id = :id")
             .SetParameter("status", LoteCefStatus.ReprovadoNaQualidade)
             .SetParameter("usuarioAprovou", new Usuario { Id = usuarioId })
             .SetParameter("dataAprovacao", DateTime.Now)
             .SetParameter("id", loteCefId)
             .ExecuteUpdate();
        }

        public void AtualizarGeracaoPdf(int lotecefId, int usuarioId)
        {
            this.Session
             .CreateQuery("update LoteCef set DataGeracaoCertificado = :dataGeracaoCertificado, UsuarioGerou = :usuarioId, DataAssinaturaCertificado = null where Id = :lotecefId")
             .SetParameter("dataGeracaoCertificado", DateTime.Now)
             .SetParameter("usuarioId", new Usuario { Id = usuarioId })
             .SetParameter("lotecefId", lotecefId)
             .ExecuteUpdate();
        }

        public IList<LoteCef> ObterReprovados()
        {
            return this.Session.QueryOver<LoteCef>()
               .Where(x => x.Status == LoteCefStatus.ReprovadoNaQualidade)
               .List();
        }
    }
}