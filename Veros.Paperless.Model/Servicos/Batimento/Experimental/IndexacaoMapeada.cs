namespace Veros.Paperless.Model.Servicos.Batimento.Experimental
{
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;
    using Veros.Framework;

    public class IndexacaoMapeada : IIndexacaoMapeada
    {
        public Indexacao Indexacao
        {
            get;
            set;
        }

        public ValorReconhecido ValorReconhecido
        {
            get;
            set;
        }

        public Campo Campo
        {
            get
            {
                return this.Indexacao.Campo;
            }
        }

        public IList<IndexacaoMapeada> ComValoresReconhecidos(
            IList<Indexacao> indexacao, 
            IList<ValorReconhecido> valoresReconhecidos)
        {
            if (valoresReconhecidos.NaoTemConteudo())
            {
                return new List<IndexacaoMapeada>();
            }

            IList<IndexacaoMapeada> indexacaoMapeada = new List<IndexacaoMapeada>();

            foreach (var index in indexacao)
            {
                foreach (var reconhecido in valoresReconhecidos)
                {
                    if (index.Campo.EstaMapeadoPara(reconhecido.CampoTemplate, reconhecido.TemplateName))
                    {
                        indexacaoMapeada.Add(new IndexacaoMapeada
                        {
                            Indexacao = index,
                            ValorReconhecido = reconhecido
                        });
                    }
                }
            }

            return indexacaoMapeada;
        }

        public string ObterValorParaBatimento()
        {
            switch (BaterDocumento.BatimentoCom)
            {
                case BatimentoDo.ValorFinal:
                    return this.Indexacao.ValorFinal;

                default:
                    return this.Indexacao.SegundoValor;
            }  
        }
    }
}