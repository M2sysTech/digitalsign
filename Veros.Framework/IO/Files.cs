//-----------------------------------------------------------------------
// <copyright file="Files.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Tratamento de arquivos. TODO: usar IFile
    /// </summary>
    public class Files
    {
        private static readonly IList<string> imageExtensions = new[]
        {
            "tif", 
            "bmp", 
            "pcx", 
            "gif", 
            "jpg", 
            "jpeg", 
            "tiff", 
            "ico", 
            "cur", 
            "pcx", 
            "dcx", 
            "pcd", 
            "fpx", 
            "wmf", 
            "emf", 
            "dxf", 
            "tga", 
            "cmp", 
            "mac", 
            "jbg"
        };

        /// <summary>
        /// http://www.w3schools.com/media/media_mimeref.asp
        /// </summary>
        private static readonly IDictionary<string, string> mimeTypes = new Dictionary<string, string>
        {
            { "pdf", "application/pdf" },
            { "txt", "text/plain" },

            //// office
            { "doc", "application/msword" },
            { "docx", "application/msword" },
            { "xls", "application/vnd.ms-excel" },
            { "xlsx", "application/vnd.ms-excel" },
            { "ppt", "application/vnd.ms-powerpoint" },
            { "pptx", "application/vnd.ms-powerpoint" },
            { "pps", "application/vnd.ms-powerpoint" },
            { "ppsx", "application/vnd.ms-powerpoint" },
            
            //// imagens
            { "png", "image/png" },
            { "gif", "image/gif" },
            { "bmp", "image/bmp" },
            { "jpg", "image/jpeg" },
            { "jpeg", "image/jpeg" },
        };

        /// <summary>
        /// Retorna conteúdo do arquivo em bytes
        /// </summary>
        /// <param name="fileName">Nome do arquivo</param>
        /// <returns>Array de bytes</returns>
        public static byte[] GetContent(string fileName)
        {
            byte[] buffer;
            System.IO.FileStream fs = null;

            try
            {
                var fi = new FileInfo(fileName);
                buffer = new byte[fi.Length];
                fs = new System.IO.FileStream(fileName, FileMode.Open);
                fs.Read(buffer, 0, buffer.Length);
            }
            catch
            {
                return null;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }

            return buffer;
        }

        /// <summary>
        /// Retorna conteúdo de um arquivo
        /// </summary>
        /// <param name="fileName">Caminho do arquivo</param>
        /// <returns>Conteúdo do arquivo</returns>
        public static string ReadContent(string fileName)
        {
            string content;

            using (StreamReader reader = File.OpenText(fileName))
            {
                content = reader.ReadToEnd();
                reader.Close();
            }

            return content;
        }

        /// <summary>
        /// Retorna subcaminho do arquivo na estrutura M0 K0
        /// </summary>
        /// <param name="fileId">Id do arquivo</param>
        /// <returns>Subcaminho do arquivo na estrutura M0 K0</returns>
        public static string GetEcmPath(int fileId)
        {
            const string Mask = "m{0}\\k{1}";

            if (fileId == 0)
            {
                return string.Format(Mask, 0, 0);
            }

            int mega = fileId / 1000000;
            int kilo;
            
            if (mega == 0)
            {
                kilo = fileId / 1000; 
            }
            else
            {
                string file = fileId.ToString();
                kilo = Convert.ToInt32(file.Substring(file.Length - 6, 6)) / 1000;
            }

            return string.Format(Mask, mega, kilo);
        }

        /// <summary>
        /// Apaga arquivo se existir
        /// </summary>
        /// <param name="path">Caminho do arquivo</param>
        public static void DeleteIfExist(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        /// <summary>
        /// Mime type de uma extensão de arquivo
        /// </summary>
        /// <param name="extension">Extensao a ser checada. Ex: pdf, doc, xls</param>
        /// <returns>MimeType do arquivo</returns>
        public static string GetMimeType(string extension)
        {
            return mimeTypes.ContainsKey(extension) ? mimeTypes[extension] : "application/octetstream";
        }

        /// <summary>
        /// Checa se uma extensão é de imagem
        /// </summary>
        /// <param name="extension">Extensão a ser checada</param>
        /// <returns>True se uma extensão é de imagem</returns>
        public static bool IsImageExtension(string extension)
        {
            return Files.imageExtensions.Contains(extension.ToLower());
        }

        /// <summary>
        /// Retorna extensão do arquivo sem o ponto
        /// </summary>
        /// <param name="file">Caminho do arquivo</param>
        /// <returns>Extensão do arquivo sem o ponto</returns>
        public static string GetExtension(string file)
        {
            var extensionWithDot = Path.GetExtension(file);

            if (extensionWithDot == null || string.IsNullOrWhiteSpace(extensionWithDot))
            {
                return string.Empty;
            }

            return extensionWithDot.Remove(0, 1);
        }

        public static void CreateFile(string path, string data)
        {
            Directories.CreateIfNotExist(path);

            using (var stream = File.Open(path, FileMode.Create))
            {
                stream.Write(Encoding.UTF8.GetBytes(data), 0, data.Length);
                stream.Flush();
            }
        }

        public static IEnumerable<string> ReadLines(string file)
        {
            using (var fileStream = System.IO.File.OpenRead(file))
            {
                using (var reader = new StreamReader(fileStream, Encoding.Default))
                {
                    while (reader.EndOfStream == false)
                    {
                        yield return reader.ReadLine();
                    }
                }
            }
        }
    }
}