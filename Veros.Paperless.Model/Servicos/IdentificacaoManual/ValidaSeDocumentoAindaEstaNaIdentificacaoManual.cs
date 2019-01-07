namespace Veros.Paperless.Model.Servicos.IdentificacaoManual
{
    using Entidades;
    using Framework;
    using Repositorios;

    public class ValidaSeDocumentoAindaEstaNaIdentificacaoManual : IValidaSeDocumentoAindaEstaNaIdentificacaoManual
    {
        private readonly IDocumentoRepositorio documentoRepositorio;

        public ValidaSeDocumentoAindaEstaNaIdentificacaoManual(IDocumentoRepositorio documentoRepositorio)
        {
            this.documentoRepositorio = documentoRepositorio;
        }

        public bool Validar(int documentoId)
        {
            if (documentoId == 0)
            {
                return false;
            }

            var documento = this.documentoRepositorio.ObterPorId(documentoId);

            var estaNaIdenticacaoManual = documento.TipoDocumento.Id == TipoDocumento.CodigoNaoIdentificado &&
                documento.Status != DocumentoStatus.Excluido;            

            Log.Application.DebugFormat("Documento Esta em Identificacao Manual {0}", estaNaIdenticacaoManual);

            return estaNaIdenticacaoManual;
        }
    }
}