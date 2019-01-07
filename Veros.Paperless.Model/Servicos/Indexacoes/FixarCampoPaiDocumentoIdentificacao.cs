namespace Veros.Paperless.Model.Servicos.Indexacoes
{
    using System.Collections.Generic;
    using Entidades;
    using Framework;
    using Repositorios;

    public class FixarCampoPaiDocumentoIdentificacao : FixarCampo 
    {
        public FixarCampoPaiDocumentoIdentificacao(IIndexacaoRepositorio indexacaoRepositorio) : base(indexacaoRepositorio)
        {
        }

        public override bool PodeComplementarIndexacao(Documento documento)
        {
            if (this.EhDocumentoDeIdentificacao(documento) == false)
            {
                Log.Application.InfoFormat(
                    "Documento #{0} n�o � um documento de identifica��o e n�o ter� a indexa��o de seus campos complementados automaticamente",
                    documento.Id);
                return false;
            }

            this.Indexacao = this.ObterIndexacao(documento, Campo.ReferenciaDeArquivoNomePaiCliente);

            if (this.Indexacao == null)
            {
                Log.Application.InfoFormat(
                    "Documento #{0} n�o possui campo [Pai] para complementa��o automatica",
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
                "N�O DECLARADO",
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

        private bool EhDocumentoDeIdentificacao(Documento documento)
        {
            return documento.TipoDocumento.Id != TipoDocumento.CodigoCnh ||
                   documento.TipoDocumento.Id != TipoDocumento.CodigoRg ||
                   documento.TipoDocumento.Id != TipoDocumento.CodigoCie ||
                   documento.TipoDocumento.Id != TipoDocumento.CodigoPassaporte;
        }
    }
}