namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IDominioCampoRepositorio : IRepositorio<DominioCampo>
    {
        DominioCampo ObterPorCodigoEChave(string codigo, string chave);
        
        IList<DominioCampo> ObterDominiosPorCodigo(string codigo);
    }
}
