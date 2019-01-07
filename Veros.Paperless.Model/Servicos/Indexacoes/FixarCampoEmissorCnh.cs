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
                    "Documento #{0} não é uma cnh e não terá a indexação de seus campos complementados automaticamente",
                    documento.Id);

                return false;
            }

            this.Indexacao = this.ObterIndexacao(documento, Campo.ReferenciaDeArquivoOrgaoEmissorDoDocumento);

            if (this.Indexacao == null)
            {
                Log.Application.InfoFormat(
                    "Documento #{0} não possui campo Orgão Emissor para complementação automatica",
                    documento.Id);

                return false;
            }

            return true;
        }
    }
}