namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface ITemplateRepositorio : IRepositorio<Template>
    {
        IList<Template> ObterPorTipoDeDocumento(int tipoDeDocumentoId);
    }
}
