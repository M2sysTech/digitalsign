namespace Veros.Paperless.Model.Servicos.Importacao
{
    using System.Collections.Generic;
    using Entidades;

    public interface IImportacaoPropostaServico
    {
        void Executar(int loteId, IList<ImagemConta> imagensConta, CapturaFinalizada capturaFinalizada);
    }
}