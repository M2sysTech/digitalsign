namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class ComparaBioRepositorio : Repositorio<ComparaBio>, IComparaBioRepositorio
    {
        public IList<ComparaBio> ObterPendentesPorProcesso(int processoId)
        {
            return this.Session.QueryOver<ComparaBio>()
                .Where(x => x.Status == "P")
                .JoinQueryOver(x => x.Face1)
                .JoinQueryOver(x => x.Pagina)
                .JoinQueryOver(x => x.Documento)
                .Where(x => x.Processo.Id == processoId)
                .List();
        }

        public IList<ComparaBio> ObterTodasPorProcesso(int processoId)
        {
            return this.Session.QueryOver<ComparaBio>()
                .JoinQueryOver(x => x.Face1)
                .JoinQueryOver(x => x.Pagina)
                .JoinQueryOver(x => x.Documento)
                .Where(x => x.Processo.Id == processoId)
                .Fetch(x => x.Face1).Eager
                .Fetch(x => x.Face2).Eager
                .List();
        }
    }
}