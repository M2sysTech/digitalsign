namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IComparaBioRepositorio : IRepositorio<ComparaBio>
    {
        IList<ComparaBio> ObterPendentesPorProcesso(int processoId);

        IList<ComparaBio> ObterTodasPorProcesso(int processoId);
    }
}