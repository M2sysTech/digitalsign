namespace Veros.Paperless.Model.Servicos.Indexacoes
{
    using System.Collections.Generic;
    using Entidades;
    using Framework;
    using Repositorios;

    public class FixarCampoComplementoResidencia : FixarCampo 
    {
        public FixarCampoComplementoResidencia(IIndexacaoRepositorio indexacaoRepositorio) : base(indexacaoRepositorio)
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

            this.Indexacao = this.ObterIndexacao(documento, Campo.ReferenciaDeArquivoComplementoDaResidenciaDoParticipante);

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
                    "Documento #{0} teve seu campo [Complemento Residencial] indexado e batido pelo ocr", 
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
                "CASA",
                "CS",
                "CSA",
                "FUNDOS",
                "FUN",
                "FU",
                "SOBRADO",
                "SN",
                "S/N",
                "NAO POSSUI",
                "NADA CONSTA",
                "DESCONHECIDO",
                "NAO INFORMADO",
                "NAO CONSTA",
                "NC",
                "N/C",
                "NAO DECLARADO",
                "INEXISTENTE"
            };

            return valoresCampoComplemento.Contains(valor.ToUpper().Trim());
        }
    }
}