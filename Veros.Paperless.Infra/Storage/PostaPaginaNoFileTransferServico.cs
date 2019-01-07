namespace Veros.Paperless.Infra.Storage
{
    using System.IO;
    using Model.Servicos;
    using Model.Servicos.Importacao;
    using Veros.Framework;
    using Veros.Framework.IO;
    using Veros.Paperless.Model.Entidades;

    public class PostaPaginaNoFileTransferServico : IPostaPaginaNoFileTransferServico
    {
        private readonly IPostaArquivoFileTransferServico postaArquivoFileTransferServico;
        private readonly IFileSystem fileSystem;

        public PostaPaginaNoFileTransferServico(
            IPostaArquivoFileTransferServico postaArquivoFileTransferServico, 
            IFileSystem fileSystem)
        {
            this.postaArquivoFileTransferServico = postaArquivoFileTransferServico;
            this.fileSystem = fileSystem;
        }

        public void Postar(Pagina pagina, string arquivo)
        {
            if (string.IsNullOrEmpty(pagina.TipoArquivoOriginal) == false && this.fileSystem.Exists(this.NomeOriginal(arquivo, pagina.TipoArquivoOriginal)))
            {
                Log.Application.DebugFormat("Postando imagem JPG original");

                this.postaArquivoFileTransferServico.PostarPagina(
                    pagina.Id,
                    pagina.TipoArquivoOriginal,
                    this.NomeOriginal(arquivo, pagina.TipoArquivoOriginal));
            }

            this.postaArquivoFileTransferServico.PostarPagina(
                pagina.Id,
                pagina.TipoArquivo,
                arquivo);
        }

        private string NomeOriginal(string arquivoOriginal, string tipoOriginal)
        {
            var nomeArquivoOriginal = string.Format("{0}.{1}", Path.GetFileNameWithoutExtension(arquivoOriginal), tipoOriginal);
            return Path.Combine(Path.GetDirectoryName(arquivoOriginal), nomeArquivoOriginal);
        }
    }
}
