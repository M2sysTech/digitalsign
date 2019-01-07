namespace Veros.Paperless.Model.Servicos.Batimento
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Comparacao;
    using Entidades;
    using Repositorios;

    public class BatimentoEComplementacaoComprovanteResidenciaServico
        : IBatimentoEComplementacaoDocumentoServico
    {
        private readonly ICepRepositorio cepRepositorio;
        private readonly IIndexacaoRepositorio indexacaoRepositorio;
        private readonly ICampoRepositorio campoRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDocumentoServico;
        private readonly ComparadorDeNumeros comparadorDeNumeros;

        public BatimentoEComplementacaoComprovanteResidenciaServico(
            ICepRepositorio cepRepositorio,
            IIndexacaoRepositorio indexacaoRepositorio,
            ICampoRepositorio campoRepositorio,
            IGravaLogDoDocumentoServico gravaLogDocumentoServico,
            ComparadorDeNumeros comparadorDeNumeros)
        {
            this.cepRepositorio = cepRepositorio;
            this.indexacaoRepositorio = indexacaoRepositorio;
            this.campoRepositorio = campoRepositorio;
            this.gravaLogDocumentoServico = gravaLogDocumentoServico;
            this.comparadorDeNumeros = comparadorDeNumeros;
        }

        [Obsolete("Utilizando apenas as rotinas para inserção de indexacao. Para batimento utilize classe BaterDocumento")]
        public void Execute(Documento documento, ImagemReconhecida imagemReconhecida, List<int> camposBatidosId = null)
        {
            if (imagemReconhecida == null)
            {
                return;
            }

            var valoresReconhecidos = imagemReconhecida.ValoresReconhecidos;

            if (valoresReconhecidos == null || valoresReconhecidos.Count == 0)
            {
                return;
            }

            var camposDoDocumento = this.campoRepositorio
                .ObterPorTipoDocumento(documento.TipoDocumento);

            foreach (var campoDoDocumento in camposDoDocumento)
            {
                var indexacao = documento.Indexacao.FirstOrDefault(x => x.Campo == campoDoDocumento);

                if (!campoDoDocumento.PodeInserirPeloOcr)
                {
                    continue;
                }

                if (indexacao == null)
                {
                    indexacao = new Indexacao
                    {
                        Campo = campoDoDocumento,
                        Documento = documento,
                    };

                    documento.Indexacao.Add(indexacao);
                }
                else
                {
                    //// ja possui indexacao cadastrada, verificar se ja tem conteudo
                    if (!string.IsNullOrEmpty(indexacao.PrimeiroValor))
                    {
                        continue;
                    }
                }

                this.SalvarIndexacaoSemComparacao(indexacao, valoresReconhecidos);
            }
        }

        private void SalvarIndexacaoSemComparacao(Indexacao indexacao, IList<ValorReconhecido> valoresReconhecidos)
        {
            foreach (var valorReconhecido in valoresReconhecidos)
            {
                if (indexacao.Campo.EstaMapeadoPara(
                    valorReconhecido.CampoTemplate,
                    valorReconhecido.TemplateName) == false)
                {
                    continue;
                }

                if (string.IsNullOrEmpty(valorReconhecido.Value) == false)
                {
                    indexacao.PrimeiroValor = valorReconhecido.Value;
                    indexacao.OcrComplementou = true;
                    indexacao.DataPrimeiraDigitacao = DateTime.Now;
                    indexacao.UsuarioPrimeiroValor = -2;
                    this.indexacaoRepositorio.Salvar(indexacao);
                }
            }
        }
    }
}