//-----------------------------------------------------------------------
// <copyright file="Stream.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.IO
{
    using System;
    using System.IO;

    public class Stream : IStream, IDisposable
    {
        private System.IO.Stream stream;

        public Stream()
        {
        }

        public Stream(string text)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(text);
            this.stream = new MemoryStream(bytes);
        }

        public Stream(System.IO.Stream stream)
        {
            this.stream = stream;
        }

        public long Length
        {
            get { return this.stream.Length; }
        }

        public void ReceiveFile(string fileName, long size)
        {
            var buffer = new byte[16 * 1024];
            long oldPosition = this.stream.Position;

            try
            {
                Files.DeleteIfExist(fileName);
                Directories.CreateIfNotExist(fileName);

                using (var fs = File.Create(fileName))
                {
                    this.stream.Position = 0;
                    int n;

                    while ((n = this.stream.Read(buffer, 0, 16 * 1024)) != 0)
                    {
                        fs.Write(buffer, 0, n);
                    }
                }
            }
            finally
            {
                this.stream.Position = oldPosition;
                this.stream.Close();
            }
        }

        public System.IO.Stream GetStream()
        {
            return this.stream;
        }

        public string GetText()
        {
            using (var streamReader = new System.IO.StreamReader(this.stream))
            {
                return streamReader.ReadToEnd();
            }
        }

        public void Dispose()
        {
            if (this.stream != null)
            {
                this.stream.Dispose();
            }
        }

        public IStream FromFile(string file)
        {
            this.stream = new FileStream(file, FileMode.Open);
            return this;
        }
    }
}
