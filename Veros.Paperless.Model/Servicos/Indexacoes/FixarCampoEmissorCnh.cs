namespace Veros.Paperless.Model.Servicos.Indexacoes
{
    using Entidades;
    using Framework;
    using Repositorios;

    public class FixarCampoEmissorCnh : FixarCampo
    {
        public FixarCampoEmissorCnh(IIndexacaoRepositorio indexacaoRepositorio) : base(indexacaoRepositorio)
        {
        }

        public override bool PodeComplementarIndexacao(Documento documento)
        {
            if (documento.TipoDocumento.Id != TipoDocumento.CodigoCnh)
            {
                Log.Application.InfoFormat(
                    "Documento #{0} n�o � uma cnh e n�o ter� a indexa��o de seus campos complementados automaticamente",
                    documento.Id);

                return false;
            }

            this.Indexacao = this.ObterIndexacao(documento, Campo.ReferenciaDeArquivoOrgaoEmissorDoDocumento);

            if (this.Indexacao == null)
            {
                Log.Application.InfoFormat(
                    "Documento #{0} n�o possui campo Org�o Emissor para complementa��o automatica",
                    documento.Id);

                return false;
            }

            return true;
        }
    }
}