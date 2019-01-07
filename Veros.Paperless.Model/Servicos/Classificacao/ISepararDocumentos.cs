namespace Veros.Paperless.Model.Servicos.Classificacao
{
    using System.Collections.Generic;
    using Entidades;

    public interface ISepararDocumentos
    {
        List<ItemParaSeparacao> Executar(int processoId);
    }
}