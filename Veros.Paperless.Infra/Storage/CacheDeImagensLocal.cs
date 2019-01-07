namespace Veros.Paperless.Infra.Storage
{
    using System;
    using System.IO;
    using Framework;
    using Framework.IO;
    using Model;

    public class CacheDeImagensLocal
    {
        private readonly IRelogio relogio;
        private readonly IFileSystem fileSystem;

        public CacheDeImagensLocal(IRelogio relogio, IFileSystem fileSystem)
        {
            this.relogio = relogio;
            this.fileSystem = fileSystem;
        }

        public static string CaminhoPadrao
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
            }
        }

        public static string CaminhoFotosDeUsuario
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UserFotos");
            }
        }

        public void ApagarImagensAntigas(string caminho, int dias)
        {
            Log.Application.Info("Limpando cache de imagens");
            Log.Application.InfoFormat("Caminho: {0}", caminho);
            Log.Application.InfoFormat("Apagando imagens com mais de {0} dia(s)", dias);

            foreach (var file in this.fileSystem.GetFiles(caminho, "*.*"))
            {
                this.ApagarArquivoSeForAntigo(dias, file);
            }

            Log.Application.Info("Concluído limpeza do cache");
        }

        private void ApagarArquivoSeForAntigo(int dias, string file)
        {
            var creationTime = this.fileSystem.GetFileCreationTime(file);
            var agora = this.relogio.Agora();

            if (agora.Subtract(creationTime).TotalDays > dias)
            {
                this.fileSystem.DeleteFile(file);
                Log.Application.DebugFormat("Apagado do cache {0}", file);
            }
        }
    }
}