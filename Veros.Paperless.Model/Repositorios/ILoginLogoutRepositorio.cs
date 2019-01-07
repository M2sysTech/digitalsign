namespace Veros.Paperless.Model.Repositorios
{
    using System;
    using Entidades;
    using Framework.Modelo;

    public interface ILoginLogoutRepositorio : IRepositorio<LoginLogout>
    {
        void GravarLogoutDosPendentes(int usuarioId, DateTime data);
    }
}