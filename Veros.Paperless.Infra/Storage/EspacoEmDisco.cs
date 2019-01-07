namespace Veros.Paperless.Infra.Storage
{
    using System;
    using System.IO;
    using Veros.Framework;

    public class EspacoEmDisco
    {
        public static bool EhSuficiente()
        {
            var driversInfo = DriveInfo.GetDrives();

            double espacoLivreEmDisco = 0;

            foreach (var driverInfo in driversInfo)
            {
                var pathRoot = Path.GetPathRoot(Aplicacao.Caminho);

                if (pathRoot != null && driverInfo.RootDirectory.Name.ToUpper() == pathRoot.ToUpper())
                {
                    espacoLivreEmDisco = Convert.ToDouble(driverInfo.TotalFreeSpace) / Convert.ToDouble(driverInfo.TotalSize);
                }
            }

            return espacoLivreEmDisco > 0.05;
        } 
    }
}