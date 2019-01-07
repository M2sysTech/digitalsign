namespace Veros.Paperless.Model.Servicos.SeparacaoServiceEngine
{
    using System.Collections.Generic;
    using Entidades;

    public interface IIdentificarPaginaSeparadora
    {
        ItemParaSeparacao Executar(IList<ItemParaSeparacao> todasAsPaginas, ItemParaSeparacao item);
    }
}