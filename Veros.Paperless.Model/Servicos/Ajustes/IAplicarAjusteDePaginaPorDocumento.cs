namespace Veros.Paperless.Model.Servicos.Ajustes
{
    using System.Collections.Generic;
    using Entidades;

    public interface IAplicarAjusteDePaginaPorDocumento
    {
        bool Executar(int documentoId, IList<AjusteDeDocumento> ajustes);
    }
}