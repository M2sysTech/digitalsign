//-----------------------------------------------------------------------
// <copyright file="FileSystem.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.IO
{
    using System;
    using System.IO;

    /// <summary>
    /// TODO: checar testes para todos os metodos
    /// </summary>
    public class FileSystem : IFileSystem
    {
        public void CreateDirectoryIfNotExist(string directory)
        {
            Directories.CreateIfNotExist(directory);
        }

        public void CreateFileFromBase64(string fileName, string base64Content)
        {
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                using (var bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(base64Content);
                    bw.Write(data);
                    bw.Close();
                }
            }
        }

        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public long GetFileSize(string fileName)
        {
            return new FileInfo(fileName).Length;
        }

        public void CreateFile(string fileName, string content)
        {
            using (var writer = File.CreateText(fileName))
            {
                writer.Write(content);
                writer.Flush();
                writer.Close();
            }
        }

        public void CreateFile(string fileName, IStream stream)
        {
            //// TODO: PERFORMANCE buffered
            using (var reader = stream.GetStream())
            {
                var bytes = new byte[stream.Length];
                reader.Read(bytes, 0, bytes.Length);

                using (var fileStream = new FileStream(fileName, FileMode.Create))
                {
                    fileStream.Write(bytes, 0, bytes.Length);
                    fileStream.Flush();
                }
            }
        }

        public void DeleteFile(string fileName)
        {
            Files.DeleteIfExist(fileName);
        }

        public string ReadFile(string file)
        {
            return File.ReadAllText(file);
        }

        public string GetTempPath()
        {
            return Path.GetTempPath();
        }

        public string GetTempFileName()
        {
            try
            {
                return Path.GetTempFileName();
            }
            catch (System.Exception exception)
            {
                throw new IOException(
                    "Erro ao criar arquivo temporário. Considere limpar a pasta %TEMP%", exception);
            }
        }

        public IStream CreateFileStream(string file)
        {
            return new Framework.IO.Stream(new FileStream(file, FileMode.Open));
        }

        public string CreateBase64(string file)
        {
            return Convert.ToBase64String(File.ReadAllBytes(file));
        }

        public void Copy(string source, string destiny)
        {
            File.Copy(source, destiny, true);
        }

        public void CreateDirectory(string directory)
        {
            Directories.CreateIfNotExist(directory);
        }

        public bool CanWrite(string path)
        {
            try
            {
                using (File.Create(path + "\\test.tst", 1024, FileOptions.DeleteOnClose))
                {
                }

                Log.Framework.DebugFormat("Permissão na pasta {0} concedida", path);

                return true;
            }
            catch (Exception exception)
            {
                Log.Framework.ErrorFormat("Permissão na pasta {0} negada", exception);
                return false;
            }
        }

        public string[] GetFiles(string path, string pattern)
        {
            return Directory.GetFiles(path, pattern);
        }

        public DateTime GetFileCreationTime(string file)
        {
            return new FileInfo(file).CreationTime;
        }
    }
}