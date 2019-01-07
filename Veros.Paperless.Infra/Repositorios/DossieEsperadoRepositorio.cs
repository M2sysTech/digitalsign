namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using NHibernate.Transform;
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class DossieEsperadoRepositorio : Repositorio<DossieEsperado>, IDossieEsperadoRepositorio
    {
        public IList<DossieEsperado> ObterComCaixaId(int pacoteId)
        {
            return this.Session.QueryOver<DossieEsperado>()
                .Fetch(x => x.Pacote).Eager
                .Where(x => x.Pacote.Id == pacoteId)
                .List();
        }

        public DossieEsperado Obter(int caixaId, string numeroContrato, string matriculaAgente)
        {
            return this.Session.QueryOver<DossieEsperado>()
                .Where(x => x.Pacote.Id == caixaId)
                .Where(x => x.NumeroContrato == numeroContrato)
                .Where(x => x.MatriculaAgente == matriculaAgente)
                .Take(1)
                .SingleOrDefault();
        }

        public IList<DossieEsperado> ObterPorPacote(Pacote pacote)
        {
            return this.Session.QueryOver<DossieEsperado>()
                .Where(x => x.Pacote.Id == pacote.Id)
                .List();
        }

        public IList<DossieEsperado> ObterPorCaixaComLotes(string identificacaoCaixa)
        {
            return this.Session.QueryOver<DossieEsperado>()
                .Fetch(x => x.Lotes).Eager
                .JoinQueryOver(x => x.Pacote)
                .Where(x => x.Identificacao == identificacaoCaixa)
                .List();
        }

        public IList<DossieEsperado> ObterPriorizados(string caixa, string folder, string processoPesquisa)
        {
            Lote lote = null;
            DossieEsperado dossieEsperado = null;
            Pacote pacote = null;
            Processo processo = null;

            var trazerSoPriorizados = true;

            var query = this.Session.QueryOver<DossieEsperado>(() => dossieEsperado)
                .JoinQueryOver(() => dossieEsperado.Pacote, () => pacote)
                .JoinQueryOver(() => dossieEsperado.Lotes, () => lote)
                .JoinQueryOver(() => lote.Processos, () => processo)
                .Fetch(x => x.Lotes).Eager
                .Fetch(x => x.Pacote).Eager
                .Where(() => pacote.Id > 0)
                .Where(() => lote.Id > 0)
                .Where(() => processo.Id > 0 && processo.Identificacao != null)
                .Where(() => lote.Status != LoteStatus.Faturamento)
                .Where(() => lote.Status != LoteStatus.AguardandoControleQualidadeCef)
                .Where(() => lote.ResultadoQualidadeCef == null || lote.ResultadoQualidadeCef != LoteStatusAuxiliar.ResultadoQualidadeCefReprovado)
                .TransformUsing(Transformers.DistinctRootEntity);

            if (string.IsNullOrEmpty(caixa) == false)
            {
                trazerSoPriorizados = false;
                query.Where(() => pacote.Identificacao == caixa);
            }

            if (string.IsNullOrEmpty(folder) == false)
            {
                trazerSoPriorizados = false;
                query.Where(() => dossieEsperado.CodigoDeBarras == folder);
            }

            if (string.IsNullOrEmpty(processoPesquisa) == false)
            {
                trazerSoPriorizados = false;
                query.Where(() => processo.Identificacao  == processoPesquisa);
            }

            if (trazerSoPriorizados)
            {
                query.Where(() => lote.ResultadoQualidadeCef == LoteStatusAuxiliar.ResultadoQualidadeCefPriorizado);   
            }
            
            return query.OrderBy(x => lote.ResultadoQualidadeCef).Asc.List();
        }

        public IList<DossieEsperado> ObterRetirados(string caixa, string folder, string processoPesquisa)
        {
            Lote lote = null;
            DossieEsperado dossieEsperado = null;
            Pacote pacote = null;
            Processo processo = null;

            var trazerSoRetirados = true;

            var query = this.Session.QueryOver<DossieEsperado>(() => dossieEsperado)
                .JoinQueryOver(() => dossieEsperado.Pacote, () => pacote)
                .JoinQueryOver(() => dossieEsperado.Lotes, () => lote)
                .JoinQueryOver(() => lote.Processos, () => processo)
                .Fetch(x => x.Lotes).Eager
                .Fetch(x => x.Pacote).Eager
                .Where(() => pacote.Id > 0)
                .Where(() => lote.Id > 0)
                .Where(() => processo.Id > 0 && processo.Identificacao != null)                
                .TransformUsing(Transformers.DistinctRootEntity);

            if (string.IsNullOrEmpty(caixa) == false)
            {
                trazerSoRetirados = false;
                query.Where(() => pacote.Identificacao == caixa);
            }

            if (string.IsNullOrEmpty(folder) == false)
            {
                trazerSoRetirados = false;
                query.Where(() => dossieEsperado.CodigoDeBarras == folder);
            }

            if (string.IsNullOrEmpty(processoPesquisa) == false)
            {
                trazerSoRetirados = false;
                query.Where(() => processo.Identificacao == processoPesquisa);
            }

            if (trazerSoRetirados)
            {
                query.Where(() => processo.AcaoClassifier == ProcessoStatusAuxiliar.RetiradoDaFila);
            }

            return query.OrderBy(x => processo.AcaoClassifier).Desc.List();
        }

        public DossieEsperado ObterDossieDuplicado(DossieEsperado dossieEsperado)
        {
            return this.Session.QueryOver<DossieEsperado>()
                .Where(x => x.NumeroContrato == dossieEsperado.NumeroContrato)
                .Where(x => x.MatriculaAgente == dossieEsperado.MatriculaAgente)
                .Where(x => x.Hipoteca == dossieEsperado.Hipoteca)
                .Where(x => x.CodigoDeBarras == dossieEsperado.CodigoDeBarras)
                .Take(1)
                .SingleOrDefault();
        }

        public void AtualizarUltimaOcorrencia(int dossieEsperadoId, Ocorrencia ocorrenciaParaSalvar)
        {
            this.Session
              .CreateQuery("update DossieEsperado set UltimaOcorrencia = :ocorrenciaParaSalvar where Id = :id")
              .SetParameter("ocorrenciaParaSalvar", ocorrenciaParaSalvar)
              .SetParameter("id", dossieEsperadoId)
              .ExecuteUpdate();
        }

        public DossieEsperado ObterDossie(DossieEsperado dossieEsperado)
        {
            return this.Session.QueryOver<DossieEsperado>()
                .Where(x => x.NumeroContrato == dossieEsperado.NumeroContrato)
                .Where(x => x.MatriculaAgente == dossieEsperado.MatriculaAgente)
                .Where(x => x.Hipoteca == dossieEsperado.Hipoteca)
                .Where(x => x.NomeDoMutuario == dossieEsperado.NomeDoMutuario)
                .Take(1)
                .SingleOrDefault();
        }

        public DossieEsperado ObterPorMatriculaContratoHipoteca(string matricula, string numeroContrato, string hipoteca)
        {
            return this.Session.QueryOver<DossieEsperado>()
                .Where(x => x.MatriculaAgente == matricula)
                .Where(x => x.NumeroContrato == numeroContrato)
                .Where(x => x.Hipoteca == hipoteca)
                .Take(1)
                .SingleOrDefault();
        }

        public int Max()
        {
            const string Hql = @"
select 
    max(Id)  
from 
    DossieEsperado";

            return this.Session.CreateQuery(Hql)
                .UniqueResult<int>();
        }

        public DossieEsperado ObterComLotes(int dossieId)
        {
            return this.Session.QueryOver<DossieEsperado>()
                .Fetch(x => x.Lotes).Eager
                .Where(x => x.Id == dossieId)
                .TransformUsing(Transformers.DistinctRootEntity)
                .SingleOrDefault();
        }

        public void ExcluirDossiesPorColeta(int coletaId)
        {
            this.Session
              .CreateQuery("delete from DossieEsperado where Pacote.Id in (select Id from Pacote where Coleta.Id = :coletaId)")
              .SetParameter("coletaId", coletaId)              
              .ExecuteUpdate();
        }
    }
}