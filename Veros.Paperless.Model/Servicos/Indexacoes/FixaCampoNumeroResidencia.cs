namespace Veros.Paperless.Model.Servicos.Indexacoes
{
    using System.Collections.Generic;
    using Entidades;
    using Framework;
    using Repositorios;

    public class FixaCampoNumeroResidencia : FixarCampo 
    {
        public FixaCampoNumeroResidencia(IIndexacaoRepositorio indexacaoRepositorio) : base(indexacaoRepositorio)
        {
        }

        public override bool PodeComplementarIndexacao(Documento documento)
        {
            if (documento.TipoDocumento.Id != TipoDocumento.CodigoComprovanteDeResidencia)
            {
                Log.Application.InfoFormat(
                    "Documento #{0} não é um comprovante de residencia e não terá a indexação de seus campos complementados automaticamente", 
                    documento.Id);
                return false;
            }

            this.Indexacao = this.ObterIndexacao(documento, Campo.ReferenciaDeArquivoNumeroDaResidenciaDoParticipante);

            if (this.Indexacao == null)
            {
                Log.Application.InfoFormat(
                    "Documento #{0} não possui campo complemento residencial para complementação automatica", 
                    documento.Id);
                return false;
            }

            if (string.IsNullOrEmpty(this.Indexacao.ValorFinal) == false)
            {
                Log.Application.InfoFormat(
                    "Documento #{0} teve seu campo [Número Residencial] indexado e batido pelo ocr", 
                    documento.Id);
                return false;
            }

            if (this.PodeFixarComplementacaoDesteValor(this.Indexacao.SegundoValor) == false)
            {
                return false;
            }

            return true;
        }

        private bool PodeFixarComplementacaoDesteValor(string valor)
        {
            if (string.IsNullOrEmpty(valor))
            {
                return true;
            }

            if (string.IsNullOrWhiteSpace(valor))
            {
                return true;
            }

            IList<string> valoresCampoComplemento = new List<string>
            {
                "SN",
                "S/N",
                "NADA CONSTA",
                "DESCONHECIDO",
                "NAO INFORMADO",
                "NC",
                "N/C",
                "INEXISTENTE"
            };

            return valoresCampoComplemento.Contains(valor.ToUpper().Trim());
        }
    }
}