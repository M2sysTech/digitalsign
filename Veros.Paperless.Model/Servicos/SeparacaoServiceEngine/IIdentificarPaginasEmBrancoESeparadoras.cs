namespace Veros.Paperless.Model.Servicos.SeparacaoServiceEngine
{
    using System.Collections.Generic;
    using Entidades;

    public interface IIdentificarPaginasEmBrancoESeparadoras
    {
        List<ItemParaSeparacao> Executar(IList<ItemParaSeparacao> itensParaSeparacao);
    }
}