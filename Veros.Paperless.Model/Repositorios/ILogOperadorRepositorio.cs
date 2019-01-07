namespace Veros.Paperless.Model.Repositorios
{
    using System;
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface ILogOperadorRepositorio : IRepositorio<LogOperador>
    {
        void GravarLogOut(int usuarioId, DateTime horaLogOut);
        
        IList<LogOperador> ObterPendenciasDeLogout(int usuarioId);

        LogOperador ObterUltimoLogin(int usuarioId);

        void AtualizarUltimoAcesso(int usuarioId, DateTime horaAtual);
    }
}
