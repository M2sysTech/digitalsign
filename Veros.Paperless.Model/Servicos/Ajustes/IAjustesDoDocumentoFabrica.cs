namespace Veros.Paperless.Model.Servicos.Ajustes
{
    using System.Collections.Generic;
    using Entidades;

    public interface IAjustesDoDocumentoFabrica
    {
        IList<AjusteDeDocumento> Obter(List<AjusteDeDocumento> ajustes);
    }
}