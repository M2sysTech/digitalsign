namespace Veros.Paperless.Model.Servicos.Validacao
{
    using System.Linq;
    using Entidades;
    using Repositorios;

    public class TitularDocumentoMestreComCertidaoCasamento : ITitularDocumentoMestreComCertidaoCasamento
    {
        private readonly IDocumentoRepositorio documentoRepositorio;

        public TitularDocumentoMestreComCertidaoCasamento(IDocumentoRepositorio documentoRepositorio)
        {
            this.documentoRepositorio = documentoRepositorio;
        }

        public Indexacao Complementar(Documento documentoMestre, Indexacao indexacaoDocumentoMestre)
        {
            var documentos = this.documentoRepositorio.ObterDocumentosDoProcessoComCpf(
               documentoMestre.Processo.Id,
               documentoMestre.Cpf);

            var certidaoCasamento = documentos.SingleOrDefault(x => x.TipoDocumento.Id == TipoDocumento.CodigoFichaDeCadastro);

            if (certidaoCasamento == null)
            {
                return indexacaoDocumentoMestre;
            }

            var sexo = documentoMestre
                .Indexacao
                .SingleOrDefault(x => x.Campo.ReferenciaArquivo == Campo.ReferenciaDeArquivoSexoDoParticipante);

            Indexacao indexacaoCertidaoCasamento;

            if (sexo == null)
            {
                return indexacaoDocumentoMestre;
            }

            if (sexo.SegundoValor == "M")
            {
                indexacaoCertidaoCasamento = certidaoCasamento
                    .Indexacao
                    .SingleOrDefault(x => x.Campo.Id == Campo.CampoCertidaoCasamentoMarido);
            }
            else
            {
                indexacaoCertidaoCasamento = certidaoCasamento
                    .Indexacao
                    .SingleOrDefault(x => x.Campo.Id == Campo.CampoCertidaoCasamentoEsposa);
            }

            indexacaoDocumentoMestre.ValorFinal = indexacaoCertidaoCasamento.ValorFinal;

            return indexacaoDocumentoMestre;
        }
    }
}