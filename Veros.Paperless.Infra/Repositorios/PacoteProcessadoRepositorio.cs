namespace Veros.Paperless.Infra.Repositorios
{
    using System;
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;
    using NHibernate.Criterion;
    using NHibernate.Transform;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class PacoteProcessadoRepositorio : Repositorio<PacoteProcessado>, IPacoteProcessadoRepositorio
    {
        public PacoteProcessado ObterPorPacote(string pacote)
        {
            return this.Session.QueryOver<PacoteProcessado>()
                .Where(x => x.Arquivo == pacote)
                .OrderBy(x => x.Id).Desc
                .SingleOrDefault();
        }

        public PacoteProcessado ObterUltimoArquivoRecebido()
        {
            return this.Session.QueryOver<PacoteProcessado>()
            .OrderBy(x => x.Id).Desc
            .Take(1)
            .SingleOrDefault();
        }

        public IList<PacoteProcessado> ObterPendentesDeImportacao()
        {
            return this.Session.QueryOver<PacoteProcessado>()
                .Where(x => x.ArquivoImportadoEm == null)
                .Where(x => x.StatusPacote != StatusPacote.Recepcionando)
                .OrderBy(x => x.Id).Asc
                .List();
        }

        public IList<AcompanhamentoProducaoConsulta> ObterAcompanhamentoProducaoPorData(
            DateTime data,
            int tipoDeProcesso)
        {
            ////TODO: Verificar uma forma melhor de filtrar por tipoDeProcesso
            return this.Session.CreateCriteria(typeof(Processo))
                .CreateAlias("Lote", "Lote")
                .CreateAlias("Lote.PacoteProcessado", "PP")
                .Add(Restrictions.Between("PP.ArquivoRecebidoEm", data.Date, data.Date.AddDays(1)))
                .Add(tipoDeProcesso > 0 ? Restrictions.Eq("TipoDeProcesso.Id", tipoDeProcesso) : Restrictions.IsNotNull("TipoDeProcesso"))
                .SetProjection(Projections.ProjectionList()
                                    .Add(Projections.Alias(Projections.GroupProperty("PP.Id"), "PacoteId"))
                                    .Add(Projections.Alias(Projections.GroupProperty("PP.Arquivo"), "NomeArquivo"))
                                    .Add(Projections.Alias(Projections.Max("PP.ArquivoRecebidoEm"), "DataRecepcao"))
                                    .Add(Projections.Alias(Projections.Max("PP.ArquivoImportadoEm"), "DataImportacao"))
                                    .Add(Projections.Alias(Projections.Max("Lote.DataFimIcr"), "DataValidacao"))
                                    .Add(Projections.Alias(Projections.Max("Lote.DataFimProvaZero"), "DataProvaZero"))
                                    .Add(Projections.Alias(Projections.Max("Lote.DataFimFormalistica"), "DataFormalistica"))
                                    .Add(Projections.Alias(Projections.Max("Lote.DataAguardandoAprovacao"), "DataAprovacao"))
                                    .Add(Projections.Alias(Projections.Max("Lote.DataFimExportacao"), "DataExportacao")))
                .SetResultTransformer(Transformers.AliasToBean(typeof(AcompanhamentoProducaoConsulta)))
                .AddOrder(Order.Asc("PacoteId"))
                .List<AcompanhamentoProducaoConsulta>();
        }

        public IList<PacoteProcessado> ObterPorPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            var dataFinalFormatada = dataFinal.AddDays(1);
            return this.Session.CreateCriteria(typeof(PacoteProcessado))
               .Add(Restrictions.Between("ArquivoRecebidoEm", dataInicial, dataFinalFormatada))
               .AddOrder(Order.Asc("ArquivoRecebidoEm"))
               .List<PacoteProcessado>();
        }

        public IList<PacoteProcessado> ObterTodosOsPacotesPendentesDeFaturamento()
        {
            return this.Session.QueryOver<PacoteProcessado>()
                .Where(x => x.StatusPacote == StatusPacote.Pendente)
                .List();
        }

        public IList<PacoteProcessado> ObterPendenciasDeFaturamentoPorPacoteParaDataReferencia(DateTime dataParaFaturamento)
        {
            var dataParaFaturamentoLimite = dataParaFaturamento.AddDays(1);
            return this.Session.QueryOver<PacoteProcessado>()
                .Where(x => x.ArquivoRecebidoEm > dataParaFaturamento)
                .And(x => x.ArquivoRecebidoEm < dataParaFaturamentoLimite)
                .And(x => x.StatusPacote == StatusPacote.Pendente)
                .List();
        }

        public IList<PacoteProcessado> ObterPendenciasDeFaturamentoPorPacote()
        {
            return this.Session.QueryOver<PacoteProcessado>()
                .WhereNot(x => x.StatusPacote == StatusPacote.Processado)
                .List();
        }

        public DateTime? ObterDataParaExpurgo(int ultimosDias)
        {
            const string Hql = @"
select 
    to_char(p.ArquivoRecebidoEm, 'yyyy-mm-dd')
from 
    PacoteProcessado p
where
    p.StatusPacote = :status
group by
    to_char(p.ArquivoRecebidoEm, 'yyyy-mm-dd')
order by
    to_char(p.ArquivoRecebidoEm, 'yyyy-mm-dd') desc";

            var retorno = this.Session.CreateQuery(Hql)
                .SetParameter("status", StatusPacote.Processado)
                .SetFirstResult(ultimosDias)
                .SetMaxResults(1)
                .UniqueResult<string>();

            DateTime data;

            if (DateTime.TryParse(retorno, out data))
            {
                return data;
            }

            return null;
        }

        public void AtualizarStatus(int id, StatusPacote statusPacote)
        {
            this.Session
               .CreateQuery("update PacoteProcessado set StatusPacote = :status where Id = :id")
               .SetInt32("id", id)
               .SetParameter("status", statusPacote)
               .ExecuteUpdate();
        }

        public void AlterarPacoteParaFaturado(int id)
        {
            this.Session.CreateQuery("update PacoteProcessado set Faturado =:faturado where Id = :id")
                .SetInt32("id", id)
                .SetParameter("faturado", PacoteProcessadoFaturado.Faturado)
                .ExecuteUpdate();
        }

        public IList<AcompanhamentoProducaoM2Consulta> ObterAcompanhamentoProducaoM2PorData(DateTime dataRecebimento, int tipoProcessoId)
        {
            return this.Session.CreateCriteria(typeof(Processo))
                .CreateAlias("Lote", "Lote")
                .CreateAlias("Lote.PacoteProcessado", "PP")
                .Add(Restrictions.Between("PP.ArquivoRecebidoEm", dataRecebimento.Date, dataRecebimento.Date.AddDays(1)))
                .Add(tipoProcessoId > 0 ? Restrictions.Eq("TipoDeProcesso.Id", tipoProcessoId) : Restrictions.IsNotNull("TipoDeProcesso"))
                .SetProjection(Projections.ProjectionList()
                                    .Add(Projections.Alias(Projections.GroupProperty("PP.Id"), "PacoteId"))
                                    .Add(Projections.Alias(Projections.GroupProperty("PP.Arquivo"), "NomeArquivo"))
                                    .Add(Projections.Alias(Projections.Max("PP.ArquivoRecebidoEm"), "DataRecepcao"))
                                    .Add(Projections.Alias(Projections.Max("PP.ArquivoImportadoEm"), "DataImportacao"))
                                    .Add(Projections.Alias(Projections.Max("Lote.DataFimIcr"), "DataOcr"))
                                    .Add(Projections.Alias(Projections.Max("Lote.DataFimIdentificacao"), "DataIdentificacao"))
                                    .Add(Projections.Alias(Projections.Max("Lote.DataFimDaMontagem"), "DataMontagem"))
                                    .Add(Projections.Alias(Projections.Max("Lote.DataFimDigitacao"), "DataDigitacao"))
                                    .Add(Projections.Alias(Projections.Max("Lote.DataFimValidacao"), "DataValidacao"))
                                    .Add(Projections.Alias(Projections.Max("Lote.DataFimProvaZero"), "DataProvaZero"))
                                    .Add(Projections.Alias(Projections.Max("Lote.DataFimFormalistica"), "DataFormalistica"))
                                    .Add(Projections.Alias(Projections.Max("Lote.DataAguardandoAprovacao"), "DataAprovacao"))
                                    .Add(Projections.Alias(Projections.Max("Lote.DataFimExportacao"), "DataExportacao"))
                                    .Add(Projections.Alias(Projections.Max("Lote.DataFinalizacao"), "DataFim")))
                .SetResultTransformer(Transformers.AliasToBean(typeof(AcompanhamentoProducaoM2Consulta)))
                .AddOrder(Order.Asc("PacoteId"))
                .List<AcompanhamentoProducaoM2Consulta>();
        }

        public void GravarFimRecepcao(int pacoteProcessadoId)
        {
            this.Session.CreateQuery("update PacoteProcessado set FimRecepcao = :data where Id = :id")
                .SetParameter("id", pacoteProcessadoId)
                .SetParameter("data", DateTime.Now)
                .ExecuteUpdate();
        }

        public IList<PacoteProcessado> ObterPacotesParaExpurgo(int intervaloDeDias)
        {
            var dataLimite = DateTime.Today.AddDays(intervaloDeDias * -1);

            return this.Session.QueryOver<PacoteProcessado>()
                .Where(x => x.ArquivoRecebidoEm <= dataLimite)
                .And(x => x.StatusPacote == StatusPacote.Cancelado || x.StatusPacote == StatusPacote.Processado)
                .List();
        }

        public PacoteProcessado ObterPacoteDoDia()
        {
            return this.Session.QueryOver<PacoteProcessado>()
                .Where(x => x.ArquivoRecebidoEm >= DateTime.Today)
                .Take(1)
                .SingleOrDefault();
        }

        public Pacote ObterPacotePorBarcodeCaixa(string barcodeCaixa)
        {
            return this.Session.QueryOver<Pacote>()
                .Where(x => x.Identificacao == barcodeCaixa)
                .Take(1)
                .SingleOrDefault();
        }

        public IList<PacoteProcessado> ObterPendentes()
        {
            return this.Session.QueryOver<PacoteProcessado>()
                .Where(x => x.StatusPacote == StatusPacote.EmProcessamento)
                .List();
        }

        public IList<PacoteProcessado> ObterAprovadosCef()
        {
            return this.Session.QueryOver<PacoteProcessado>()
                .Where(x => x.StatusPacote == StatusPacote.AprovadoNaQualidade)
                .Fetch(x => x.Lotes).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
        }

        public void GravarFimControleDeQualidade(int pacoteProcessadoId, StatusPacote novoStatus)
        {
            this.Session
               .CreateQuery("update PacoteProcessado set StatusPacote = :novoStatus, FimRecepcao = sysdate where Id = :id")
               .SetParameter("novoStatus", novoStatus)
               .SetParameter("id", pacoteProcessadoId)
               .ExecuteUpdate();
        }

        public void AlterarStatus(int pacoteProcessadoId, StatusPacote novoStatus)
        {
            this.Session
               .CreateQuery("update PacoteProcessado set StatusPacote = :novoStatus where Id = :id")
               .SetParameter("novoStatus", novoStatus)
               .SetParameter("id", pacoteProcessadoId)
               .ExecuteUpdate();
        }

        public void AlterarFlagAtivado(int pacoteProcessadoId, string situacao)
        {
            this.Session
               .CreateQuery("update PacoteProcessado set Ativado = :situacao where Id = :id")
               .SetParameter("situacao", situacao)
               .SetParameter("id", pacoteProcessadoId)
               .ExecuteUpdate();
        }

        public void AlterarFlagExibeNaQualidadeCef(int pacoteProcessadoId, string novaSituacao)
        {
            this.Session
               .CreateQuery("update PacoteProcessado set ExibirNaQualidadeCef = :exibirNaQualidadeCef where Id = :id")
               .SetParameter("exibirNaQualidadeCef", novaSituacao)
               .SetParameter("id", pacoteProcessadoId)
               .ExecuteUpdate();
        }
    }
}
