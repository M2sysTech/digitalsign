namespace Veros.Paperless.Model.Servicos.Ajustes
{
    using System.Collections.Generic;
    using Entidades;

    public interface IAplicarAjustesNaPaginaServico
    {
        string Executar(Pagina pagina, IList<AjusteDeDocumento> ajustesDaPagina);
    }
}