namespace Veros.Paperless.Model.Servicos.SeparacaoServiceEngine
{
    using System.Collections.Generic;
    using Entidades;

    public interface ICorrigeOrientacaoServico
    {
        void Executar(List<List<Pagina>> todasAsPaginas, string cacheLocalImagens);
    }
}