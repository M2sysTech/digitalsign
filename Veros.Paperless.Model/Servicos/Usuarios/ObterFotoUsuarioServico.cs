namespace Veros.Paperless.Model.Servicos.Usuarios
{
    using Framework.IO;
    using Repositorios;

    public class ObterFotoUsuarioServico : IObterFotoUsuarioServico
    {
        private readonly IFileSystem fileSystem;
        private readonly IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico;
        private readonly IUsuarioRepositorio usuarioRepositorio;

        public ObterFotoUsuarioServico(
            IFileSystem fileSystem,
            IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico,
            IUsuarioRepositorio usuarioRepositorio)
        {
            this.fileSystem = fileSystem;
            this.baixaArquivoFileTransferServico = baixaArquivoFileTransferServico;
            this.usuarioRepositorio = usuarioRepositorio;
        }

        public string Obter(int usuarioId)
        {
            if (usuarioId < 1)
            {
                return string.Empty;
            }

            var usuario = this.usuarioRepositorio.ObterPorId(usuarioId);

            if (string.IsNullOrEmpty(usuario.NomeArquivoFoto))
            {
                return string.Empty;
            }

            var arquivoLocal = this.baixaArquivoFileTransferServico.BaixarFotoUsuario(usuario.NomeArquivoFoto);

            return this.fileSystem.CreateBase64(arquivoLocal);
        }
    }
}