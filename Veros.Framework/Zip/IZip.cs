//-----------------------------------------------------------------------
// <copyright file="IZip.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Zip
{
    /// <summary>
    /// Contrato para compactadores
    /// </summary>
    public interface IZip
    {
        /// <summary>
        /// Compacta um arquivo
        /// </summary>
        /// <param name="sourcePath">Arquivo original</param>
        /// <param name="destinationPath">Arquivo de destino compactado</param>
        void Zip(string sourcePath, string destinationPath);

        /// <summary>
        /// Descompacta um arquivo
        /// </summary>
        /// <param name="sourcePath">Arquivo compactado</param>
        /// <param name="destinationPath">Arquivo de destino descompactado</param>
        void Unzip(string sourcePath, string destinationPath);
    }
}
