namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class FaceRepositorio : Repositorio<Face>, IFaceRepositorio
    {
        public IList<int> ObterIdsParaCompararExceto(int faceId)
        {
            return this.Session.QueryOver<Face>()
                .Where(x => x.Comum != "S" || x.Comum == null)
                .And(x => x.Id != faceId)
                .Select(x => x.Id)
                .Cacheable()
                .CacheRegion("1d")
                .List<int>();
        }

        public Face ObterComPagina(int faceId)
        {
            return this.Session.QueryOver<Face>()
                .Fetch(x => x.Pagina).Eager
                .Where(x => x.Id == faceId)
                .SingleOrDefault();
        }

        public void ComparacaoFaceFinalizada(int faceId, bool faceComum)
        {
            this.Session
              .CreateQuery("update Face set StatusDaComparacao = :status, Comum = :faceComum where Id = :id")
              .SetParameter("id", faceId)
              .SetParameter("status", FaceStatus.ComparacaoFinalizada)
              .SetParameter("faceComum", faceComum ? "S" : "N")
              .ExecuteUpdate();
        }

        public IList<Face> ObterFacesPorPaginaId(int paginaId)
        {
            return this.Session.QueryOver<Face>()
                .Fetch(x => x.Pagina).Eager
                .Where(x => x.Pagina.Id == paginaId)
                .List<Face>();
        }

        public string ObterNomeArquivoPorId(int faceId)
        {
            return this.Session.QueryOver<Face>()
            .Where(x => x.Id == faceId)
            .Select(x => x.NomeArquivo)
            .SingleOrDefault<string>();
        }
    }
}