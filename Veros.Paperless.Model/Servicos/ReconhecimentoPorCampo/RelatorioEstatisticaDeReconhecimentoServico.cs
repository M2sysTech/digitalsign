namespace Veros.Paperless.Model.Servicos.ReconhecimentoPorCampo
{
    using System;
    using System.Collections.Generic;
    using Entidades;
    using Repositorios;

    public class RelatorioEstatisticaDeReconhecimentoServico : IRelatorioEstatisticaDeReconhecimentoServico
    {
        private readonly ICampoRepositorio campoRepositorio;
        private readonly IIndexacaoRepositorio indexacaoRepositorio;

        public RelatorioEstatisticaDeReconhecimentoServico( 
            ICampoRepositorio campoRepositorio, IIndexacaoRepositorio indexacaoRepositorio)
        {
            this.campoRepositorio = campoRepositorio;
            this.indexacaoRepositorio = indexacaoRepositorio;
        }

        public RelatorioDeReconhecimentoPorCampo Obter(DateTime dataInicial, DateTime dataFinal)
        {
            return new RelatorioDeReconhecimentoPorCampo()
            {
                Itens = this.GerarRelatorio(dataInicial, dataFinal),
                DataProducaoInicial = dataInicial,
                DataProducaoFinal = dataFinal
            };
        }

        private IList<ItemRelatorioDeReconhecimentoPorCampo> GerarRelatorio(DateTime dataInicial, DateTime dataFinal)
        {
            var listaItemRelatorioDeReconhecimentoPorCampo = new List<ItemRelatorioDeReconhecimentoPorCampo>();
            var itemRelatorioDeReconhecimentoPorCampo = new ItemRelatorioDeReconhecimentoPorCampo();

            var campos = this.campoRepositorio.ObterTodosReconheciveis();

            foreach (var campo in campos)
            {
                itemRelatorioDeReconhecimentoPorCampo.CampoNome = campo.Description;
                itemRelatorioDeReconhecimentoPorCampo.CampoId = campo.Id;
                itemRelatorioDeReconhecimentoPorCampo.PercentualAcertos = this.CalcularAcertos(campo, dataInicial, dataFinal);
                itemRelatorioDeReconhecimentoPorCampo.PercentualBatimento = this.CalcularBatimento(campo, dataInicial, dataFinal);
                itemRelatorioDeReconhecimentoPorCampo.PercentualFalsoPositivo = this.CalcularFalsoPositivo(campo, dataInicial, dataFinal);

                listaItemRelatorioDeReconhecimentoPorCampo.Add(itemRelatorioDeReconhecimentoPorCampo);
            }

            return listaItemRelatorioDeReconhecimentoPorCampo;
        }

        private long CalcularFalsoPositivo(Campo campo, DateTime dataInicial, DateTime dataFinal)
        {
            throw new NotImplementedException();
        }

        private long CalcularBatimento(Campo campo, DateTime dataInicial, DateTime dataFinal)
        {
            throw new NotImplementedException();
        }

        private long CalcularAcertos(Campo campo, DateTime dataInicial, DateTime dataFinal)
        {
            throw new NotImplementedException();
        }
    }
}
