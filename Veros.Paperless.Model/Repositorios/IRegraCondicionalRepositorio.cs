namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IRegraCondicionalRepositorio : IRepositorio<RegraCondicional>
    {
        IList<RegraCondicional> ObterPorRegraId(int regraId);
    }
}
