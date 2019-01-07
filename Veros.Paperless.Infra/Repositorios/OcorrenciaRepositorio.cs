namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using System.Linq;
    using NHibernate.Transform;
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class OcorrenciaRepositorio : Repositorio<Ocorrencia>, IOcorrenciaRepositorio
    {
        public void AlterarStatus(int ocorrenciaId, OcorrenciaStatus statusNovo)
        {
            this.Session
               .CreateQuery("update Ocorrencia set Status = :status where Id = :id")
               .SetInt32("id", ocorrenciaId)
               .SetParameter("status", statusNovo)
               .ExecuteUpdate();
        }

        public IList<Ocorrencia> Pesquisar(IList<OcorrenciaStatus> status, string barcodeCaixa, int tipoOcorrenciaId)
        {
            Ocorrencia ocorrencia = null;
            Pacote pacote = null;

            var query = this.Session.QueryOver<Ocorrencia>(() => ocorrencia)
                .JoinAlias(() => ocorrencia.Pacote, () => pacote)
                .Where(() => ocorrencia.Status != OcorrenciaStatus.Excluida);

            if (string.IsNullOrEmpty(barcodeCaixa) == false)
            {
                query.Where(() => pacote.Identificacao == barcodeCaixa);
            }

            if (tipoOcorrenciaId > 0)
            {
                query.Where(() => ocorrencia.Tipo.Id == tipoOcorrenciaId);
            }

            if (status.Any())
            {
                query.WhereRestrictionOn(() => ocorrencia.Status).IsIn(status.ToArray());
            }

            return query
                .OrderBy(() => ocorrencia.Id).Asc
                .List<Ocorrencia>();
        }

        public Ocorrencia ObterPorIdComDocumento(int ocorrenciaId)
        {
            return this.Session.QueryOver<Ocorrencia>()
                .Where(x => x.Id == ocorrenciaId)
                .Fetch(x => x.Documento).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .SingleOrDefault();
        }

        public IList<Ocorrencia> ObterPorDossie(int dossieId)
        {
            return this.Session.QueryOver<Ocorrencia>()
                .Where(x => x.DossieEsperado.Id == dossieId)                
                .TransformUsing(Transformers.DistinctRootEntity)
                .List<Ocorrencia>();
        }

        public Ocorrencia ObterComTipo(int ocorrenciaId)
        {
            return this.Session.QueryOver<Ocorrencia>()
                .Where(x => x.Id == ocorrenciaId)
                .Fetch(x => x.Tipo).Eager
                .SingleOrDefault();
        }

        public IList<Ocorrencia> ObterPorColeta(int coletaId)
        {
            return this.Session.QueryOver<Ocorrencia>()
                .OrderBy(x => x.DataRegistro).Asc
                .JoinQueryOver(x => x.Pacote)
                .Fetch(x => x.Pacote).Eager
                .Where(x => x.Coleta.Id == coletaId)
                .ThenBy(x => x.Identificacao).Asc
                .List<Ocorrencia>();
        }

        public void LimparResponsavel(int ocorrenciaId)
        {
            this.Session
               .CreateQuery("update Ocorrencia set UsuarioResponsavel = null, HoraInicio = null where Id = :id")
               .SetInt32("id", ocorrenciaId)
               .ExecuteUpdate();
        }
    }
}