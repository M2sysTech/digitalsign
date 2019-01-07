namespace Veros.Paperless.Model.Servicos.Batimento.Experimental.BatimentoPorDocumento
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Comparacao;
    using Entidades;
    using Framework;
    using Repositorios;

    public class ComprovanteResidenciaConsultaCepBatimento : ModuloBatimentoIndexacaoMapeada
    {
        private readonly ICepRepositorio cepRepositorio;
        private readonly IValorReconhecidoRepositorio valorReconhecidoRepositorio;
        private readonly ITagRepositorio tagRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDocumentoServico;

        public ComprovanteResidenciaConsultaCepBatimento(
            ICriadorDeComparador criadorDeComparador, 
            ICepRepositorio cepRepositorio, 
            IValorReconhecidoRepositorio valorReconhecidoRepositorio, 
            ITagRepositorio tagRepositorio, 
            IGravaLogDoDocumentoServico gravaLogDocumentoServico) :
            base(criadorDeComparador)
        {
            this.cepRepositorio = cepRepositorio;
            this.valorReconhecidoRepositorio = valorReconhecidoRepositorio;
            this.tagRepositorio = tagRepositorio;
            this.gravaLogDocumentoServico = gravaLogDocumentoServico;
        }

        public override bool EstaBatido(IIndexacaoMapeada indexacaoMapeada)
        {
            if (this.PodeProcessarCampo(indexacaoMapeada.Campo) == false)
            {
                return false;
            }

            var indexacaoCompleta = indexacaoMapeada
                .Indexacao
                .Documento
                .Indexacao;

            var valoresReconhecidos = this.valorReconhecidoRepositorio
                .ObtemPorDocumento(indexacaoMapeada.Indexacao.Documento.Id);

            var cepDoCadastro = this.ObterValorCampoCepDoCadastroDoBanco(indexacaoCompleta);

            var endereco = this.ObtemEnderecoDoCepCoincidenteDoReconhecimento(valoresReconhecidos, cepDoCadastro);

            if (endereco.Cep.Equals(0) == false)
            {
                if (this.tagRepositorio.ObterValorPorNome("BATE_END_ANTES_DE_ATRIBUIR", "true").ToBoolean() == false)
                {
                    var observacao = string.Format(
                        "Campo [{0}] do documento {1} batido pelo serviço de cep sem batimento", 
                        indexacaoMapeada.Indexacao.Campo.Description,
                        indexacaoMapeada.Indexacao.Documento.Id);

                    this.gravaLogDocumentoServico.Executar(
                        LogDocumento.AcaoDocumentoOcr,
                        indexacaoMapeada.Indexacao.Documento.Id,
                        observacao);

                    if (indexacaoMapeada.Indexacao.Campo.Id.Equals(858))
                    {
                        if (string.IsNullOrEmpty(indexacaoMapeada.Indexacao.ObterValorParaBatimento()) || 
                            string.IsNullOrWhiteSpace(indexacaoMapeada.Indexacao.ObterValorParaBatimento()))
                        {
                            Log.Application.InfoFormat(
                            "Para indexacao [{0}] do campo complemento com valor null ou espaçado foi [{1}] Comparador:{2}",
                            indexacaoMapeada.Indexacao.Id,
                            true,
                            "BATE_END_ANTES_DE_ATRIBUIR");
                        }
                    }

                    return true;
                }
            }

            var comparador = this.criadorDeComparador.Cria(indexacaoMapeada.Indexacao.Campo.TipoCampo);
            var valorEndereco = this.CriarRetornoEndereco(endereco);

            var batido = comparador.SaoIguais(
                indexacaoMapeada.ObterValorParaBatimento(),
                valorEndereco[indexacaoMapeada.Indexacao.Campo.ReferenciaArquivo]);

            if (batido)
            {
                var observacao = string.Format(
                            "Campo [{0}] do documento {1} batido pelo serviço de cep com batimento",
                            indexacaoMapeada.Indexacao.Campo.Description,
                            indexacaoMapeada.Indexacao.Documento.Id);

                this.gravaLogDocumentoServico.Executar(
                    LogDocumento.AcaoDocumentoOcr,
                    indexacaoMapeada.Indexacao.Documento.Id,
                    observacao);
            }

            if (indexacaoMapeada.Indexacao.Campo.Id.Equals(858))
            {
                if (string.IsNullOrEmpty(indexacaoMapeada.Indexacao.ObterValorParaBatimento()) ||
                    string.IsNullOrWhiteSpace(indexacaoMapeada.Indexacao.ObterValorParaBatimento()))
                {
                    Log.Application.InfoFormat(
                    "Para indexacao [{0}] do campo complemento com valor null ou espaçado foi [{1}] Comparador:{2}",
                    indexacaoMapeada.Indexacao.Id,
                    batido,
                    comparador);
                }
            }

            return batido;
        }

        private bool PodeProcessarCampo(Campo campo)
        {
            return campo.ReferenciaArquivo == Campo.ReferenciaDeArquivoCepDaResidenciaDoParticipante ||
                campo.ReferenciaArquivo == Campo.ReferenciaDeArquivoEstadoDaResidenciaDoParticipante ||
                campo.ReferenciaArquivo == Campo.ReferenciaDeArquivoCidadeDaResidenciaDoParticipante ||
                campo.ReferenciaArquivo == Campo.ReferenciaDeArquivoBairroDaResidenciaDoParticipante ||
                campo.ReferenciaArquivo == Campo.ReferenciaDeArquivoLogradouroDaResidenciaDoParticipante;
        }

        private IDictionary<string, string> CriarRetornoEndereco(dynamic enderecoBaseCep)
        {
            var endereco = new Dictionary<string, string>();

            endereco.Add(
                Campo.ReferenciaDeArquivoCepDaResidenciaDoParticipante, 
                enderecoBaseCep.Cep.ToString(CultureInfo.InvariantCulture));

            endereco.Add(
                Campo.ReferenciaDeArquivoEstadoDaResidenciaDoParticipante, 
                enderecoBaseCep.Estado);
            
            endereco.Add(
                Campo.ReferenciaDeArquivoCidadeDaResidenciaDoParticipante, 
                enderecoBaseCep.Cidade);

            endereco.Add(
                Campo.ReferenciaDeArquivoBairroDaResidenciaDoParticipante, 
                enderecoBaseCep.Bairro);

            endereco.Add(
                Campo.ReferenciaDeArquivoLogradouroDaResidenciaDoParticipante, 
                enderecoBaseCep.Logradouro);

            return endereco;
        }

        private string ObterValorCampoCepDoCadastroDoBanco(IEnumerable<Indexacao> indexacao)
        {
            switch (BaterDocumento.BatimentoCom)
            {
                case BatimentoDo.ValorFinal:
                    var cep = indexacao
                        .FirstOrDefault(
                            x => x.Campo.ReferenciaArquivo == Campo.ReferenciaDeArquivoCepDaResidenciaDoParticipante);

                    return cep == null ? 
                        string.Empty : 
                        cep.ValorFinal;

                default:
                    var cepNumero = indexacao.
                        FirstOrDefault(
                            x => x.Campo.ReferenciaArquivo == Campo.ReferenciaDeArquivoCepDaResidenciaDoParticipante);

                    return cepNumero == null ? 
                        string.Empty : 
                        cepNumero.SegundoValor;
            }
        }

        private dynamic ObtemEnderecoDoCepCoincidenteDoReconhecimento(
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
                if (ceps.Any(x => x.Value != null && x.Value.RemoverDiacritico().RemoveCaracteresEspeciais().RemoverEspacosEntreAsPalavras() == cepDoCadastro))
                {
                    var cep = cepDoCadastro
                        .RemoverDiacritico()
                        .RemoveCaracteresEspeciais()
                        .RemoverEspacosEntreAsPalavras()
                        .ToInt();

                    dynamic endereco = this.cepRepositorio.ObterEndereco(cep);

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
