namespace Veros.Paperless.Model.Servicos.SeparacaoServiceEngine
{
    using System.Collections.Generic;
    using Entidades;

    public interface IIdentificacaoPaginasSeparadoras
    {
        IList<Pagina> Executar(Documento documento, IList<Pagina> paginasJpeg);
    }
}