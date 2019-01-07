namespace Veros.Paperless.Model.Servicos.Batimento
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Framework;
    using Repositorios;

    /// <summary>
    /// TODO: Fortalecer testes
    /// </summary>
    public class BatimentoEComplementacaoCnh :
        IBatimentoEComplementacaoDocumentoServico
    {
        private readonly ICampoRepositorio campoRepositorio;
        private readonly IIndexacaoRepositorio indexacaoRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDocumentoServico;
        private readonly BatimentoFullText batimentoFullText;
        private readonly IMapeamentoCampoRepositorio mapeamentoCampoRepositorio;

        public BatimentoEComplementacaoCnh(
            ICampoRepositorio campoRepositorio,
            IIndexacaoRepositorio indexacaoRepositorio,
            IGravaLogDoDocumentoServico gravaLogDocumentoServico,
            BatimentoFullText batimentoFullText,
            IMapeamentoCampoRepositorio mapeamentoCampoRepositorio)
        {
            this.campoRepositorio = campoRepositorio;
            this.indexacaoRepositorio = indexacaoRepositorio;
            this.gravaLogDocumentoServico = gravaLogDocumentoServico;
            this.batimentoFullText = batimentoFullText;
            this.mapeamentoCampoRepositorio = mapeamentoCampoRepositorio;
        }

        [Obsolete("Utilizando apenas as rotinas para inserção de indexacao. Para batimento utilize classe BaterDocumento")]
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

            foreach (var campoDoDocumento in camposDoDocumento)
            {
                if (this.mapeamentoCampoRepositorio.ExisteMapeamentoParaCampo(campoDoDocumento.Id) == false)
                {
                    Log.Application.DebugFormat("ALERTA: campo {0} não está mapeado para nenhum template", campoDoDocumento.Description);
                    continue;
                }

                var indexacao = documento.Indexacao.FirstOrDefault(x => x.Campo == campoDoDocumento);

                //// tenta bater com ancoragem ABBYY
                if (this.BatimentoAncoragemDeCnh(indexacao, campoDoDocumento, valoresReconhecidos, documento))
                {
                    this.gravaLogDocumentoServico.Executar(
                        LogDocumento.AcaoDocumentoOcr,
                        documento.Id,
                        string.Format("Campo [{0}] complementado pelo ocr. Valor:[{1}]", indexacao.Campo.Description, BatimentoFullText.LimitaString(indexacao.SegundoValor.Trim(), 99)));
                    this.indexacaoRepositorio.Salvar(indexacao);
                }
            }
        }

        private bool BatimentoAncoragemDeCnh(Indexacao indexacao, Campo campoDoDocumento, IList<ValorReconhecido> valoresReconhecidos, Documento documento)
        {
            foreach (var valorReconhecido in valoresReconhecidos)
            {
                if (string.IsNullOrEmpty(valorReconhecido.Value))
                {
                    continue;
                }

                if (campoDoDocumento
                    .EstaMapeadoPara(valorReconhecido.CampoTemplate, valorReconhecido.TemplateName) == false)
                {
                    continue;
                }

                Log.Application.DebugFormat("Campo {0} mapeado para campo {1} do template {2}",
                    campoDoDocumento.Description,
                    valorReconhecido.CampoTemplate,
                    valorReconhecido.TemplateName);

                if (indexacao == null)
                {
                    if (campoDoDocumento.PodeInserirPeloOcr && string.IsNullOrEmpty(valorReconhecido.Value) == false)
                    {
                        //// TODO: revisar -2
                        indexacao = new Indexacao()
                        {
                            Campo = campoDoDocumento,
                            Documento = documento,
                            PrimeiroValor = valorReconhecido.Value,
                            OcrComplementou = true,
                            DataPrimeiraDigitacao = DateTime.Now,
                            UsuarioPrimeiroValor = -2
                        };

                        documento.Indexacao.Add(indexacao);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}