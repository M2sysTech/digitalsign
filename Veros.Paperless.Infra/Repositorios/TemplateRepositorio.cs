namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class TemplateRepositorio : Repositorio<Template>, ITemplateRepositorio
    {
        public IList<Template> ObterPorTipoDeDocumento(int tipoDeDocumentoId)
        {
            return this.Session.QueryOver<Template>()
                .Where(x => x.TipoDocumento.Id == tipoDeDocumentoId)
                .List();
        }
    }
}
