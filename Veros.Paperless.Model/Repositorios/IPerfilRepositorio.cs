namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IPerfilRepositorio : IRepositorio<Perfil>
    {
        Perfil ObterPorSigla(string sigla);

        IList<Perfil> ObterPerfis();
    }
}
