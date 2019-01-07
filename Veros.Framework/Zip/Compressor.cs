//-----------------------------------------------------------------------
// <copyright file="Compressor.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------
namespace Veros.Framework.Zip
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Compactação e descompactação
    /// </summary>
    public class Compressor
    {
        /// <summary>
        /// Senha para compactação e descompactação
        /// </summary>
        private const string PASSWORD = "v3r0s1@54321%1$2#3@4!5veros1@";

        /// <summary>
        /// Compactação de bytes
        /// </summary>
        /// <param name="bufferInput">Entrada de bytes</param>
        /// <returns>Retorna bytes compactados</returns>
        public static byte[] Compress(byte[] bufferInput)
        {
            ////Compacta os dados
            MemoryStream ms = new MemoryStream();
            DeflateStream compressedStream = new DeflateStream(ms, CompressionMode.Compress, true);
            compressedStream.Write(bufferInput, 0, bufferInput.Length);
            compressedStream.Close();
           
            byte[] compressedData = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(compressedData, 0, compressedData.Length);
            ms.Close(); 

            ////Gera Hash apartir de uma senha + o tamanho do dado compactado
            byte[] hash = ChangeHash(compressedData.Length.ToString() + PASSWORD);

            ////Gera array de bytes com tamanho dos dados compactados
            byte[] lengthCompress = Encoding.UTF8.GetBytes(compressedData.Length.ToString()); 

            ////Gera array de saida
            byte[] bufferOutput = new byte[compressedData.Length + hash.Length + lengthCompress.Length + 1];

            ////Inserindo tamanho dos dados compactados no array de saida
            lengthCompress.CopyTo(bufferOutput, 0);
            
            ////Inserindo dados compactados no array de saida
            compressedData.CopyTo(bufferOutput, lengthCompress.Length);

            ////Inserindo dados do hash no array de saida
            hash.CopyTo(bufferOutput, compressedData.Length + lengthCompress.Length);
           
            ////Inserindo indice de onde termina o array que representa o tamanho dos dados compactados
            //// e começa o array com os dados compactados
            bufferOutput.SetValue(Convert.ToByte(lengthCompress.Length), bufferOutput.Length - 1);
            
            return bufferOutput;
        }
        
        /// <summary>
        /// Descompactação de bytes
        /// </summary>
        /// <param name="bufferInput">Entrada de bytes compactados</param>
        /// <returns>Retorna bytes descompactados</returns>
        public static byte[] DeCompress(byte[] bufferInput)
        {
            ////Pegando indice de onde termina o array que representa o tamanho dos dados compactados
            //// e começa o array com os dados compactados
            int indexRef = Convert.ToInt16(bufferInput.GetValue(bufferInput.Length - 1));

            ////Pega o tamanho do dado compactado
            byte[] lengthCompress = new byte[indexRef];
            Array.Copy(bufferInput, 0, lengthCompress, 0, lengthCompress.Length);
            long lenCompress = Convert.ToInt64(ConvertBytesToString(lengthCompress));

            ////Pega os bytes para descompactar
            byte[] decompress = new byte[lenCompress];
            Array.Copy(bufferInput, indexRef, decompress, 0, lenCompress);

            ////Gera Hash apartir de uma senha + o tamanho do dado compactado
            byte[] hash = ChangeHash(lenCompress + PASSWORD);

            ////Pega o hash compactado para fazer comparação
            byte[] hashDecompress = new byte[bufferInput.Length - indexRef - lenCompress - 1];
            Array.Copy(bufferInput, indexRef + lenCompress, hashDecompress, 0, hashDecompress.Length);

            ////Compara os Hash para permitir a descompactação
            if (ConvertBytesToString(hash) != ConvertBytesToString(hashDecompress))
            {
                return new byte[] { 0, 0, 0 };
            }

            ////Descompactando e retornando array de bytes
            MemoryStream ms = new MemoryStream(decompress);
            DeflateStream decompressedStream = new DeflateStream(ms, CompressionMode.Decompress);

            byte[] buffer = ConvertStreamToBytes(decompressedStream);
            ms.Position = 0;
            return buffer;
        }

        /// <summary>
        /// Converte um Stream para um array de bytes
        /// </summary>
        /// <param name="stream">Stream de entrada</param>
       /// <returns>Retorna um array de bytes</returns>
       private static byte[] ConvertStreamToBytes(Stream stream)
        {
            byte[] buffer = new byte[32768];
            using (MemoryStream ms = new MemoryStream())
            {
                while (true)
                {
                    int read = stream.Read(buffer, 0, buffer.Length);
                    if (read <= 0)
                    {
                        return ms.ToArray();
                    }

                    ms.Write(buffer, 0, read);
                }
            }
        }

        /// <summary>
        /// Gera um Hash
        /// </summary>
        /// <param name="key">Chave de entrada</param>
        /// <returns>Retorna array de bytes do hash</returns>
        private static byte[] ChangeHash(string key)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
            return keyArray;
        }

        /// <summary>
        /// Converte array de bytes para string
        /// </summary>
        /// <param name="buffer">Buffer de entrada</param>
        /// <returns>String de saida</returns>
        private static string ConvertBytesToString(byte[] buffer)
        {
            ASCIIEncoding enc = new ASCIIEncoding();
            return enc.GetString(buffer);
        }
    }
}