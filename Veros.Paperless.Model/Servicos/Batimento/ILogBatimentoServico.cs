namespace Veros.Paperless.Model.Servicos.Batimento
{
    using System.Collections.Generic;
    using Entidades;

    public interface ILogBatimentoServico
    {
        void ExecutarValorReconhecido(Indexacao indexacao, ValorReconhecido valorReconhecido);

        void ExecutarFullText(Indexacao indexacao, List<dynamic> palavras);
    }
}