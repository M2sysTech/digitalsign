namespace Veros.Paperless.Model.Servicos.SeparacaoServiceEngine
{
    using System.Collections.Generic;
    using Entidades;

    public interface IGerarThumbnailServico
    {
        void Executar(List<List<Pagina>> todasAsPaginas, string cacheLocalImagens); 
    }
}