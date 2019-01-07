namespace Veros.Paperless.Model.Servicos.PreparaAjuste
{
    using Entidades;
    using Framework;
    using Repositorios;
    using System.Linq;

    public class ExisteDocumentoPngAtivoServico : IExisteDocumentoPngAtivoServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;

        public ExisteDocumentoPngAtivoServico(IDocumentoRepositorio documentoRepositorio)
        {
            this.documentoRepositorio = documentoRepositorio;
        }

        public bool Existe(Documento documento)
        {
            var documentosPng = this.documentoRepositorio
                .ObterPorProcessoETipo(documento.Processo.Id, TipoDocumento.CodigoEmAjuste);

            var pngAtivo = documentosPng.FirstOrDefault(
                x => x.DocumentoPaiId == documento.Id &&
                    x.Status != DocumentoStatus.Excluido);

            if (pngAtivo != null)
            {
                Log.Application.InfoFormat("Documento #{0} já possui PNG ativo [{1}].", documento.Id, pngAtivo.Id);
                return true;
            }

            return false;
        }
    }
}
