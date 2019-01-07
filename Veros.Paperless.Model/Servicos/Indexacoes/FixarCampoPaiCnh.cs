namespace Veros.Paperless.Model.Servicos.Indexacoes
{
    using System.Collections.Generic;
    using Entidades;
    using Framework;
    using Repositorios;

    public class FixarCampoPaiCnh : FixarCampo 
    {
        public FixarCampoPaiCnh(IIndexacaoRepositorio indexacaoRepositorio) : base(indexacaoRepositorio)
        {
        }

        public override bool PodeComplementarIndexacao(Documento documento)
        {
            if (documento.TipoDocumento.Id != TipoDocumento.CodigoCnh)
            {
                Log.Application.InfoFormat(
                    "Documento #{0} não é uma cnh e não terá a indexação de seus campos complementados automaticamente",
                    documento.Id);
                return false;
            }

            this.Indexacao = this.ObterIndexacao(documento, Campo.ReferenciaDeArquivoNomePaiCliente);

            if (this.Indexacao == null)
            {
                Log.Application.InfoFormat(
                    "Documento #{0} não possui campo [Pai] para complementação automatica",
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
                "NI",
                "NAO DECLARADO",
                "NAO POSSUI",
                "N/C",
                "NAO",
                "N CONSTA",
                "NO INFORMADO",
                "NAO CONSTA",
                "NAO INFORMADO",
                "NATURAL",
                "N D",
                "NAO TEM",
                "NAO RECONHECIDO",
                "N DECLARADO",
                "XXXXXXXXXXXXXX",
                "NAO INFORMADA",
                "SEM FILIACAO",
                "DESC",
                "NAO INF",
                "NAO DECLARADO",
                "IGNORADO",
                "NAO COSNTA",
                "NADA CONSTA",
                "N¿O DECLARADO",
                "AUSENTE",
                "NAO INFORMOU",
                "NP",
                "ND",
                "DESCONHECIDO",
                "NAO CADASTRADO",
                "N INFORMADO",
                "INEXISTENTE",
                "NDA",
                "NO CONSTA",
                "NAO COSTA",
                "NAO INFORMA",
                "NO DECLARADO",
                "NC",
                "NULO",
                "NAO REGISTRADO",
                "N LOCALIZADO",
                "NADA DECLARADO"
            };

            return valoresCampoComplemento.Contains(valor.ToUpper().Trim());
        }
    }
}