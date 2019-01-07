namespace Veros.Paperless.Model.Servicos.Identificacao
{
    using System.Collections.Generic;

    public interface IObtemTiposBloqueadosParaIdentificacaoServico
    {
        IList<int> Obter(int documentoId);
    }
}