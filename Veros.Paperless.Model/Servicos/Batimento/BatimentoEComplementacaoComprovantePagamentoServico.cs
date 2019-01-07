namespace Veros.Paperless.Model.Servicos.Batimento
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Comparacao;
    using Entidades;
    using Framework;
    using Repositorios;

    public class BatimentoEComplementacaoComprovantePagamentoServico
        : IBatimentoEComplementacaoDocumentoServico
    {
        private readonly ICepRepositorio cepRepositorio;
        private readonly IIndexacaoRepositorio indexacaoRepositorio;
        private readonly ICampoRepositorio campoRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDocumentoServico;
        private readonly ComparadorDeNumeros comparadorDeNumeros;

        public BatimentoEComplementacaoComprovantePagamentoServico(
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

        public void Execute(Documento documento, ImagemReconhecida imagemReconhecida, List<int> camposBatidosId = null)
        {
            if (imagemReconhecida == null)
            {
                return;
            }

            var valoresReconhecidos = imagemReconhecida.ValoresReconhecidos;
            
            var camposDoDocumento = this.campoRepositorio.ObterPorTipoDocumentoComMapeamentoOcr(documento.TipoDocumento.Id);

            foreach (var campoDoDocumento in camposDoDocumento)
            {
                var indexacao = documento.Indexacao.FirstOrDefault(x => x.Campo == campoDoDocumento);
                var deveInserir = false;

                if (indexacao == null)
                {
                    if (campoDoDocumento.PodeInserirPeloOcr)
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
                    indexacao = new Indexacao()
                    {
                        Campo = campoDoDocumento,
                        Documento = documento,
                        OcrComplementou = true,
                        DataPrimeiraDigitacao = DateTime.Now,
                        UsuarioPrimeiroValor = -2
                    };

                    documento.Indexacao.Add(indexacao);
                    this.SalvarIndexacaoSemComparacao(indexacao, valoresReconhecidos);
                }
                else
                {
                    //// tratamento para validação de código de barras: se campo estiver mapeado e for um codigo válido, pode inserir o que o OCR achou!
                    foreach (var valorReconhecido in valoresReconhecidos)
                    {
                        if (campoDoDocumento.EstaMapeadoPara(valorReconhecido.CampoTemplate, valorReconhecido.TemplateName) == false)
                        {
                            continue;
                        }

                        try
                        {
                            string linhaDigitavelLimpa;
                            if (this.ExtrairNumerosDoCodigoDeBarras(valorReconhecido.Value, out linhaDigitavelLimpa))
                            {
                                indexacao.Campo = campoDoDocumento;
                                indexacao.Documento = documento;
                                indexacao.OcrComplementou = true;
                                indexacao.DataPrimeiraDigitacao = DateTime.Now;
                                indexacao.UsuarioPrimeiroValor = -2;
                                
                                this.SalvarIndexacao(indexacao, linhaDigitavelLimpa);
                                break;
                            }
                        }
                        catch (Exception exception)
                        {
                            Log.Application.Error(string.Format("Erro ao processar batimento de Código de Barras de Comprovante Pagto - Documento #{0} ", documento.Id), exception);
                        }
                    }
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

        private bool CompareNumeros(string primeiroValor, string segundoValor)
        {
            var valueWords = primeiroValor.Split(Convert.ToChar(" "));
            if (valueWords.Length != 1)
            {
                return false;
            }

            double numero;
            var numeral = double.TryParse(primeiroValor, out numero);
            if (!numeral)
            {
                return false;
            }

            if (segundoValor.ToUpper().Contains(numero.ToString()))
            {
                return true;
            }

            return false;
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
                    this.indexacaoRepositorio.Salvar(indexacao);
                }
            }
        }

        private void SalvarIndexacao(Indexacao indexacao, string valor)
        {
            if (string.IsNullOrEmpty(indexacao.ValorFinal) == false || string.IsNullOrWhiteSpace(indexacao.ValorFinal) == false)
            {
                return;
            }

            indexacao.PrimeiroValor = valor;
            
            if (string.IsNullOrEmpty(valor) == false)
            {
                indexacao.PrimeiroValor = valor;
                indexacao.ValorFinal = valor;
                indexacao.OcrComplementou = true;
                indexacao.DataPrimeiraDigitacao = DateTime.Now;

                this.gravaLogDocumentoServico.Executar(
                    LogDocumento.AcaoDocumentoOcr,
                    indexacao.Documento.Id,
                    string.Format("Valor do campo {0} complementado pelo ocr", indexacao.Campo.Description));
            }

            this.indexacaoRepositorio.Salvar(indexacao);
        }

        private string ObtemValorPorReferenciaArquivo(Indexacao indexacao, dynamic endereco)
        {
            switch (indexacao.Campo.ReferenciaArquivo)
            {
                case Campo.ReferenciaDeArquivoCepDaResidenciaDoParticipante:
                    return endereco.Cep.ToString(CultureInfo.InvariantCulture);
                    
                case Campo.ReferenciaDeArquivoEstadoDaResidenciaDoParticipante:
                    return endereco.Estado;

                case Campo.ReferenciaDeArquivoCidadeDaResidenciaDoParticipante:
                    return endereco.Cidade;
                    
                case Campo.ReferenciaDeArquivoBairroDaResidenciaDoParticipante:
                    return endereco.Bairro;

                case Campo.ReferenciaDeArquivoLogradouroDaResidenciaDoParticipante:
                    return endereco.Logradouro;

                default:
                    return string.Empty;
            }
        }

        private dynamic ObtemEnderecoApartirDoCep(
            IList<ValorReconhecido> valoresReconhecidos, 
            string cepDoCadastro)
        {
            List<ValorReconhecido> ceps;

            try
            {
                ceps = valoresReconhecidos
                        .Where(x => x.CampoTemplate.ToUpper().Trim().Contains(Campo.ReferenciaDeArquivoCep))
                        .ToList();
            }
            catch (Exception exception)
            {
                Log.Application.Error(exception);
                return null;
            }

            if (ceps.Count == 0)
            {
                return null;
            }

            try
            {
                if (ceps.Any(x => x.Value != null && x.Value.RemoverDiacritico() == cepDoCadastro))
                {
                    dynamic endereco = this.cepRepositorio.ObterEndereco(cepDoCadastro.RemoverDiacritico().ToInt());
                    return endereco;
                }
            }
            catch (Exception exception)
            {
                Log.Application.Error(exception);
                return null;
            }

            return null;
        }
    }
}