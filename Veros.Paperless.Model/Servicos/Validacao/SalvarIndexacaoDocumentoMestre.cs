namespace Veros.Paperless.Model.Servicos.Validacao
{
    using System.Linq;
    using Campos;
    using Entidades;
    using Repositorios;

    public class SalvarIndexacaoDocumentoMestre
    {
        private readonly ICamposValidacaoRepositorio camposValidacaoRepositorio;
        private readonly IIndexacaoRepositorio indexacaoRepositorio;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly ITitularDocumentoMestreComCertidaoCasamento titularDocumentoMestreComCertidaoCasamento;

        public SalvarIndexacaoDocumentoMestre(
            ICamposValidacaoRepositorio camposValidacaoRepositorio, 
            IIndexacaoRepositorio indexacaoRepositorio, 
            IDocumentoRepositorio documentoRepositorio, 
            ITitularDocumentoMestreComCertidaoCasamento titularDocumentoMestreComCertidaoCasamento)
        {
            this.camposValidacaoRepositorio = camposValidacaoRepositorio;
            this.indexacaoRepositorio = indexacaoRepositorio;
            this.documentoRepositorio = documentoRepositorio;
            this.titularDocumentoMestreComCertidaoCasamento = titularDocumentoMestreComCertidaoCasamento;
        }

        public void Executar(Documento documentoMestre, Indexacao indexacaoDocumentoComprovacao)
        {
            var mapeamentosCamposDocumentoComprovacao = this.camposValidacaoRepositorio
                .ObterPorCampoDocumentoComprovacao(indexacaoDocumentoComprovacao.Campo);

            if (mapeamentosCamposDocumentoComprovacao == null)
            {
                return;
            }

            foreach (var mapeamentoCamposDocumentoComprovacao in mapeamentosCamposDocumentoComprovacao)
            {
                var indexacaoDocumentoMestre = documentoMestre
                    .Indexacao
                    .FirstOrDefault(x => x.Campo == mapeamentoCamposDocumentoComprovacao.CampoDocumentoMestre);

                if (indexacaoDocumentoMestre == null)
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(indexacaoDocumentoMestre.ValorFinal) == false &
                    string.IsNullOrEmpty(indexacaoDocumentoMestre.ValorFinal) == false)
                {
                    continue;
                }

                if (this.PodeComplementarTitularComCertidaoCasamento(documentoMestre, indexacaoDocumentoMestre))
                {
                    indexacaoDocumentoMestre = this.titularDocumentoMestreComCertidaoCasamento.Complementar(documentoMestre, indexacaoDocumentoMestre);
                }
                else
                {
                    indexacaoDocumentoMestre.PrimeiroValor = indexacaoDocumentoComprovacao.ValorFinal;

                    if (this.PodeAbreviarValorIndexacao(indexacaoDocumentoComprovacao))
                    {
                        indexacaoDocumentoMestre.ValorFinal = Nome.AbreviarExcetoPrimeiroEUltimo(indexacaoDocumentoComprovacao.ValorFinal);
                    }
                    else
                    {
                        indexacaoDocumentoMestre.ValorFinal = indexacaoDocumentoComprovacao.ValorFinal;
                    }
                }
                
                indexacaoDocumentoMestre.ValorUtilizadoParaValorFinal = indexacaoDocumentoComprovacao.ValorUtilizadoParaValorFinal;
                indexacaoDocumentoMestre.ValidacaoComplementou = true;
                
                this.indexacaoRepositorio.Salvar(indexacaoDocumentoMestre);
            }
        }

        private bool PodeComplementarTitularComCertidaoCasamento(Documento documentoMestre, Indexacao indexacaoDocumentoMestre)
        {
            if (indexacaoDocumentoMestre.Campo.ReferenciaArquivo != Campo.ReferenciaDeArquivoNomeTitular)
            {
                return false;
            }

            var documentos = this.documentoRepositorio.ObterDocumentosDoProcessoComCpf(
                documentoMestre.Processo.Id, 
                documentoMestre.Cpf);

            ////if (documentos.Any(x => x.TipoDocumento.Id == TipoDocumento.CodigoCertidaoCasamento) == false)
            ////{
            ////    return false;
            ////}

            if (this.TitularEhCasado(documentoMestre) == false)
            {
                return false;
            }

            return true;
        }

        private bool TitularEhCasado(Documento documentoMestre)
        {
            var estadoCivil = documentoMestre
                .Indexacao
                .SingleOrDefault(x => x.Campo.ReferenciaArquivo == Campo.ReferenciaDeArquivoEstadoCivilDoParticipante);

            return estadoCivil != null && ((estadoCivil.SegundoValor == "1") || (estadoCivil.SegundoValor == "7"));
        }

        private bool PodeAbreviarValorIndexacao(Indexacao indexacaoDocumentoComprovacao)
        {
            if (indexacaoDocumentoComprovacao.ValorFinal == null)
            {
                return false;
            }

            return (indexacaoDocumentoComprovacao.EhCampoNomeDaMae() || indexacaoDocumentoComprovacao.EhCampoNomeDoPai()) 
                && indexacaoDocumentoComprovacao.ValorFinal.Length > 30;
        }
    }
}