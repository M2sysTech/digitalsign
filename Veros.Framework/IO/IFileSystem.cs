//-----------------------------------------------------------------------
// <copyright file="IFileSystem.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.IO
{
    using System;

    public interface IFileSystem
    {
        bool Exists(string path);

        long GetFileSize(string fileName);

        void CreateFile(string fileName, string content);

        void CreateFile(string fileName, IStream stream);

        void DeleteFile(string fileName);

        string ReadFile(string file);

        string GetTempPath();

        string GetTempFileName();

        IStream CreateFileStream(string file);

        string CreateBase64(string file);

        void Copy(string source, string destiny);

        void CreateDirectory(string directory);

        bool CanWrite(string path);

        string[] GetFiles(string path, string searchPattern);

        DateTime GetFileCreationTime(string file);

        void CreateDirectoryIfNotExist(string directory);

        void CreateFileFromBase64(string fileName, string base64Content);
    }
}