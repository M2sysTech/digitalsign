namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IAberturaContaRepositorio : IRepositorio<AberturaConta>
    {
        IList<AberturaConta> ObterPendentesImportacao();

        void FinalizarImportacao(int aberturaContaId);
    }
}