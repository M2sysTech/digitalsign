namespace Veros.Paperless.Model.Servicos.Batimento
{
    using System;
    using System.Collections.Generic;
    using Entidades;
    using Framework;
    using Repositorios;
    using Validacao;

    /// <summary>
    /// TODO: Fortalecer testes
    /// </summary>
    public class BatimentoEComplementacaoRg : BatimentoEComplementacaoDocumentoBase
    {
        private readonly ILogBatimentoServico logBatimentoServico;
        private readonly IValidaDadosReconhecidosCampoData validaDadosReconhecidosCampoData;

        public BatimentoEComplementacaoRg(
            ICampoRepositorio campoRepositorio,
            IGravaLogDoDocumentoServico gravaLogDocumentoServico,
            IIndexacaoRepositorio indexacaoRepositorio,
            ILogBatimentoServico logBatimentoServico,
            IValidaDadosReconhecidosCampoData validaDadosReconhecidosCampoData)
            : base(campoRepositorio, gravaLogDocumentoServico, indexacaoRepositorio)
        {
            this.logBatimentoServico = logBatimentoServico;
            this.validaDadosReconhecidosCampoData = validaDadosReconhecidosCampoData;
            this.PodeInserirCampoNaoReconhecido = true;
        }

        protected override Indexacao AdicionaNovaIndexacaoParaCampoReconhecido(
            Documento documento,
            ValorReconhecido valorReconhecido,
            Campo campoDoDocumento)
        {
            var indexacao = new Indexacao
            {
                Campo = campoDoDocumento,
                DataPrimeiraDigitacao = DateTime.Now
            };

            documento.AdicionaIndexacao(indexacao);

            if (campoDoDocumento.Id != Campo.CampoDtEmissaoNumerica &&
                campoDoDocumento.Id != Campo.CampoDtNascimentoNumerica)
            {
                return indexacao;
            }

            //// so deve inserir para campos CampoDt*******Numerica se tiver
            ////  reconhecido o conteúdo do respectivo campo original
            Indexacao indexacaoCampoOriginal = null;

            if (campoDoDocumento.Id == Campo.CampoDtEmissaoNumerica)
            {
                indexacaoCampoOriginal = this.indexacaoRepositorio
                    .ObterPorCampoDeUmDocumento(Campo.CampoDataEmissao, documento);
            }

            if (campoDoDocumento.Id == Campo.CampoDtNascimentoNumerica)
            {
                indexacaoCampoOriginal = this.indexacaoRepositorio
                    .ObterPorCampoDeUmDocumento(Campo.CampoDataNascimento, documento);
            }

            if (this.CampoOriginalFoiReconhecido(indexacaoCampoOriginal) == false)
            {
                indexacao.PrimeiroValor = string.Empty;
                indexacao.ValorFinal = string.Empty;
                indexacao.OcrComplementou = false;

                return indexacao;
            }

            try
            {
                var campoMesEhNumero = this.validaDadosReconhecidosCampoData
                    .PossuiMesNumerico(indexacaoCampoOriginal);

                if (campoMesEhNumero)
                {
                    indexacao.PrimeiroValor = "S";
                    indexacao.ValorFinal = "S";
                    indexacao.OcrComplementou = true;
                }

                return indexacao;
            }
            catch (Exception exception)
            {
                Log.Application.Info(exception.Message);
            }

            if (valorReconhecido.NaoTemConteudo() || valorReconhecido.Value == "E")
            {
                indexacao.PrimeiroValor = string.Empty;
                indexacao.ValorFinal = string.Empty;
                indexacao.OcrComplementou = false;

                return indexacao;
            }

            indexacao.PrimeiroValor = valorReconhecido.Value;
            indexacao.ValorFinal = valorReconhecido.Value;
            indexacao.OcrComplementou = true;

            return indexacao;
        }

        [Obsolete("Utilizando apenas as rotinas para inserção de indexacao. Para batimento utilize classe BaterDocumento")]
        protected override Indexacao ModificaIndexacaoDoCampoReconhecido(
            Indexacao indexacao,
            ValorReconhecido valorReconhecido,
            IList<PalavraReconhecida> palavras)
        {
            return indexacao;
        }

        private bool CampoOriginalFoiReconhecido(Indexacao indexacaoCampoOriginal)
        {
            return indexacaoCampoOriginal != null && indexacaoCampoOriginal.OcrComplementou;
        }
    }
}