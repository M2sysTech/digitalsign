namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IAgenciaRepositorio : IRepositorio<Agencia>
    {
        IList<Agencia> ObterPorBancoId(int bancoId);

        Agencia ObterPorNumeroAgenciaBancoId(string numeroAgencia, int bancoId);
    }
}