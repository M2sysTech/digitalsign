namespace Veros.Paperless.Model.Servicos.Classificacao
{
    using Entidades;
    using Framework;
    using Repositorios;

    public class ValidaSeDocumentoAindaEstaNaClassificacao : IValidaSeDocumentoAindaEstaNaClassificacao
    {
        private readonly IDocumentoRepositorio documentoRepositorio;

        public ValidaSeDocumentoAindaEstaNaClassificacao(IDocumentoRepositorio documentoRepositorio)
        {
            this.documentoRepositorio = documentoRepositorio;
        }

        public bool Validar(int documentoId)
        {
            var documento = this.documentoRepositorio.ObterPorId(documentoId);

            ////var estaNaClassificacao = documento.TipoDocumento.Id == TipoDocumento.CodigoNaoIdentificado &&
            ////    documento.Status != DocumentoStatus.Excluido;

            var estaNaClassificacao = documento.Status == DocumentoStatus.AguardandoIdentificacao;

            Log.Application.DebugFormat("Documento Esta em Classificacao {0}", estaNaClassificacao);

            return estaNaClassificacao;
        }
    }
}