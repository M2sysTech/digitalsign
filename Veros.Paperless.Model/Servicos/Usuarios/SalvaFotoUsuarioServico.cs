namespace Veros.Paperless.Model.Servicos.Usuarios
{
    using System.IO;
    using Entidades;
    using Framework.IO;
    using Repositorios;

    public class SalvaFotoUsuarioServico : ISalvaFotoUsuarioServico
    {
        private readonly IFileSystem fileSystem;
        private readonly IPostaArquivoFileTransferServico postaArquivoFileTransferServico;
        private readonly IUsuarioRepositorio usuarioRepositorio;

        public SalvaFotoUsuarioServico(
            IFileSystem fileSystem,
            IPostaArquivoFileTransferServico postaArquivoFileTransferServico,
            IUsuarioRepositorio usuarioRepositorio)
        {
            this.fileSystem = fileSystem;
            this.postaArquivoFileTransferServico = postaArquivoFileTransferServico;
            this.usuarioRepositorio = usuarioRepositorio;
        }

        public void Salvar(Usuario usuario)
        {
            if (usuario.Id < 1 || string.IsNullOrEmpty(usuario.Foto))
            {
                return;
            }

            var caminho = this.PreparaCaminho();

            var arquivo = new Arquivo
            {
                FormatoBase64 = usuario.Foto
            };
            var nomeArquivo = string.Format("{0}{1}", usuario.Id, arquivo.ObterTipoArquivo()).ToUpper();
            var caminhoCompleto = Path.Combine(caminho, nomeArquivo);

            this.fileSystem.CreateFileFromBase64(caminhoCompleto, arquivo.ObterBase64());

            this.postaArquivoFileTransferServico.PostarFotoUsuario(nomeArquivo, caminhoCompleto);

            this.usuarioRepositorio.GravarNomeFoto(usuario.Id, nomeArquivo);
        }

        private string PreparaCaminho()
        {
            var caminho = Configuracao.CaminhoDeFotoDeUsuario;

            if (Directory.Exists(caminho) == false)
            {
                Directory.CreateDirectory(caminho);
            }

            return caminho;
        }
    }
}