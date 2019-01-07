namespace Veros.Paperless.Model.Servicos.SeparacaoServiceEngine
{
    using System.Collections.Generic;
    using Entidades;

    public interface IIdentificacaoPaginasEmBranco
    {
        IList<Pagina> Executar(Documento documento, IList<Pagina> paginas);
    }
}