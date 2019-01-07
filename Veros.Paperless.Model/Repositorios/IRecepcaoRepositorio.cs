namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Veros.Framework.Modelo;
    using Veros.Paperless.Model.Entidades;

    public interface IRecepcaoRepositorio : IRepositorio<Recepcao>
    {
        Recepcao[] ObterPendente();

        void IncrementarImportado(Recepcao recepcao);
    }
}