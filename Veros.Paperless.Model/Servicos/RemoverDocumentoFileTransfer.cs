namespace Veros.Paperless.Model.Servicos
{
    using Entidades;
    using Framework;
    using Repositorios;

    public class RemoverDocumentoFileTransfer : IRemoverDocumentoFileTransfer
    {
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IRemoverPaginaFileTransfer removerPaginaFileTransfer;

        public RemoverDocumentoFileTransfer(
            IPaginaRepositorio paginaRepositorio, 
            IRemoverPaginaFileTransfer removerPaginaFileTransfer)
        {
            this.paginaRepositorio = paginaRepositorio;
            this.removerPaginaFileTransfer = removerPaginaFileTransfer;
        }

        public void Executar(Documento documento)
        {
            Log.Application.InfoFormat("Procurando paginas png para o documento #{0}", documento.Id);

            var paginasPng = this.paginaRepositorio.ObterTipoPng(documento);

            foreach (var paginaPng in paginasPng)
            {
                Log.Application.InfoFormat("Removendo pagina PNG #{0}", paginaPng.Id);
                ////this.removerPaginaFileTransfer.Executar(paginaPng);
            }
        }
    }
}