namespace Veros.Paperless.Model.Servicos.Batimento
{
    using System;
    using Entidades;
    using Framework;
    using Repositorios;
    using System.Text.RegularExpressions;
    using Veros.Framework.Validation;
    using System.Linq;
    using System.Collections.Generic;

    public class BatimentoEComplementacaoDocumentoServico : 
        IBatimentoEComplementacaoDocumentoServico
    {
        private readonly ICampoRepositorio campoRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDocumentoServico;
        private readonly IIndexacaoRepositorio indexacaoRepositorio;

        public BatimentoEComplementacaoDocumentoServico(
            ICampoRepositorio campoRepositorio, 
            IGravaLogDoDocumentoServico gravaLogDocumentoServico, 
            IIndexacaoRepositorio indexacaoRepositorio)
        {
            this.campoRepositorio = campoRepositorio;
            this.gravaLogDocumentoServico = gravaLogDocumentoServico;
            this.indexacaoRepositorio = indexacaoRepositorio;
        }

        private enum TipoCampoAnalisado
        {
            DataNascimento,
            Nome,
            Cpf,
            CodigoNumerico,
            Indefinido
        }

        //// customização para "batimento" direto do OCR, sem informação para comparar, confiando no que o OCR leu. 
        public void Execute(Documento documento, ImagemReconhecida imagemReconhecida, List<int> camposBatidos)
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
            foreach (var campoAtual in camposDoDocumento)
            {
                //// verifica se ja foi preenchido via batimento normal (evitar inserir linha duplicada) 
                if (camposBatidos != null && camposBatidos.Any(x => x == campoAtual.Id))
                {
                    continue;
                }

                if (!campoAtual.PodeInserirPeloOcr)
                {
                    continue;
                }

                //// Há casos em que o Fluxo retorna para batimento depois da Identificação, e o documento já tem campos preenchidos
                //// se for um desses casos, não deve acrescentar batimento duplicado
                var indexacaoCampoAtual = this.indexacaoRepositorio.ObterPorTipoCampoDeUmDocumento(documento.Id, campoAtual.TipoCampo).FirstOrDefault();
                if (this.VerificaSeJaBateuCampoAtual(indexacaoCampoAtual))
                {
                    continue;
                }

                Indexacao indexacao = null;
                var encontrouIndexacao = false;
                foreach (var valorReconhecido in valoresReconhecidos)
                {
                    if (string.IsNullOrEmpty(valorReconhecido.Value))
                    {
                        continue;
                    }

                    if (campoAtual.EstaMapeadoPara(valorReconhecido.CampoTemplate, valorReconhecido.TemplateName) == false)
                    {
                        continue;
                    }
                    
                    if (this.ConteudoEncontradoValido(ref indexacao, campoAtual, valorReconhecido))
                    {
                        encontrouIndexacao = true;
                        Log.Application.InfoFormat("Bateu TdCampo_code[{0}] do mdoc[{1}]. Conteudo:[{2}]", campoAtual.Id, documento.Id, indexacao.PrimeiroValor);
                        //// Se for uma data de nascimento, continuar buscando por data mais antiga
                        if (!this.DeveContinuarProcurando(campoAtual))
                        {
                            break;
                        }
                    }
                }

                if (encontrouIndexacao)
                {
                    if (indexacaoCampoAtual != null)
                    {
                        indexacaoCampoAtual.PrimeiroValor = indexacao.PrimeiroValor;
                        indexacao = indexacaoCampoAtual;
                    }
                    else
                    {
                        indexacao.Documento = documento;
                        documento.Indexacao.Add(indexacao);
                    }

                    indexacao.DataPrimeiraDigitacao = DateTime.Now;
                    this.gravaLogDocumentoServico.Executar(
                        LogDocumento.AcaoDocumentoOcr,
                        documento.Id,
                        string.Format("Campo [{0}] complementado pelo ocr. Valor:[{1}]", indexacao.Campo.Description, BatimentoFullText.LimitaString(indexacao.PrimeiroValor.Trim(), 99))); 
                    this.indexacaoRepositorio.Salvar(indexacao);
                }
            }
        }

        public string TratarCampoDataNascimento(ValorReconhecido valorReconhecido, string valorJaCadastrado)
        {
            if (valorReconhecido == null || string.IsNullOrEmpty(valorReconhecido.Value))
            {
                return string.Empty;
            }

            var match = Regex.Match(valorReconhecido.Value, @"[0-9]{1,2}[ \/-][0-9]{1,2}[ \/-][0-9]{2,4}");
            if (!match.Success)
            {
                match = Regex.Match(valorReconhecido.Value.ToUpper(), @"[0-9]{1,2}[ \/-]{0,1}(JAN|FEV|MAR|ABR|MAI|JUN|JUL|AGO|SET|OUT|NOV|DEZ)[ \/-]{0,1}[0-9]{2,4}");
            }

            if (match.Success)
            {
                if (match.Value.Length <= 4 || match.Value.Length > 11)
                {
                    return string.Empty;
                }

                var resultado = string.Empty;
                DateTime dataConvertida;
                if (!DateTime.TryParse(match.Value, out dataConvertida))
                {
                    return string.Empty;
                }

                resultado = dataConvertida.ToString("dd") + dataConvertida.ToString("MM") + dataConvertida.ToString("yyyy");
                if (string.IsNullOrEmpty(valorJaCadastrado))
                {
                    return resultado;
                }

                if (dataConvertida < DateTime.ParseExact(valorJaCadastrado, "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture))
                {
                    return resultado;
                }
            }

            return string.Empty;
        }

        private bool VerificaSeJaBateuCampoAtual(Indexacao index)
        {
            if (index == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(index.PrimeiroValor))
            {
                return false;
            }

            return true;
        }

        private bool ConteudoEncontradoValido(ref Indexacao indexacao, Campo campoAtual, ValorReconhecido valorReconhecido)
        {
            if (indexacao == null)
            {
                indexacao = new Indexacao()
                {
                    PrimeiroValor = string.Empty,
                    Campo = campoAtual,
                    OcrComplementou = true,
                    UsuarioPrimeiroValor = -2
                };
            }

            var defineTipo = this.AvaliarConteudoCampo(campoAtual);
            var valorTratado = string.Empty;
            switch (defineTipo)
            {
                case TipoCampoAnalisado.DataNascimento:
                    valorTratado = this.TratarCampoDataNascimento(valorReconhecido, indexacao.PrimeiroValor);
                    break;
                case TipoCampoAnalisado.Nome:
                    valorTratado = this.TratarCampoNome(valorReconhecido);
                    break;
                case TipoCampoAnalisado.CodigoNumerico:
                    valorTratado = this.TratarCampoCodigoNumerico(valorReconhecido);
                    break;
                case TipoCampoAnalisado.Cpf:
                    valorTratado = this.TratarCampoCpf(valorReconhecido);
                    break;
                case TipoCampoAnalisado.Indefinido:
                    valorTratado = string.Empty;
                    return false;
                default:
                    valorTratado = string.Empty;
                    return false;
            }

            if (!string.IsNullOrEmpty(valorTratado))
            {
                indexacao.PrimeiroValor = valorTratado;
                return true;
            }

            return false;
        }

        private string TratarCampoNome(ValorReconhecido valorReconhecido)
        {
            if (valorReconhecido == null || string.IsNullOrEmpty(valorReconhecido.Value))
            {
                return string.Empty;
            }
            
            if (this.PareceComNomeValido(valorReconhecido.Value))
            {
                return valorReconhecido.Value.ToUpper().RemoverDiacritico().Trim();
            }

            return string.Empty;
        }

        private bool PareceComNomeValido(string value)
        {
            var aux = value.ToUpper().RemoverDiacritico().Trim();
            if (aux.Length < 12)
            {
                return false;
            }

            if (aux.IndexOf(" ") < 0)
            {
                return false;
            }

            var listaValidos = "ABCDEFGHIJKLMNOPQRSTUVWXYZ ";
            for (int i = 0; i < aux.Length; i++)
            {
                if (listaValidos.IndexOf(aux.Substring(i, 1)) < 0)
                {
                    return false;
                }
            }

            return true;
        }

        private string TratarCampoCodigoNumerico(ValorReconhecido valorReconhecido)
        {
            if (valorReconhecido == null || string.IsNullOrEmpty(valorReconhecido.Value))
            {
                return string.Empty;
            }

            var numeros = valorReconhecido.Value.ObterInteiros();
            if (numeros.Length >= 5 && numeros.Length <= 12)
            {
                return numeros;
            }

            return string.Empty;
        }

        private string TratarCampoCpf(ValorReconhecido valorReconhecido)
        {
            if (valorReconhecido == null || string.IsNullOrEmpty(valorReconhecido.Value))
            {
                return string.Empty;
            }

            var match = Regex.Match(valorReconhecido.Value, @"[0-9]{3}[ ./\-]{0,1}[0-9]{3}[ ./\-]{0,1}[0-9]{3}[ ./\-]{0,1}[0-9]{2}");
            if (match.Success)
            {
                var validaCpf = new CpfValidation();
                if (validaCpf.IsValid(match.Value.ObterInteiros()))
                {
                    return match.Value.ObterInteiros();
                }
            }

            return string.Empty;
        }

        private TipoCampoAnalisado AvaliarConteudoCampo(Campo campoAtual)
        {
            if (campoAtual.ReferenciaArquivo == Campo.ReferenciaDeArquivoDataDeNascimentoDoParticipante)
            {
                return TipoCampoAnalisado.DataNascimento;
            }

            if (campoAtual.ReferenciaArquivo == Campo.ReferenciaDeArquivoNomeTitular || 
                campoAtual.ReferenciaArquivo == Campo.ReferenciaDeArquivoNomePaiCliente ||
                campoAtual.ReferenciaArquivo == Campo.ReferenciaDeArquivoNomeMaeCliente)
            {
                return TipoCampoAnalisado.Nome;
            }

            if (campoAtual.ReferenciaArquivo == Campo.ReferenciaDeArquivoNumeroDocumentoIdentificacao)
            {
                return TipoCampoAnalisado.CodigoNumerico;
            }

            if (campoAtual.ReferenciaArquivo == Campo.ReferenciaDeArquivoCpf)
            {
                return TipoCampoAnalisado.Cpf;
            }

            return TipoCampoAnalisado.Indefinido;
        }

        private bool DeveContinuarProcurando(Campo campoAtual)
        {
            if (campoAtual == null)
            {
                return false;
            }

            //// data nascimento
            if (campoAtual.ReferenciaArquivo == Campo.ReferenciaDeArquivoDataDeNascimentoDoParticipante)
            {
                return true;
            }

            return false;
        }
    }
}
