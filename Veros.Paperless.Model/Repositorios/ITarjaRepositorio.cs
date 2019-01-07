namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface ITarjaRepositorio : IRepositorio<Tarja>
    {
        IList<Tarja> ObterPorDocumentoCampoePagina(int documentoId, int campoId, int paginaId);

        IList<Tarja> ObterTodasPorPagina(int paginaId);

        void ExcluirTarja(int tarjaId);
    }
}
