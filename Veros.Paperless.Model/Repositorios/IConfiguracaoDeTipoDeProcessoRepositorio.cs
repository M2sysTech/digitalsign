namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IConfiguracaoDeTipoDeProcessoRepositorio : IRepositorio<ConfiguracaoDeTipoDeProcesso>
    {
        IList<ConfiguracaoDeTipoDeProcesso> ObterPorTipoDeProcesso(int tipoProcessoId);
        
        IList<TipoDocumento> ObterTiposNaoObrigatorios(int tipoProcessoId);
    }
}
