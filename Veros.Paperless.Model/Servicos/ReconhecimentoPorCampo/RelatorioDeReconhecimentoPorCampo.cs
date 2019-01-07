namespace Veros.Paperless.Model.Servicos.ReconhecimentoPorCampo
{
    using System;
    using System.Collections.Generic;

    public class RelatorioDeReconhecimentoPorCampo
    {
        public DateTime DataProducaoInicial
        {
            get; 
            set;
        }

        public DateTime DataProducaoFinal
        {
            get;
            set;
        }

        public IList<ItemRelatorioDeReconhecimentoPorCampo> Itens
        {
            get; 
            set;
        }
    }
}