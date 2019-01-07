namespace Veros.Paperless.Model.Servicos.Batimento
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Comparacao;
    using Entidades;
    using Framework;
    using Repositorios;

    public class BatimentoEComplementacaoBoleto :
        IBatimentoEComplementacaoDocumentoServico
    {
         private readonly ICampoRepositorio campoRepositorio;
        private readonly IIndexacaoRepositorio indexacaoRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDocumentoServico;

        public BatimentoEComplementacaoBoleto(
            ICampoRepositorio campoRepositorio,
            IIndexacaoRepositorio indexacaoRepositorio, IGravaLogDoDocumentoServico gravaLogDocumentoServico)
        {
            this.campoRepositorio = campoRepositorio;
            this.indexacaoRepositorio = indexacaoRepositorio;
            this.gravaLogDocumentoServico = gravaLogDocumentoServico;
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

                    if (indexacao == null)
                    {
                        continue;
                    }

                    if (string.IsNullOrEmpty(indexacao.ValorFinal) == false || string.IsNullOrWhiteSpace(indexacao.ValorFinal) == false)
                    {
                        continue;
                    }

                    var listaDeCamposDataEmissaoDosBoletos = new List<int>()
                    {
                        Campo.CampoBoletoComAutenticDataEmissao,
                        Campo.CampoBoletoSemAutenticDataEmissao
                    };

                    ////tratamento especial para datas de emissao dos boletos
                    if (listaDeCamposDataEmissaoDosBoletos.Contains(campoDoDocumento.Id) && campoDoDocumento.PodeInserirPeloOcr && string.IsNullOrEmpty(valorReconhecido.Value) == false )
                    {
                        string dataEmissao; 
                        if (this.ValidarDataEmissao(valorReconhecido.Value, out dataEmissao))
                        {
                            indexacao.PrimeiroValor = dataEmissao;
                            indexacao.ValorFinal = dataEmissao;
                            indexacao.ValorUtilizadoParaValorFinal = ValorUtilizadoParaValorFinal.PrimeiroValor;
                            indexacao.OcrComplementou = true;
                            indexacao.DataPrimeiraDigitacao = DateTime.Now;

                            this.gravaLogDocumentoServico.Executar(
                                LogDocumento.AcaoDocumentoOcr,
                                documento.Id,
                                string.Format("Campo [{0}] complementado pelo ocr. Valor:[{1}]", campoDoDocumento.Description, BatimentoFullText.LimitaString(dataEmissao.Trim(), 99)));
                            this.indexacaoRepositorio.Salvar(indexacao);

                            continue;
                        }
                    }

                    ////tratamento especial para campo de codigo de barras
                    if (indexacao.Campo.Id == Campo.CampoBoletoComAutenticCodBarras || indexacao.Campo.Id == Campo.CampoBoletoSemAutenticCodBarras)
                    {
                        string linhaDigitavelLimpa;
                        if (this.ExtrairNumerosDoCodigoDeBarras(valorReconhecido.Value, out linhaDigitavelLimpa))
                        {
                            indexacao.PrimeiroValor = linhaDigitavelLimpa;
                            indexacao.ValorFinal = linhaDigitavelLimpa;
                            indexacao.ValorUtilizadoParaValorFinal = ValorUtilizadoParaValorFinal.PrimeiroValor;
                            indexacao.OcrComplementou = true;
                            indexacao.DataPrimeiraDigitacao = DateTime.Now;

                            this.gravaLogDocumentoServico.Executar(
                                LogDocumento.AcaoDocumentoOcr,
                                documento.Id,
                                string.Format("Campo [{0}] complementado pelo ocr. Valor:[{1}]", campoDoDocumento.Description, BatimentoFullText.LimitaString(linhaDigitavelLimpa.Trim(), 99)));
                        }
                    }
                    else
                    {
                        if (indexacao.BateCom(valorReconhecido.Value))
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
                    }

                    this.indexacaoRepositorio.Salvar(indexacao);
                }

                if (encontrouMapeamento == false)
                {
                    Log.Application.DebugFormat("ALERTA: campo {0} do template {1} não está mapeado para nenhum campo",
                        valorReconhecido.CampoTemplate,
                        valorReconhecido.TemplateName);
                }
            }

            ////batimento com fullTExt
            try
            {
                this.BaterUsandoFullText(documento, imagemReconhecida.Palavras);
            }
            catch (Exception exception)
            {
                Log.Application.Error(string.Format("Erro ao processar batimento full text de Atestado de Matricula - Documento #{0} ", documento.Id), exception);
            }
        }

        public void BaterUsandoFullText(Documento documento, IList<PalavraReconhecida> palavras)
        {
            if (palavras == null)
            {
                return;
            }

            if (palavras.Count == 0)
            {
                return;
            }

            var listaLimpa = new List<dynamic>();
            foreach (var regiao in palavras)
            {
                var palavraAtual = Equivalencia.LimpaConteudoNome(regiao.Texto);
                if (string.IsNullOrEmpty(palavraAtual) == false)
                {
                    ////listaLimpa.Add(new dynamic()
                    ////{
                    ////    Texto = palavraAtual,
                    ////    Localizacao = regiao.Localizacao
                    ////});
                }
            }

            Log.Application.DebugFormat("Documento {0} - Palavras: {1} ", documento.Id, BatimentoFullText.ImprimirLista(listaLimpa));

            if (listaLimpa.Count == 0)
            {
                return;
            }

            var camposSemValor1 = this.indexacaoRepositorio.ObterComMapeamentoPorDocumento(documento);
            if (camposSemValor1 == null)
            {
                return;
            }

            if (camposSemValor1.Count == 0)
            {
                return;
            }

            var ultimoCampo = new Campo();
            foreach (var indexacao in camposSemValor1)
            {
                if (ultimoCampo.Id == indexacao.Campo.Id || indexacao.PrimeiroValor != null)
                {
                    continue;
                }

                ultimoCampo = indexacao.Campo;
                var deveSalvar = false;
                switch (indexacao.Campo.Id)
                {
                    default:
                        if (indexacao.BateComFullText(listaLimpa))
                        {
                            deveSalvar = true;
                        }

                        break;
                }

                if (deveSalvar)
                {
                    indexacao.PrimeiroValor = indexacao.SegundoValor;
                    indexacao.ValorFinal = indexacao.SegundoValor;
                    indexacao.ValorUtilizadoParaValorFinal = ValorUtilizadoParaValorFinal.SegundoValor;
                    indexacao.OcrComplementou = true;
                    indexacao.DataPrimeiraDigitacao = DateTime.Now;

                    this.gravaLogDocumentoServico.Executar(
                        LogDocumento.AcaoDocumentoOcr,
                        documento.Id,
                        string.Format("Campo [{0}] complementado pelo ocr-FT. Valor:[{1}]", indexacao.Campo.Description, BatimentoFullText.LimitaString(indexacao.SegundoValor.Trim(), 99)));

                    this.indexacaoRepositorio.Salvar(indexacao);
                }
            }
        }

        private bool ExtrairNumerosDoCodigoDeBarras(string valoresReconhecido, out string linhaDigitavelLimpa)
        {
            var linhaDigitavelValidada2 = new ConversorLinhaDigitavel(valoresReconhecido);
            linhaDigitavelLimpa = linhaDigitavelValidada2.RetornaLinhaDigitavelEValida();
            if (string.IsNullOrEmpty(linhaDigitavelLimpa))
            {
                return false;
            }

            return true;
        }

        private bool ValidarDataEmissao(string valorReconhecido, out string dataEmissao)
        {
            dataEmissao = string.Empty;

            if (string.IsNullOrEmpty(valorReconhecido))
            {
                return false;
            }

            Regex pattern = new Regex(@"([0-9]{1,2})/([0-9]{1,2})/([0-9]{2,4})");
            Match match = pattern.Match(valorReconhecido);
            if (match.Success == false)
            {
                return false;
            }

            DateTime dataValida;
            if (DateTime.TryParse(valorReconhecido, out dataValida) == false)
            {
                return false;
            }

            dataEmissao = dataValida.ToString("ddMMyyyy");
            return true;
        }
    }
}