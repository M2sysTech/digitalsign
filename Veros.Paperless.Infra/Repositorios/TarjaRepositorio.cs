namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Entidades;
    using Model.Repositorios;

    public class TarjaRepositorio : Repositorio<Tarja>, ITarjaRepositorio
    {
        public IList<Tarja> ObterPorDocumentoCampoePagina(int documentoId, int campoId, int paginaId)
        {
            return this.Session.QueryOver<Tarja>()
            .Where(x => x.Documento.Id == documentoId)
            .And(x => x.Campo.Id == campoId)
            .And(x => x.Pagina.Id == paginaId)
            .List<Tarja>();
        }

        public IList<Tarja> ObterTodasPorPagina(int paginaId)
        {
            return this.Session.QueryOver<Tarja>()
            .Where(x => x.Pagina.Id == paginaId)
            .List<Tarja>();
        }

        public void ExcluirTarja(int tarjaId)
        {
            this.Session.CreateQuery(
                "delete from Tarja t where t.Id = :tarjaId")
                .SetInt32("tarjaId", tarjaId)
                .ExecuteUpdate();
        }
    }
}
