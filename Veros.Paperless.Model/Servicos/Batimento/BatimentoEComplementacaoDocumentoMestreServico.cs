namespace Veros.Paperless.Model.Servicos.Batimento
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Framework;
    using Repositorios;

    public class BatimentoEComplementacaoDocumentoMestreServico : BatimentoEComplementacaoDocumentoBase 
    {
        public BatimentoEComplementacaoDocumentoMestreServico(
            ICampoRepositorio campoRepositorio, 
            IGravaLogDoDocumentoServico gravaLogDocumentoServico, 
            IIndexacaoRepositorio indexacaoRepositorio) : base(campoRepositorio, gravaLogDocumentoServico, indexacaoRepositorio)
        {
            this.PodeInserirCampoNaoReconhecido = true;
        }

        public override void Execute(Documento documento, ImagemReconhecida imagemReconhecida, List<int> camposBatidosId = null)
        {
            if (this.PodeComplementarIndexacao(documento, imagemReconhecida) == false)
            {
                return;
            }
            
            var camposDoDocumento = this.campoRepositorio
                .ObterPorTipoDocumentoComMapeamentoOcr(documento.TipoDocumento.Id)
                .OrderBy(campo => campo.PodeInserirPeloOcr);

            foreach (var campoDoDocumento in camposDoDocumento)
            {
                var indexacao = documento.Indexacao.FirstOrDefault(x => x.Campo == campoDoDocumento);
                var valoresReconhecidos = imagemReconhecida.ValoresReconhecidos;

                if (this.ExistemValoresReconhecidosDeTemplatesEspecificos(valoresReconhecidos) == false)
                {
                    if (this.CampoPodeSerInseridoMesmoQuandoNaoHaReconhecimento(indexacao, campoDoDocumento))
                    {
                        indexacao = this.AdicionaNovaIndexacaoParaCampoReconhecido(
                            documento,
                            null,
                            campoDoDocumento);

                        this.gravaLogDocumentoServico.Executar(
                            LogDocumento.AcaoDocumentoOcr,
                            documento.Id,
                            string.Format("Campo [{0}] inserido pelo ocr.", campoDoDocumento.Description));

                        this.indexacaoRepositorio.Salvar(indexacao);
                    }

                    continue;
                }

                foreach (var valorReconhecido in valoresReconhecidos)
                {
                    if (string.IsNullOrEmpty(valorReconhecido.Value))
                    {
                        continue;
                    }

                    if (campoDoDocumento.EstaMapeadoPara(valorReconhecido.CampoTemplate, valorReconhecido.TemplateName) == false)
                    {
                        continue;
                    }

                    indexacao = this.AlterarIndexacao(
                        documento,
                        valorReconhecido,
                        campoDoDocumento,
                        indexacao,
                        imagemReconhecida.Palavras);

                    if (indexacao != null)
                    {
                        this.indexacaoRepositorio.Salvar(indexacao);
                    }
                }
            }
        }

        protected override Indexacao ModificaIndexacaoDoCampoReconhecido(
            Indexacao indexacao, 
            ValorReconhecido valorReconhecido, 
            IList<PalavraReconhecida> palavras)
        {
            if (this.EhCampoAgenciaOuConta(indexacao) || this.EhCampoPacoteServicos(indexacao))
            {
                if (indexacao.BateCom(this.ObterPrimeiroValor(valorReconhecido, indexacao.Campo)))
                {
                    indexacao.PrimeiroValor = this.ObterPrimeiroValor(valorReconhecido, indexacao.Campo);
                    indexacao.OcrComplementou = true;
                    indexacao.DataPrimeiraDigitacao = DateTime.Now;
                    indexacao.ValorUtilizadoParaValorFinal = ValorUtilizadoParaValorFinal.NaoDefinido;
                }

                return indexacao;
            }
            
            indexacao.PrimeiroValor = this.ObterPrimeiroValor(valorReconhecido, indexacao.Campo);
            indexacao.OcrComplementou = true;
            indexacao.DataPrimeiraDigitacao = DateTime.Now;
            indexacao.ValorUtilizadoParaValorFinal = ValorUtilizadoParaValorFinal.NaoDefinido;

            return indexacao;
        }
        
        protected override Indexacao AdicionaNovaIndexacaoParaCampoReconhecido(
            Documento documento, 
            ValorReconhecido valorReconhecido, 
            Campo campoDoDocumento)
        {
            var indexacao = new Indexacao
            {
                Campo = campoDoDocumento,
                Documento = documento,
                DataPrimeiraDigitacao = DateTime.Now,
                OcrComplementou = true,
                PrimeiroValor = this.ObterPrimeiroValor(valorReconhecido, campoDoDocumento)
            };

            documento.Indexacao.Add(indexacao);

            return indexacao;
        }

        protected override bool PodeComplementarIndexacao(
            Documento documento, 
            ImagemReconhecida imagemReconhecida)
        {
            var camposDoDocumento = this.campoRepositorio
                .ObterPorTipoDocumentoComMapeamentoOcr(documento.TipoDocumento.Id);

            if (camposDoDocumento == null)
            {
                return false;
            }

            if (camposDoDocumento.Count == 0)
            {
                return false;
            }

            return true;
        }

        private bool ExistemValoresReconhecidosDeTemplatesEspecificos(IList<ValorReconhecido> valoresReconhecidos)
        {
            if (valoresReconhecidos == null)
            {
                return false;
            }

            if (valoresReconhecidos.Count == 0)
            {
                return false;
            }

            try
            {
                if (valoresReconhecidos.FirstOrDefault(x => x.TemplateName.ToLower() == "pacquintapagina") == null
                    && valoresReconhecidos.FirstOrDefault(x => x.TemplateName.ToLower() == "pac24529_0214_pag3") == null
                    && valoresReconhecidos.FirstOrDefault(x => x.TemplateName.ToLower() == "pac24529_0214_pag4") == null)
                {
                    return false;
                }

                if (valoresReconhecidos.FirstOrDefault(x => x.TemplateName.ToLower() == "pacsextapagina") == null
                     && valoresReconhecidos.FirstOrDefault(x => x.TemplateName.ToLower() == "pac24529_0214_pag4") == null)
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                return false;
            }

            return true;
        }
        
        private string ObterPrimeiroValor(
            ValorReconhecido valorReconhecido, 
            Campo campoDoDocumento)
        {
            if (Campo.CamposAssinaturaGerenteDaConta.Contains(campoDoDocumento.Id))
            {
                if (valorReconhecido == null)
                {
                    return "NP";
                }

                return valorReconhecido.Value == "Yes" ? "P" : "NP";
            }

            if (Campo.CamposIndicadorMensalidadeMenor.Contains(campoDoDocumento.Id))
            {
                if (valorReconhecido == null)
                {
                    return "Erro";
                }

                return valorReconhecido.Value;
            }

            if (valorReconhecido.NaoTemConteudo())
            {
                return string.Empty;
            }

            if (valorReconhecido.Value.NaoTemConteudo())
            {
                return string.Empty;
            }

            ////if (Campo.CamposVersaoDaPac.Contains(campoDoDocumento.Id))
            ////{
            ////    return VersaoPacHelper.Converter(valorReconhecido.Value);
            ////}

            ////if (Campo.CamposAgenciaDaPac.Contains(campoDoDocumento.Id))
            ////{
            ////    return AgenciaHelper.Converter(valorReconhecido.Value);
            ////}

            ////if (Campo.CamposContasDaPac.Contains(campoDoDocumento.Id))
            ////{
            ////    return ContaHelper.Converter(valorReconhecido.Value);
            ////}

            if (campoDoDocumento.TipoDado == TipoDado.Bool)
            {
                return valorReconhecido.Value == "Sim" ? "S" : "N";
            }

            return valorReconhecido.Value;
        }

        private bool EhCampoAgenciaOuConta(Indexacao indexacao)
        {
            throw new NotImplementedException();
        }

        private bool EhCampoPacoteServicos(Indexacao indexacao)
        {
            throw new NotImplementedException();
        }
    }
}
