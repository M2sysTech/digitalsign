namespace Veros.Paperless.Infra.Repositorios
{
    using System;
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Entidades;
    using Model.Repositorios;
    using Model.ViewModel;
    using NHibernate.Criterion;
    using NHibernate.Transform;
    using NHibernate.Util;

    public class PacoteRepositorio : Repositorio<Pacote>, IPacoteRepositorio
    {
        public Pacote ObterPacoteAbertoNaEstacao(string estacao)
        {
            return this.Session.QueryOver<Pacote>()
                .Where(x => x.Status == Pacote.Aberto)
                .And(x => x.Estacao == estacao)
                .SingleOrDefault<Pacote>();
        }

        public Pacote ObterPorIdentificacaoCaixa(string barcodeCx)
        {
            return this.Session.QueryOver<Pacote>()
                .Where(x => x.Identificacao == barcodeCx)
                .TransformUsing(Transformers.DistinctRootEntity)
                .SingleOrDefault<Pacote>();
        }        

        public Pacote ObterPorCaixa(string identificacao)
        {
            return this.Session.QueryOver<Pacote>()
                .Fetch(x => x.Coleta).Eager
                .Where(x => x.Identificacao == identificacao)
                .Take(1)
                .SingleOrDefault();
        }

        public void MarcarEtiquetaCefGerada(int pacoteId)
        {
            this.Session
              .CreateQuery("update Pacote set EtiquetaCefGerada = 1 where Id = :id")
              .SetInt32("id", pacoteId)
              .ExecuteUpdate();
        }

        public IList<int> ObterParaExportacao()
        {
            return this.Session.QueryOver<Pacote>()
                .Where(x => x.Status == Pacote.ParaExportar)
                .OrderBy(x => x.Id).Asc
                .Select(x => x.Id)
                .List<int>();
        }

        public void MarcarComoExportado(int pacoteId)
        {
            this.Session
              .CreateQuery("update Pacote set Status = :status where Id = :id")
              .SetInt32("id", pacoteId)
              .SetString("status", Pacote.Fechado)
              .ExecuteUpdate();
        }

        public void MarcarComErro(int pacoteId)
        {
            this.Session
              .CreateQuery("update Pacote set Status = :status where Id = :id")
              .SetInt32("id", pacoteId)
              .SetString("status", Pacote.ErroAoExportar)
              .ExecuteUpdate();
        }

        public void MarcarParaExportar(Pacote pacote)
        {
            this.Session
              .CreateQuery("update Pacote set Status = :status where Id = :id")
              .SetInt32("id", pacote.Id)
              .SetString("status", Pacote.ParaExportar)
              .ExecuteUpdate();
        }

        public void MarcarParaDevolucao(int pacoteId)
        {
            this.Session
              .CreateQuery("update Pacote set Status = :status where Id = :id")
              .SetInt32("id", pacoteId)
              .SetString("status", Pacote.MarcadaParaDevolucao)
              .ExecuteUpdate();
        }

        public void MarcarComoDevolvida(int pacoteId)
        {
            this.Session
              .CreateQuery("update Pacote set Status = :status, DataDevolucao = :dataDevolucao where Id = :id")
              .SetParameter("status", Pacote.Devolvida)
              .SetParameter("dataDevolucao", DateTime.Today)
              .SetParameter("id", pacoteId)
              .ExecuteUpdate();
        }

        public void MarcarComoConferida(int pacoteId)
        {
            this.Session
              .CreateQuery("update Pacote set Status = :status where Id = :id")
              .SetInt32("id", pacoteId)
              .SetString("status", Pacote.Fechado)
              .ExecuteUpdate();
        }

        public Pacote ObterPacoteDoProcesso(Processo processo)
        {
              const string Hql = @"
select
    pacote
from 
    Pacote pacote,
    Lote lote,
    Processo processo
where 
    pacote.Id = lote.Pacote.Id
    and processo.Lote.Id = lote.Id
    and processo.Id = :processoId";

              var pacotes = this.Session.CreateQuery(Hql)
                  .SetParameter("processoId", processo.Id)
                  .SetResultTransformer(Transformers.DistinctRootEntity)
                  .List<Pacote>();

            return pacotes[0];
        }

        public IList<Pacote> ObterPorColeta(Coleta coleta)
        {
            return this.Session.QueryOver<Pacote>()
                .Where(x => x.Coleta == coleta)
                .JoinQueryOver(x => x.Lotes.First())
                .Fetch(x => x.Lotes).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .List<Pacote>();
        }

        public IList<Pacote> ObterPorStatus(string status)
        {
            return this.Session.QueryOver<Pacote>()
                .Fetch(x => x.Coleta).Eager
                .Where(x => x.Status == status)
                .List();
        }

        public Pacote ObterComDossiesEsperados(int pacoteId)
        {
            return this.Session.QueryOver<Pacote>()
            .Fetch(x => x.DossiesEsperados).Eager
            .Where(x => x.Id == pacoteId)
            .SingleOrDefault<Pacote>();
        }

        public Pacote ObterComColetaPorId(int pacoteId)
        {
            return this.Session.QueryOver<Pacote>()
                .Fetch(x => x.Coleta).Eager
                .Where(x => x.Id == pacoteId)
                .SingleOrDefault<Pacote>();
        }

        public IList<Pacote> Pesquisar(PesquisaPacoteViewModel filtros)
        {
            Pacote pacote = null;
            Coleta coleta = null;

            var query = this.Session.QueryOver<Pacote>(() => pacote)
                .JoinAlias(() => pacote.Coleta, () => coleta);

            if (string.IsNullOrEmpty(filtros.PacoteStatus) == false)
            {
                query.Where(() => pacote.Status == filtros.PacoteStatus);
            }

            query.TransformUsing(Transformers.DistinctRootEntity);

            switch (filtros.ColunaDeOrdenacao)
            {
                case "caixa":
                    if (filtros.TipoDeOrdenacao == "A")
                    {
                        query.OrderBy(() => pacote.Identificacao).Asc();
                    }
                    else
                    {
                        query.OrderBy(() => pacote.Identificacao).Desc();
                    }

                    break;

                case "coleta":
                    if (filtros.TipoDeOrdenacao == "A")
                    {
                        query.OrderBy(() => coleta.Id).Asc();
                    }
                    else
                    {
                        query.OrderBy(() => coleta.Id).Desc();
                    }
                 
                    break;

                case "descricao":
                    if (filtros.TipoDeOrdenacao == "A")
                    {
                        query.OrderBy(() => coleta.Descricao).Asc();
                    }
                    else
                    {
                        query.OrderBy(() => coleta.Descricao).Desc();
                    }

                    break;

                case "endereco":
                    if (filtros.TipoDeOrdenacao == "A")
                    {
                        query.OrderBy(() => coleta.Endereco).Asc();
                    }
                    else
                    {
                        query.OrderBy(() => coleta.Endereco).Desc();
                    }

                    break;

                case "dataColeta":
                    if (filtros.TipoDeOrdenacao == "A")
                    {
                        query.OrderBy(() => coleta.DataColetaRealizada).Asc();
                    }
                    else
                    {
                        query.OrderBy(() => coleta.DataColetaRealizada).Desc();
                    }

                    break;

                case "dataRecepcao":
                    if (filtros.TipoDeOrdenacao == "A")
                    {
                        query.OrderBy(() => coleta.DataRecepcao).Asc();
                    }
                    else
                    {
                        query.OrderBy(() => coleta.DataRecepcao).Desc();
                    }

                    break;
            }

            return query
                .List();
        }

        public Pacote ObterPorIdentificacaoCaixaEColeta(string identificacao, int coleta)
        {
            return this.Session.QueryOver<Pacote>()
                .Where(x => x.Identificacao == identificacao)
                .Where(x => x.Coleta.Id == coleta)
                .TransformUsing(Transformers.DistinctRootEntity)
                .SingleOrDefault<Pacote>();
        }

        public void AlterarStatusPorColeta(Coleta coleta, string status)
        {
            this.Session
              .CreateQuery("update Pacote set Status = :status where Coleta.Id = :coletaId")
              .SetInt32("coletaId", coleta.Id)
              .SetString("status", status)
              .ExecuteUpdate();
        }

        public void ApagarPorColeta(Coleta coleta, string status)
        {
            this.Session
              .CreateQuery("delete Pacote where Coleta.Id = :coletaId and Status = :status ")
              .SetInt32("coletaId", coleta.Id)
              .SetString("status", status)
              .ExecuteUpdate();
        }

        public void AlterarStatus(Pacote pacote, string status)
        {
            this.Session
              .CreateQuery("update Pacote set Status = :status where Id = :id")
              .SetString("status", status)
              .SetInt32("id", pacote.Id)
              .ExecuteUpdate();
        }

        public void ExcluirPacotePorColeta(int coletaId)
        {
            this.Session
              .CreateQuery("delete Pacote where Coleta.Id = :coletaId")
              .SetInt32("coletaId", coletaId)
              .ExecuteUpdate();
        }

        public Pacote ObterCaixa(int pacoteId)
        {
            return this.Session.QueryOver<Pacote>()
                .Fetch(x => x.Coleta).Eager
                .Where(x => x.Id == pacoteId)
                .Take(1)
                .SingleOrDefault();
        }
    }
}