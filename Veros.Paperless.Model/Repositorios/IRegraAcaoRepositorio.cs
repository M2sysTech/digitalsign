namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IRegraAcaoRepositorio : IRepositorio<RegraAcao>
    {
        RegraAcao ObterAcaoPorRegra(int regraId);
    }
}