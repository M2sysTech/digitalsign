namespace Veros.Paperless.Model.Servicos.Batimento
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Comparacao;
    using Entidades;
    using Framework;
    using Repositorios;

    public class BatimentoEComplementacaoAtestadoMatricula :
        IBatimentoEComplementacaoDocumentoServico
    {
         private readonly ICampoRepositorio campoRepositorio;
        private readonly IIndexacaoRepositorio indexacaoRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDocumentoServico;

        public BatimentoEComplementacaoAtestadoMatricula(
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
                    var deveInserir = false;
                    
                    if (indexacao == null)
                    {
                        if (campoDoDocumento.PodeInserirPeloOcr && string.IsNullOrEmpty(valorReconhecido.Value) == false )
                        {
                            deveInserir = true;
                        }
                        else
                        {
                            continue;    
                        }
                    }

                    if (deveInserir)
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
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(indexacao.ValorFinal) == false || string.IsNullOrWhiteSpace(indexacao.ValorFinal) == false)
                        {
                            continue;
                        }

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

            ////batimento especial para campos de data (validade e emissao)
            try
            {
                this.BatimentoDatasDoAtestado(documento, valoresReconhecidos, camposDoDocumento);
            }
            catch (Exception exception)
            {
                Log.Application.Error(string.Format("Erro ao processar batimento de data do Atestado de Matricula- Documento #{0} ", documento.Id), exception);
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

        ////batimento especial para campos de data (validade e emissao)
        public void BatimentoDatasDoAtestado(Documento documento, IList<ValorReconhecido> valoresReconhecidos, IList<Campo> camposDoDocumento)
        {
            var indexacaoSolicitacao = this.indexacaoRepositorio.ObterPorCampoDeUmDocumento(Campo.CampoDataSolicitacaoAtestadoMatricula, documento);
            if (indexacaoSolicitacao == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(indexacaoSolicitacao.SegundoValor))
            {
                return;
            }

            DateTime dataSolicitacao;
            if (DateTime.TryParseExact(indexacaoSolicitacao.SegundoValor, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataSolicitacao) == false)
            {
                Log.Application.Info(string.Format("Formato da data de solicitação não bateu com ddMMyyyy ao processar batimento da data no Atestado de Matricula - Documento #{0} ", documento.Id));
                return;
            }

            var indexacaoValidade = this.indexacaoRepositorio.ObterPorCampoDeUmDocumento(Campo.CampoDataValidadeAtestadoMatricula, documento);
            if (string.IsNullOrEmpty(indexacaoValidade.PrimeiroValor))
            {
                var dataSelecionada = this.BaterCamposParaDataValidade(valoresReconhecidos, dataSolicitacao);    
                if (dataSelecionada != new DateTime(1900, 1, 1))
                {
                    indexacaoValidade.PrimeiroValor = dataSelecionada.ToString("ddMMyyyy");
                    indexacaoValidade.ValorFinal = dataSelecionada.ToString("ddMMyyyy");
                    indexacaoValidade.ValorUtilizadoParaValorFinal = ValorUtilizadoParaValorFinal.PrimeiroValor;
                    indexacaoValidade.OcrComplementou = true;
                    indexacaoValidade.DataPrimeiraDigitacao = DateTime.Now;

                    this.gravaLogDocumentoServico.Executar(
                        LogDocumento.AcaoDocumentoOcr,
                        documento.Id,
                        string.Format("Campo [Data Validade] complementado pelo ocr-DT. Valor:[{0}]", dataSelecionada.ToString("ddMMyyyy")));

                    this.indexacaoRepositorio.Salvar(indexacaoValidade);                    
                }
            }

            var indexacaoEmissao = this.indexacaoRepositorio.ObterPorCampoDeUmDocumento(Campo.CampoDataEmissaoAtestadoMatricula, documento);
            if (string.IsNullOrEmpty(indexacaoEmissao.PrimeiroValor))
            {
                var dataSelecionada = this.BaterCamposParaDataEmissao(valoresReconhecidos, dataSolicitacao);
                if (dataSelecionada != new DateTime(1900, 1, 1))
                {
                    indexacaoEmissao.PrimeiroValor = dataSelecionada.ToString("ddMMyyyy");
                    indexacaoEmissao.ValorFinal = dataSelecionada.ToString("ddMMyyyy");
                    indexacaoEmissao.ValorUtilizadoParaValorFinal = ValorUtilizadoParaValorFinal.PrimeiroValor;
                    indexacaoEmissao.OcrComplementou = true;
                    indexacaoEmissao.DataPrimeiraDigitacao = DateTime.Now;

                    this.gravaLogDocumentoServico.Executar(
                        LogDocumento.AcaoDocumentoOcr,
                        documento.Id,
                        string.Format("Campo [Data Emissão] complementado pelo ocr-DT. Valor:[{0}]", dataSelecionada.ToString("ddMMyyyy")));

                    this.indexacaoRepositorio.Salvar(indexacaoEmissao);                    
                }
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

        private DateTime BaterCamposParaDataValidade(IList<ValorReconhecido> valoresReconhecidos, DateTime dataSolicitacao)
        {
            var lblValidade = valoresReconhecidos.FirstOrDefault(x => x.CampoTemplate.ToLower() == "lblvalidade");
            if (lblValidade != null)
            {
                if (!string.IsNullOrEmpty(lblValidade.Value))
                {
                    DateTime dataValidade;
                    if (DateTime.TryParse(lblValidade.Value, out dataValidade))
                    {
                        if (dataValidade > dataSolicitacao)
                        {
                            return dataValidade;
                        }
                    }
                }
            }

            var dataRetorno = this.AvaliarRegexAnoComCampo("regletivo", valoresReconhecidos, dataSolicitacao);
            if (dataRetorno > dataSolicitacao)
            {
                return dataRetorno;
            }

            var dataRetornoPeriodo = this.AvaliarRegexAnoComCampo("regperiodo", valoresReconhecidos, dataSolicitacao);
            if (dataRetornoPeriodo > dataSolicitacao)
            {
                return dataRetornoPeriodo;
            }

            return new DateTime(1900, 1, 1);
        }

        private DateTime AvaliarRegexAnoComCampo(string regperiodo, IList<ValorReconhecido> valoresReconhecidos, DateTime dataSolicitacao)
        {
            var campoReconhecido = valoresReconhecidos.FirstOrDefault(x => x.CampoTemplate.ToLower() == regperiodo.ToLower());
            if (campoReconhecido != null)
            {
                if (!string.IsNullOrEmpty(campoReconhecido.Value))
                {
                    Regex pattern = new Regex(@"20[0-9]{1,2}");
                    Match match = pattern.Match(campoReconhecido.Value);
                    if (string.IsNullOrEmpty(match.Value) == false)
                    {
                        int ano;
                        if (int.TryParse(match.Groups[0].Value, out ano))
                        {
                            if (ano >= dataSolicitacao.Year)
                            {
                                DateTime dataConvertida;
                                if (DateTime.TryParse(string.Format("31/12/{0}", ano), out dataConvertida))
                                {
                                    return dataConvertida;
                                }
                            }
                        }
                    }
                }
            }

            return new DateTime(1900, 1, 1);
        }

        private DateTime BaterCamposParaDataEmissao(IList<ValorReconhecido> valoresReconhecidos, DateTime dataSolicitacao)
        {
            var campoReconhecidoDataEmissaoExtenso = valoresReconhecidos.FirstOrDefault(x => x.CampoTemplate.ToLower() == "dataemissaoext");
            if (campoReconhecidoDataEmissaoExtenso != null)
            {
                if (!string.IsNullOrEmpty(campoReconhecidoDataEmissaoExtenso.Value))
                {
                    DateTime dataEmissao;
                    if (this.ConverterExtensoParaData(campoReconhecidoDataEmissaoExtenso.Value, out dataEmissao))
                    {
                        if (dataEmissao <= dataSolicitacao)
                        {
                            return dataEmissao;
                        }
                    }
                }
            }

            var dataRetornoLetivo = this.AvaliarRegexAnoComCampo("regletivo", valoresReconhecidos, dataSolicitacao);
            if (dataRetornoLetivo > dataSolicitacao)
            {
                //// diferente da data de VALIDADE, aqui na data de EMISSAO é retornado a data da SOLICITAÇÃO
                return dataSolicitacao;
            }

            var dataRetornoPeriodo = this.AvaliarRegexAnoComCampo("regperiodo", valoresReconhecidos, dataSolicitacao);
            if (dataRetornoPeriodo > dataSolicitacao)
            {
                //// diferente da data de VALIDADE, aqui na data de EMISSAO é retornado a data da SOLICITAÇÃO
                return dataSolicitacao;
            }

            return new DateTime(1900, 1, 1);
        }

        private bool ConverterExtensoParaData(string dataExtenso, out DateTime dataValidade)
        {
            if (!string.IsNullOrEmpty(dataExtenso))
            {
                Regex pattern = new Regex(@"([0-9]{1,2})(\s[a-z]{2,3}\s)(janeiro|fevereiro|março|marco|abril|maio|junho|julho|agosto|setembro|outubro|novembro|dezembro)(\s[a-z]{2,3}\s)(20[0-9]{2})");
                Match match = pattern.Match(dataExtenso);
                if (string.IsNullOrEmpty(match.Value) == false)
                {
                    int dia;
                    int mes;
                    int ano;
                    if (int.TryParse(match.Groups[1].Value, out dia))
                    {
                        switch (match.Groups[3].Value)
                        {
                            case "janeiro":
                                mes = 1;
                                break;
                            case "fevereiro":
                                mes = 2;
                                break;
                            case "março":
                            case "marco":
                                mes = 3;
                                break;
                            case "abril":
                                mes = 4;
                                break;
                            case "maio":
                                mes = 5;
                                break;
                            case "junho":
                                mes = 6;
                                break;
                            case "julho":
                                mes = 7;
                                break;
                            case "agosto":
                                mes = 8;
                                break;
                            case "setembro":
                                mes = 9;
                                break;
                            case "outubro":
                                mes = 10;
                                break;
                            case "novembro":
                                mes = 11;
                                break;
                            case "dezembro":
                                mes = 12;
                                break;
                            default:
                                mes = 0;
                                break;
                        }

                        if (mes > 0)
                        {
                            if (int.TryParse(match.Groups[5].Value, out ano))
                            {
                                if (DateTime.TryParse(string.Format("{0}/{1}/{2}", dia, mes, ano), out dataValidade))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            dataValidade = new DateTime(1900, 1, 1);
            return false;
        }
    }
}