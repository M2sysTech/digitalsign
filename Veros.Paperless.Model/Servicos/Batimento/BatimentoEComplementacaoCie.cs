namespace Veros.Paperless.Model.Servicos.Batimento
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Comparacao;
    using Entidades;
    using Framework;
    using Repositorios;

    public class BatimentoEComplementacaoCie : IBatimentoEComplementacaoDocumentoServico
    {
        private readonly ICampoRepositorio campoRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDocumentoServico;
        private readonly IIndexacaoRepositorio indexacaoRepositorio;

        public BatimentoEComplementacaoCie(
            ICampoRepositorio campoRepositorio, 
            IGravaLogDoDocumentoServico gravaLogDocumentoServico, 
            IIndexacaoRepositorio indexacaoRepositorio)
        {
            this.campoRepositorio = campoRepositorio;
            this.gravaLogDocumentoServico = gravaLogDocumentoServico;
            this.indexacaoRepositorio = indexacaoRepositorio;
        }

        public void Execute(Documento documento, ImagemReconhecida imagemReconhecida, List<int> camposBatidosId = null)
        {
            if (imagemReconhecida == null)
            {
                return;
            }

            var valoresReconhecidos = imagemReconhecida.ValoresReconhecidos;

            if (valoresReconhecidos == null)
            {
                return;
            }

            var camposDoDocumento = this.campoRepositorio.ObterPorTipoDocumentoComMapeamentoOcr(documento.TipoDocumento.Id);

            foreach (var valorReconhecido in valoresReconhecidos)
            {
                if (camposDoDocumento == null)
                {
                    break;
                }

                if (camposDoDocumento.Count == 0)
                {
                    break;
                }

                if (string.IsNullOrEmpty(valorReconhecido.Value))
                {
                    continue;
                }

                var encontrouMapeamento = false;
                foreach (var campoDoDocumento in camposDoDocumento)
                {
                    if (campoDoDocumento
                        .EstaMapeadoPara(valorReconhecido.CampoTemplate, valorReconhecido.TemplateName) == false)
                    {
                        continue;
                    }

                    encontrouMapeamento = true;
                    Log.Application.DebugFormat("Campo {0} mapeado para campo {1} do template {2}",
                        campoDoDocumento.Description,
                        valorReconhecido.CampoTemplate,
                        valorReconhecido.TemplateName);

                    var indexacao = documento.Indexacao.FirstOrDefault(x => x.Campo == campoDoDocumento);

                    if (string.IsNullOrEmpty(indexacao.ValorFinal) == false || string.IsNullOrWhiteSpace(indexacao.ValorFinal) == false)
                    {
                        continue;
                    }

                    var campoReconhecido = valorReconhecido.Value;

                    if (indexacao.Campo.TipoDado == TipoDado.DateTime)
                    {
                        campoReconhecido = campoReconhecido.FormatarData();

                        if (string.IsNullOrEmpty(campoReconhecido))
                        {
                            continue;
                        }
                    }

                    if (indexacao.BateCom(campoReconhecido))
                    {
                        indexacao.PrimeiroValor = indexacao.SegundoValor;
                        indexacao.ValorFinal = indexacao.SegundoValor;
                        indexacao.ValorUtilizadoParaValorFinal = ValorUtilizadoParaValorFinal.SegundoValor;
                        indexacao.OcrComplementou = true;
                        indexacao.DataPrimeiraDigitacao = DateTime.Now;

                        this.gravaLogDocumentoServico.Executar(
                            LogDocumento.AcaoDocumentoOcr,
                            documento.Id,
                            string.Format("Campo [{0}] complementado pelo ocr. Valor:[{1}]", campoDoDocumento.Description, BatimentoFullText.LimitaString(indexacao.SegundoValor.Trim(), 99)));
                    }
                    else
                    {
                        indexacao.PrimeiroValor = string.Empty;
                        indexacao.ValorFinal = string.Empty;
                        indexacao.DataPrimeiraDigitacao = DateTime.Now;
                    }

                    this.indexacaoRepositorio.Salvar(indexacao);
                }
            }
        }
    }
}