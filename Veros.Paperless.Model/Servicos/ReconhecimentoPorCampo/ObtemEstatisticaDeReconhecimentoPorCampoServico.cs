namespace Veros.Paperless.Model.Servicos.ReconhecimentoPorCampo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class ObtemEstatisticaDeReconhecimentoPorCampoServico : IObtemEstatisticaDeReconhecimentoPorCampoServico
    {
        private readonly IIndexacaoRepositorio indexacaoRepositorio;

        public ObtemEstatisticaDeReconhecimentoPorCampoServico(IIndexacaoRepositorio indexacaoRepositorio)
        {
            this.indexacaoRepositorio = indexacaoRepositorio;
        }

        public RelatorioDeReconhecimentoPorCampo Obter(DateTime dataProducao)
        {
            var indexacoes = this.indexacaoRepositorio.ObterValidadosPorDataDeProducao(dataProducao);

            var itens = indexacoes.Where(x => x.Documento.TipoDocumento.TipoDocumentoEhMestre).Select(x => x.Campo.Description).Distinct().Select(campoNome => new ItemRelatorioDeReconhecimentoPorCampo
            {
                CampoNome = campoNome,
                PercentualAcertos = this.PercenturalAcertos(indexacoes, campoNome),
                PercentualFalsoPositivo = this.PercenturalFalsoPositivo(indexacoes, campoNome),
                PercentualBatimento = this.PercenturalBatimento(indexacoes, campoNome)
            }).OrderBy(x => x.CampoNome).ToList();
            
            return new RelatorioDeReconhecimentoPorCampo
            {
                DataProducaoInicial = dataProducao,
                Itens = itens
            };
        }

        private long PercenturalAcertos(IList<Indexacao> indexacoes, string campoNome)
        {
            var indexacoesDoCampo = indexacoes.Where(x => x.Campo.Description == campoNome && x.Documento.TipoDocumento.TipoDocumentoEhMestre);
            var quantidadeAcerto = indexacoesDoCampo.Count(x => x.Valor1UtilizadoNoValorFinal());

            return indexacoesDoCampo.Any() ? 
                quantidadeAcerto * 100 / indexacoesDoCampo.Count() :
                0;
        }

        private long PercenturalFalsoPositivo(IList<Indexacao> indexacoes, string campoNome)
        {
            var indexacoesDoCampo = indexacoes.Where(x => x.Campo.Description == campoNome && x.Documento.TipoDocumento.TipoDocumentoEhMestre);
            var quantidadeErro = indexacoesDoCampo.Count(x => x.Valor1UtilizadoNoValorFinal() == false);

            return indexacoesDoCampo.Any() ? 
                quantidadeErro * 100 / indexacoesDoCampo.Count() :
                0;
        }

        private long PercenturalBatimento(IList<Indexacao> indexacoes, string campoNome)
        {
            var indexacoesDoCampo = indexacoes.Where(x => x.Campo.Description == campoNome && x.Documento.TipoDocumento.TipoDocumentoEhMestre);
            var quantiadeBatidos = indexacoesDoCampo.Count(x => x.Valor2UtilizadoNoValorFinal());

            return indexacoesDoCampo.Any() ? 
                quantiadeBatidos * 100 / indexacoesDoCampo.Count() :
                0;
        }
    }
}