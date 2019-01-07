namespace Veros.Paperless.Model.Servicos.Ajustes
{
    using System.Collections.Generic;
    using Entidades;

    public interface ICriaListaDeAjustesServico
    {
        IEnumerable<AjusteDeDocumento> Executar(string acoes);
    }
}
