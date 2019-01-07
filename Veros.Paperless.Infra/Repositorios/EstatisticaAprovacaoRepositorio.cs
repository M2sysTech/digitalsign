namespace Veros.Paperless.Infra.Repositorios
{
    using System;
    using System.Collections.Generic;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Consultas;
    using Veros.Paperless.Model.Entidades;
    using NHibernate.Criterion;
    using NHibernate.Transform;
    using Veros.Paperless.Model.Repositorios;

    public class EstatisticaAprovacaoRepositorio : Repositorio<EstatisticaAprovacao>, IEstatisticaAprovacaoRepositorio
    {
        public override void Salvar(EstatisticaAprovacao item)
        {
            base.Salvar(item);
            this.Session.Flush();
        }

        public EstatisticaAprovacao ObterDeHojePorUsuario(int usuarioId)
        {
            //// TODO: mostrar pro felix como obter 1
            ////return this.Session.QueryOver<EstatisticaAprovacao>()
            ////   .Where(x => x.Usuario.Id == usuarioId)
            ////   .Where(x => x.DataRegistro == DateTime.Today)
            ////   .List<EstatisticaAprovacao>().FirstOrDefault();

            return this.Session.QueryOver<EstatisticaAprovacao>()
               .Where(x => x.Usuario.Id == usuarioId)
               .Where(x => x.DataRegistro == DateTime.Today)
               .Take(1)
               .SingleOrDefault();
        }

        public EstatisticaAprovacao ObterProducaoPorUsuarioAndData(int usuarioId, DateTime data)
        {
            return this.Session.QueryOver<EstatisticaAprovacao>()
                .Where(x => x.Usuario.Id == usuarioId)
                .And(x => x.DataRegistro == data)
                .Take(1)
                .SingleOrDefault();
        }

        public SomaDeEstatisticaDeAprovacaoConsulta ObterSomaPorUsuarioAndData(int usuarioId, DateTime data)
        {
            //// TODO: converter para HQL com 'select new'
            return this.Session.CreateCriteria(typeof(EstatisticaAprovacao))
                .Add(Restrictions.Eq("Usuario.Id", usuarioId))
                .Add(Restrictions.Eq("DataRegistro", data))
                .SetProjection(Projections.ProjectionList()
                                    .Add(Projections.Alias(Projections.GroupProperty("DataRegistro"), "DataRegistro"))
                                    .Add(Projections.Alias(Projections.Sum("TotalDevolvidas"), "TotalDevolvidas"))
                                    .Add(Projections.Alias(Projections.Sum("TotalLiberadas"), "TotalLiberadas"))
                                    .Add(Projections.Alias(Projections.Sum("TotalFraudes"), "TotalFraudes"))
                                    .Add(Projections.Alias(Projections.Sum("TotalDevolvidasComFraude"), "TotalDevolvidasComFraude"))
                                    .Add(Projections.Alias(Projections.Sum("TotalLiberadasComFraude"), "TotalLiberadasComFraude"))
                                    .Add(Projections.Alias(Projections.Sum("TotalAbandonadas"), "TotalAbandonadas")))
                .SetMaxResults(1)
                .SetResultTransformer(Transformers.AliasToBean(typeof(SomaDeEstatisticaDeAprovacaoConsulta)))
                .UniqueResult<SomaDeEstatisticaDeAprovacaoConsulta>();
        }

        /// <summary>
        /// TODO: mover para objeto de consulta
        /// </summary>
        public SomaDeEstatisticaDeAprovacaoConsulta ObterSomaPorData(DateTime data)
        {
            return this.Session.CreateCriteria(typeof(EstatisticaAprovacao))
                .Add(Restrictions.Eq("DataRegistro", data))
                .SetProjection(Projections.ProjectionList()
                                    .Add(Projections.Alias(Projections.GroupProperty("DataRegistro"), "DataRegistro"))
                                    .Add(Projections.Alias(Projections.Sum("TotalDevolvidas"), "TotalDevolvidas"))
                                    .Add(Projections.Alias(Projections.Sum("TotalLiberadas"), "TotalLiberadas"))
                                    .Add(Projections.Alias(Projections.Sum("TotalFraudes"), "TotalFraudes"))
                                    .Add(Projections.Alias(Projections.Sum("TotalDevolvidasComFraude"), "TotalDevolvidasComFraude"))
                                    .Add(Projections.Alias(Projections.Sum("TotalLiberadasComFraude"), "TotalLiberadasComFraude"))
                                    .Add(Projections.Alias(Projections.Sum("TotalAbandonadas"), "TotalAbandonadas")))
                .SetMaxResults(1)
                .SetResultTransformer(Transformers.AliasToBean(typeof(SomaDeEstatisticaDeAprovacaoConsulta)))
                .UniqueResult<SomaDeEstatisticaDeAprovacaoConsulta>();
        }

        public IList<SomaDeEstatisticaDeAprovacaoConsulta> ObterSomaPorPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            return this.Session.CreateCriteria(typeof(EstatisticaAprovacao))
                .Add(Restrictions.Between("DataRegistro", dataInicial, dataFinal))
                .SetProjection(Projections.ProjectionList()
                                    .Add(Projections.Alias(Projections.GroupProperty("DataRegistro"), "DataRegistro"))
                                    .Add(Projections.Alias(Projections.Sum("TotalDevolvidas"), "TotalDevolvidas"))
                                    .Add(Projections.Alias(Projections.Sum("TotalLiberadas"), "TotalLiberadas"))
                                    .Add(Projections.Alias(Projections.Sum("TotalFraudes"), "TotalFraudes"))
                                    .Add(Projections.Alias(Projections.Sum("TotalDevolvidasComFraude"), "TotalDevolvidasComFraude"))
                                    .Add(Projections.Alias(Projections.Sum("TotalLiberadasComFraude"), "TotalLiberadasComFraude"))
                                    .Add(Projections.Alias(Projections.Sum("TotalAbandonadas"), "TotalAbandonadas")))
                .SetResultTransformer(Transformers.AliasToBean(typeof(SomaDeEstatisticaDeAprovacaoConsulta)))
                .AddOrder(Order.Asc("DataRegistro")) 
                .List<SomaDeEstatisticaDeAprovacaoConsulta>();
        }

        public IList<SomaDeEstatisticaDeAprovacaoConsulta> ObterSomaPorUsuarioAndPeriodo(
            int usuarioId,
            DateTime dataInicial,
            DateTime dataFinal)
        {
            return this.Session.CreateCriteria(typeof(EstatisticaAprovacao))
                .Add(Restrictions.Eq("Usuario.Id", usuarioId))
                .Add(Restrictions.Between("DataRegistro", dataInicial, dataFinal))
                .SetProjection(Projections.ProjectionList()
                                    .Add(Projections.Alias(Projections.GroupProperty("DataRegistro"), "DataRegistro"))
                                    .Add(Projections.Alias(Projections.Sum("TotalDevolvidas"), "TotalDevolvidas"))
                                    .Add(Projections.Alias(Projections.Sum("TotalLiberadas"), "TotalLiberadas"))
                                    .Add(Projections.Alias(Projections.Sum("TotalFraudes"), "TotalFraudes"))
                                    .Add(Projections.Alias(Projections.Sum("TotalDevolvidasComFraude"), "TotalDevolvidasComFraude"))
                                    .Add(Projections.Alias(Projections.Sum("TotalLiberadasComFraude"), "TotalLiberadasComFraude"))
                                    .Add(Projections.Alias(Projections.Sum("TotalAbandonadas"), "TotalAbandonadas")))
                .SetResultTransformer(Transformers.AliasToBean(typeof(SomaDeEstatisticaDeAprovacaoConsulta)))
                .AddOrder(Order.Asc("DataRegistro"))
                .List<SomaDeEstatisticaDeAprovacaoConsulta>();
        }

        public void IncrementarDevolvidasParaHoje(int usuarioId)
        {
            var linhasAfetadas = this.Session
               .CreateQuery(@"
update EstatisticaAprovacao set 
TotalDevolvidas = TotalDevolvidas + 1 
where Usuario.Id = :usuarioId
and DataRegistro = trunc(current_date())
")
               .SetInt32("usuarioId", usuarioId)
               .ExecuteUpdate();

            if (linhasAfetadas <= 0)
            {
                this.IncluirEstatistica(usuarioId, totalDevolvidas: 1);
            }
        }

        public void IncrementarDevolvidasComFraudeParaHoje(int usuarioId)
        {
            var linhasAfetadas = this.Session
               .CreateQuery(@"
update EstatisticaAprovacao set 
TotalDevolvidasComFraude = TotalDevolvidasComFraude + 1 
where Usuario.Id = :usuarioId
and DataRegistro = trunc(current_date())
")
               .SetInt32("usuarioId", usuarioId)
               .ExecuteUpdate();

            if (linhasAfetadas <= 0)
            {
                this.IncluirEstatistica(usuarioId, totalDevolvidasComFraude: 1);
            }
        }

        public void IncrementarLiberadasParaHoje(int usuarioId)
        {
            var linhasAfetadas = this.Session
               .CreateQuery(@"
update EstatisticaAprovacao set 
TotalLiberadas = TotalLiberadas + 1 
where Usuario.Id = :usuarioId
and DataRegistro = trunc(current_date())
")
               .SetInt32("usuarioId", usuarioId)
               .ExecuteUpdate();

            if (linhasAfetadas <= 0)
            {
                this.IncluirEstatistica(usuarioId, totalLiberadas: 1);
            }
        }

        public void IncrementarLiberadasComFraudeParaHoje(int usuarioId)
        {
            var linhasAfetadas = this.Session
               .CreateQuery(@"
update EstatisticaAprovacao set 
TotalLiberadasComFraude = TotalLiberadasComFraude + 1 
where Usuario.Id = :usuarioId
and DataRegistro = trunc(current_date())
")
               .SetInt32("usuarioId", usuarioId)
               .ExecuteUpdate();

            if (linhasAfetadas <= 0)
            {
                this.IncluirEstatistica(usuarioId, totalLiberadasComFraude: 1);
            }
        }

        public void IncrementarAbandonadasParaHoje(int usuarioId)
        {
            var linhasAfetadas = this.Session
               .CreateQuery(@"
update EstatisticaAprovacao set 
TotalAbandonadas = TotalAbandonadas + 1 
where Usuario.Id = :usuarioId
and DataRegistro = trunc(current_date())
")
               .SetInt32("usuarioId", usuarioId)
               .ExecuteUpdate();

            if (linhasAfetadas <= 0)
            {
                this.IncluirEstatistica(usuarioId, totalAbandonadas: 1);
            }
        }

        public void IncrementarFraudesParaHoje(int usuarioId)
        {
            var linhasAfetadas = this.Session
               .CreateQuery(@"
update EstatisticaAprovacao set 
TotalFraudes = TotalFraudes + 1 
where Usuario.Id = :usuarioId
and DataRegistro = trunc(current_date())
")
               .SetInt32("usuarioId", usuarioId)
               .ExecuteUpdate();

            if (linhasAfetadas <= 0)
            {
                this.IncluirEstatistica(usuarioId, totalFraudes: 1);
            }
        }

        public IList<EstatisticaAprovacao> ObterPorPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            return this.Session.QueryOver<EstatisticaAprovacao>()
                .Fetch(x => x.Usuario).Eager
                .Where(x => x.DataRegistro >= dataInicio)
                .Where(x => x.DataRegistro <= dataFim)
                .List<EstatisticaAprovacao>();
        }

        public IList<EstatisticaAprovacao> ObterPorPeriodoEUsuario(DateTime dataInicio, DateTime dataFim, int usuarioId)
        {
            return this.Session.QueryOver<EstatisticaAprovacao>()
                .Fetch(x => x.Usuario).Eager
                .Where(x => x.DataRegistro >= dataInicio)
                .Where(x => x.DataRegistro <= dataFim)
                .Where(x => x.Usuario.Id == usuarioId)
                .List<EstatisticaAprovacao>();
        }

        private void IncluirEstatistica(int usuarioId, 
                                        int totalDevolvidas = 0, 
                                        int totalLiberadas = 0, 
                                        int totalFraudes = 0, 
                                        int totalAbandonadas = 0, 
                                        int totalDevolvidasComFraude = 0, 
                                        int totalLiberadasComFraude = 0)
        {
            var estatisticaAprovacao = new EstatisticaAprovacao
            {
                TotalDevolvidas = totalDevolvidas,
                TotalLiberadas = totalLiberadas,
                TotalFraudes = totalFraudes,
                TotalAbandonadas = totalAbandonadas,
                Usuario = new Usuario { Id = usuarioId },
                TotalDevolvidasComFraude = totalDevolvidasComFraude,
                TotalLiberadasComFraude = totalLiberadasComFraude
            };

            this.Salvar(estatisticaAprovacao);
        }
    }
}