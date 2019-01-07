namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class ConfiguracaoDeTipoDeProcessoRepositorio : Repositorio<ConfiguracaoDeTipoDeProcesso>, IConfiguracaoDeTipoDeProcessoRepositorio
    {
        public IList<ConfiguracaoDeTipoDeProcesso> ObterPorTipoDeProcesso(int tipoProcessoId)
        {
            return this.Session.QueryOver<ConfiguracaoDeTipoDeProcesso>()
                .Where(x => x.TipoProcesso.Id == tipoProcessoId)
                .OrderBy(x => x.TipoProcesso).Asc
                .List();
        }

        public IList<TipoDocumento> ObterTiposNaoObrigatorios(int tipoProcessoId)
        {
            return this.Session.QueryOver<ConfiguracaoDeTipoDeProcesso>()
                .Where(x => x.TipoProcesso.Id == tipoProcessoId)
                .And(x => x.Obrigatorio == "S")
                .Select(x => x.TipoDocumento)
                .List<TipoDocumento>();
        }
    }
}