//-----------------------------------------------------------------------
// <copyright file="Converter.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------
namespace Veros.Framework.Image
{
    using System;
    using System.IO;

    /// <summary>
    /// Classe para conversao de imagens
    /// </summary>
    public class Converter
    {
        /// <summary>
        /// Converte arquivo em array de bytes
        /// </summary>
        /// <param name="imageFileName">caminho do arquivo a ser convertido</param>
        /// <returns>array de bytes</returns>
        public static byte[] FileToByte(string imageFileName)
        {
            Stream stm = (Stream)File.Open(imageFileName, FileMode.Open);
            byte[] buf = ConvertStreamToByte(stm, 0);
            stm.Close();
            stm.Dispose();
            return buf;
        }

        /// <summary>
        /// Converte um array de bytes em arquivo
        /// </summary>
        /// <param name="pathFileName">Caminho do arquivo completo para conversão</param>
        /// <param name="buffer">Array de bytes para conversão</param>
        public static void BytesToFile(string pathFileName, byte[] buffer)
        {
            FileStream newFile = new FileStream(pathFileName, FileMode.Create);

            newFile.Write(buffer, 0, buffer.Length);

            newFile.Close();
        }

        /// <summary>
        /// Reads data from a stream until the end is reached. The
        /// data is returned as a byte array. An IOException is
        /// thrown if any of the underlying IO calls fail.
        /// </summary>
        /// <param name="stream">The stream to read data from</param>
        /// <param name="initialLength">The initial buffer length</param>
        /// <returns>array de bytes</returns>
        private static byte[] ConvertStreamToByte(Stream stream, long initialLength)
        {
            // If we've been passed an unhelpful initial length, just
            // use 32K.
            if (initialLength < 1)
            {
                initialLength = 32768;
            }

            byte[] buffer = new byte[initialLength];
            int read = 0;

            int chunk;
            while ((chunk = stream.Read(buffer, read, buffer.Length - read)) > 0)
            {
                read += chunk;

                // If we've reached the end of our buffer, check to see if there's
                // any more information
                if (read == buffer.Length)
                {
                    int nextByte = stream.ReadByte();

                    // End of stream? If so, we're done
                    if (nextByte == -1)
                    {
                        return buffer;
                    }

                    // Nope. Resize the buffer, put in the byte we've just
                    // read, and continue
                    byte[] newBuffer = new byte[buffer.Length * 2];
                    Array.Copy(buffer, newBuffer, buffer.Length);
                    newBuffer[read] = (byte)nextByte;
                    buffer = newBuffer;
                    read++;
                }
            }
            
            // Buffer is now too big. Shrink it.
            byte[] ret = new byte[read];
            Array.Copy(buffer, ret, read);
            return ret;
        }
    }
}
