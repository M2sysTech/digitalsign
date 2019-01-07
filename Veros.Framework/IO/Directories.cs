//-----------------------------------------------------------------------
// <copyright file="Directories.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.IO
{
    using System.IO;

    /// <summary>
    /// Extensão para diretórios
    /// </summary>
    public class Directories
    {
        /// <summary>
        /// Apaga diretório caso exista
        /// </summary>
        /// <param name="path">Diretório a ser apagado</param>
        public static void DeleteIfExist(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        /// <summary>
        /// Cria diretório caso não exista
        /// </summary>
        /// <param name="fileName">Caminho do diretório</param>
        public static void CreateIfNotExist(string fileName)
        {
            var directory = Path.HasExtension(fileName) ? Path.GetDirectoryName(fileName) : fileName;

            if (string.IsNullOrEmpty(directory) == false)
            {
                if (Directory.Exists(directory) == false)
                {
                    Directory.CreateDirectory(directory);
                }
            }
        }
    }
}