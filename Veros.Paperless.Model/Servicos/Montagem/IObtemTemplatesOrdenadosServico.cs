namespace Veros.Paperless.Model.Servicos.Montagem
{
    using System.Collections.Generic;
    using Entidades;

    public interface IObtemTemplatesOrdenadosServico
    {
        string Obter(IList<ValorReconhecido> valoresReconhecidos,
            IEnumerable<Template> templatesDoDocumento);
    }
}