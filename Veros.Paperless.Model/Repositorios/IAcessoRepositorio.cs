namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IAcessoRepositorio : IRepositorio<Acesso>
    {
        IList<Acesso> ObterAcessosPorPerfil(int perfilId);

        void ApagarPorPerfil(int perfilId);

        bool ExisteAcessoPorPerfilEFuncionalidade(int perfilId, Funcionalidade funcionalidade);
    }
}