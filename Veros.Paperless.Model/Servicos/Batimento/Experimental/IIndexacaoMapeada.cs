namespace Veros.Paperless.Model.Servicos.Batimento.Experimental
{
    using System.Collections.Generic;
    using Entidades;

    public interface IIndexacaoMapeada
    {
        Indexacao Indexacao
        {
            get;
            set;
        }

        ValorReconhecido ValorReconhecido
        {
            get;
            set;
        }

        Campo Campo
        {
            get;
        }

        IList<IndexacaoMapeada> ComValoresReconhecidos(
            IList<Indexacao> indexacao, 
            IList<ValorReconhecido> valoresReconhecidos);

        string ObterValorParaBatimento();
    }
}