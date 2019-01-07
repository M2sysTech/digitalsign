namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IRegraRepositorio : IRepositorio<Regra>
    {
        Regra ObterRegraPorVinculo(string vinculo);

        IList<Regra> ObterRegrasVinculadas(string vinculo);

        IList<Regra> ObterRegrasPorFase(string fase, TipoProcesso tipoProcesso);

        Regra ObterRegraPorIdentificador(string identificador);
    }
}
