namespace Veros.Paperless.Model.Servicos.Pacotes
{
    using System.IO;
    using Repositorios;
    using Veros.Framework.IO;
    using Veros.Paperless.Model.Entidades;
    using System;
                 
    public class CriaArquivoPack : ICriaArquivoPack
    {
        private readonly IFileSystem fileSystem;
        private readonly IArquivoPackRepositorio arquivoPackRepositorio;

        public CriaArquivoPack(
            IFileSystem fileSystem, 
            IArquivoPackRepositorio arquivoPackRepositorio)
        {
            this.fileSystem = fileSystem;
            this.arquivoPackRepositorio = arquivoPackRepositorio;
        }

        public void Executar(string pack, PacoteProcessado rajada, ArquivoPackStatus status, string observacao)
        {
            var arquivoPack = new ArquivoPack
            {
                PacoteProcessado = rajada,
                Data = DateTime.Now,
                NomePacote = Path.GetFileNameWithoutExtension(pack),
                Tamanho = this.fileSystem.GetFileSize(pack),
                Status = status,
                Observacao = observacao 
            };

            this.arquivoPackRepositorio.Salvar(arquivoPack);
        }
    }
}
