namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IPalavraTipoRepositorio : IRepositorio<PalavraTipo>
    {
        IList<PalavraTipo> ObterTodosComTipo(); 
    }
}