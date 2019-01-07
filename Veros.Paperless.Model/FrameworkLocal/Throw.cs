namespace Veros.Paperless.Model.FrameworkLocal
{
    using System;
    using System.IO;
    using Framework;
    using Framework.IO;

    /// <summary>
    /// TODO: mover pro framework
    /// </summary>
    public static class Throw
    {
        public static void SeForNulo(object item, string exceptionMessage)
        {
            if (item == null)
            {
                throw new ArgumentNullException(exceptionMessage);
            }
        }

        public static void SeArquivoNaoExistir(string caminhoPacote, string exceptionMessage)
        {
            if (IoC.Current.Resolve<IFileSystem>().Exists(caminhoPacote) == false)
            {
                throw new FileNotFoundException(exceptionMessage);
            }
        }

        public static void SeArquivoEstiverZerado(string caminhoPacote, string exceptionMessage)
        {
            if (IoC.Current.Resolve<IFileSystem>().GetFileSize(caminhoPacote) == 0)
            {
                throw new Exception(exceptionMessage);
            }
        }
    }
}
