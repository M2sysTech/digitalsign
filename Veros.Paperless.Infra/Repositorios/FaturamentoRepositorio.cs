namespace Veros.Paperless.Infra.Repositorios
{
    using System;
    using System.Collections.Generic;
    using Model.Consultas;
    using NHibernate.Criterion;
    using NHibernate.Transform;
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class FaturamentoRepositorio : Repositorio<Faturamento>, IFaturamentoRepositorio
    {
        public IList<AcompanhamentoProducaoConsulta> ObterAcompanhamentoProducaoPorData(DateTime data)
        {
            return this.Session.CreateCriteria(typeof(Lote))
                .CreateAlias("EstatisticaPacoteProcessado", "Faturamento")
                .Add(Restrictions.Between("DataCriacao", data.AddDays(-1), data.AddDays(1)))
                .SetProjection(Projections.ProjectionList()
                                    .Add(Projections.Alias(Projections.GroupProperty("Faturamento.Arquivo"), "NomeArquivo"))
                                    .Add(Projections.Alias(Projections.Max("DataCriacao"), "DataImportacao"))
                                    .Add(Projections.Alias(Projections.Max("DataFimIcr"), "DataValidacao"))
                                    .Add(Projections.Alias(Projections.Max("DataFimProvaZero"), "DataProvaZero"))
                                    .Add(Projections.Alias(Projections.Max("DataAguardandoAprovacao"), "DataAprovacao"))
                                    .Add(Projections.Alias(Projections.Max("DataFimExportacao"), "DataExportacao")))
                .SetResultTransformer(Transformers.AliasToBean(typeof(AcompanhamentoProducaoConsulta)))
                .List<AcompanhamentoProducaoConsulta>();
        }

        public IList<Faturamento> ObterPorPeriodoETipo(int tipoFaturamento, DateTime dataInicio, DateTime dataFim)
        {
            return this.Session.QueryOver<Faturamento>()
                .Where(x => x.DataFaturamento >= dataInicio)
                .And(x => x.DataFaturamento <= dataFim)
                .And(x => x.TipoDeFaturamento == tipoFaturamento)
                .OrderBy(x => x.DataFaturamento).Asc
                .List();
        }

        public IList<Faturamento> ObterPedenciasDeFaturamentoPorArquivoParaDataReferencia(DateTime dataParaFaturamento)
        {
            var dataParaFaturamentoLimite = dataParaFaturamento.AddDays(1);
            return this.Session.QueryOver<Faturamento>()
                .Where(x => x.TipoDeFaturamento == Faturamento.TipoFaturamentoPorArquivo)
                .And(x => x.Status == Faturamento.StatusFaturamentoPendente)
                .And(x => x.DataFaturamento >= dataParaFaturamento)
                .And(x => x.DataFaturamento < dataParaFaturamentoLimite)
                .List();
        }

        public IList<Faturamento> ObterFaturamentoDiarioOk(DateTime dataParaFaturamento)
        {
            var dataParaFaturamentoLimite = dataParaFaturamento.AddDays(1);
            return this.Session.QueryOver<Faturamento>()
                .Where(x => x.TipoDeFaturamento == Faturamento.TipoFaturamentoDiario)
                .And(x => x.Status == Faturamento.StatusFaturamentoOk)
                .And(x => x.DataFaturamento >= dataParaFaturamento)
                .And(x => x.DataFaturamento < dataParaFaturamentoLimite)
                .List();
        }

        ////TODO:PASSAR O PACOTE,PRECISAR SER FEITA NO PACOTEPROCESSADOREPOSITORIO
        public IList<Faturamento> ObterPendenciasDeFaturamentoPorPacote()
        {
            return null;
        }

        public IList<Faturamento> ObterPendenciasDeFaturamentoMensal()
        {
            return null;
        }

        public Faturamento ObterUmFaturamentoDiarioPendente(DateTime dataFaturamento)
        {
            return this.Session.QueryOver<Faturamento>()
                .Where(x => x.DataFaturamento == dataFaturamento)
                .And(x => x.Status == Faturamento.StatusFaturamentoPendente)
                .And(x => x.TipoDeFaturamento == Faturamento.TipoFaturamentoDiario)
                .SingleOrDefault();
        }

        public IList<Faturamento> ObterDatasPendentesDeFaturamentoPorTipo(int tipoFaturamento)
        {
            return this.Session.QueryOver<Faturamento>()
                .Where(x => x.TipoDeFaturamento == tipoFaturamento)
                .And(x => x.Status == Faturamento.StatusFaturamentoPendente)
                .List();
        }

        public IList<Faturamento> ObterTodosPorTipoParaDataReferencia(int tipoFaturamentoDiario, DateTime dataFaturamento)
        {
            var dataParaFaturamentoLimite = dataFaturamento.AddDays(1);
            return this.Session.QueryOver<Faturamento>()
                .Where(x => x.TipoDeFaturamento == tipoFaturamentoDiario)
                .And(x => x.DataFaturamento >= dataFaturamento)
                .And(x => x.DataFaturamento < dataParaFaturamentoLimite)
                .List();
        }

        public Faturamento ObterUmFaturamentoMensalPendente(DateTime dataDeFaturamento)
        {
            var dataParaFaturamentoLimite = dataDeFaturamento.AddDays(1);
            return this.Session.QueryOver<Faturamento>()
                .Where(x => x.DataFaturamento >= dataDeFaturamento)
                .Where(x => x.DataFaturamento < dataParaFaturamentoLimite)
                .And(x => x.TipoDeFaturamento == Faturamento.TipoFaturamentoMensal)
                .And(x => x.Status == Faturamento.StatusFaturamentoPendente)
                .SingleOrDefault();
        }

        public IList<Faturamento> ObterPendenciasDeFaturamentoPorDiaParaDataReferencia(DateTime dataParaFaturamento)
        {
            var dataParaFaturamentoLimite = dataParaFaturamento.AddDays(1);
            return this.Session.QueryOver<Faturamento>()
                .Where(x => x.TipoDeFaturamento == Faturamento.TipoFaturamentoDiario)
                .And(x => x.Status == Faturamento.StatusFaturamentoPendente)
                .And(x => x.DataFaturamento >= dataParaFaturamento)
                .And(x => x.DataFaturamento < dataParaFaturamentoLimite)
                .List();
        }

        public Faturamento ObterUmFaturamentoArquivoPendentePorNome(string nomeArquivo)
        {
            return this.Session.QueryOver<Faturamento>()
                .Where(x => x.NomeArquivo == nomeArquivo)
                .SingleOrDefault();
        }

        public IList<Faturamento> ObterFaturamentoPorArquivoOk(DateTime dataDeFaturamento)
        {
            var dataParaFaturamentoLimite = dataDeFaturamento.AddDays(1);
            return this.Session.QueryOver<Faturamento>()
                .Where(x => x.TipoDeFaturamento == Faturamento.TipoFaturamentoPorArquivo)
                .And(x => x.Status == Faturamento.StatusFaturamentoOk)
                .And(x => x.DataFaturamento >= dataDeFaturamento)
                .And(x => x.DataFaturamento < dataParaFaturamentoLimite)
                .List();
        }
    }
}