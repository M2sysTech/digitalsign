//-----------------------------------------------------------------------
// <copyright file="NetworkStream.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Network
{
    using System;
    using System.IO;
    using System.Text;
    using IO;
    using Sockets = System.Net.Sockets;

    /// <summary>
    /// Stream de rede
    /// </summary>
    public class NetworkStream : INetworkStream
    {
        /// <summary>
        /// Tamanho do buffer de transferência
        /// </summary>
        private const int BufferSize = 1024 * 1000; // 1 mega

        /// <summary>
        /// Implementação de network stream do .NET Framework
        /// </summary>
        private readonly Sockets.NetworkStream stream;

        /// <summary>
        /// Initializes a new instance of the NetworkStream class
        /// </summary>
        /// <param name="stream">Implementação de network stream do .NET Framework</param>
        public NetworkStream(Sockets.NetworkStream stream)
        {
            this.stream = stream; 
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, 
        /// or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.stream.Dispose();
        }

        /// <summary>
        /// Envia dados no stream de rede
        /// </summary>
        /// <param name="data">Dados para ser enviado no stream de rede</param>
        public void Send(long data)
        {
            var dataBuffer = data.ToBytes();
            this.stream.Write(dataBuffer, 0, dataBuffer.Length);

#if DEBUG
            Log.Framework.DebugFormat("Stream {0} enviou {1}", this.GetHashCode(), data);
#endif
        }

        /// <summary>
        /// Envia dados no stream de rede
        /// </summary>
        /// <param name="data">Dados para ser enviado no stream de rede</param>
        public void Send(string data)
        {
            var dataBuffer = data.ToBytes();
            this.stream.Write(dataBuffer, 0, dataBuffer.Length);

#if DEBUG
            Log.Framework.DebugFormat("Stream {0} enviou {1}", this.GetHashCode(), data);
#endif
        }

        /// <summary>
        /// Envia dados no stream de rede
        /// </summary>
        /// <param name="data">Dados para ser enviado no stream de rede</param>
        public void Send(byte[] data)
        {
            this.stream.Write(data, 0, data.Length);

#if DEBUG
            Log.Framework.DebugFormat("Stream {0} enviou {1}", this.GetHashCode(), data);
#endif
        }

        /// <summary>
        /// Envia dados no stream da rede
        /// </summary>
        /// <param name="data">Dados para ser enviado no stream de rede</param>
        /// <param name="length">Tamanho da mensagem</param>
        public void Send(string data, int length)
        {
            data = data.PadRight(length);
            this.Send(data);
        }

        /// <summary>
        /// Recebe dados do stream de rede
        /// </summary>
        /// <param name="size">Tamanho a ser lido</param>
        /// <typeparam name="T">Tipo do dado a ser recebido</typeparam>
        /// <returns>Dado recebido</returns>
        public T Receive<T>(int size)
        {
            var buffer = new byte[size];
          
            this.stream.Read(buffer, 0, buffer.Length);
            
            object value = null;

            if (typeof(T).Equals(typeof(string)))
            {
                value = Encoding.UTF8.GetString(buffer)
                    .Replace("\0", string.Empty)
                    .Trim();
            }
            else if (typeof(T).Equals(typeof(long)))
            {
                value = BitConverter.ToInt64(buffer, 0);
            }
            else if (typeof(T).Equals(typeof(byte[])))
            {
                value = buffer;
            }

#if DEBUG
            Log.Framework.DebugFormat("Stream {0} recebeu {1}", this.GetHashCode(), (T)value);
#endif

            return (T)value;
        }

        /// <summary>
        /// Envia um arquivo no stream de rede
        /// </summary>
        /// <param name="pathSource">Caminho do arquivo a ser enviado</param>
        /// <param name="fileSize">Tamanho do arquivo</param>
        public void SendFile(string pathSource, long fileSize)
        {
#if DEBUG
            Log.Framework.DebugFormat("Stream {0} irá enviar arquivo {1} de {2} bytes", this.GetHashCode(), pathSource, fileSize);
#endif

            using (var reader = new System.IO.FileStream(pathSource, FileMode.Open))
            {
                var totalSendedBytes = 0;

                try
                {
                    while (fileSize > totalSendedBytes)
                    {
                        var bufferSize = this.GetBufferSize(fileSize, totalSendedBytes);
                        var fileBuffer = new byte[bufferSize];

                        var sendedBytes = reader.Read(fileBuffer, 0, fileBuffer.Length);

                        this.stream.Write(fileBuffer, 0, fileBuffer.Length);
                        totalSendedBytes += sendedBytes;

#if DEBUG
                        Log.Framework.DebugFormat("Stream {0} enviou {1} de {2} bytes. Restam {3} bytes", this.GetHashCode(), sendedBytes, fileSize, fileSize - totalSendedBytes);
#endif
                    }
                }
#if DEBUG
                catch (Exception exception)
                {
                    Log.Application.Error(string.Format("Stream {0} não conseguiu enviar arquivo", this.GetHashCode()), exception);
                }
#else
                catch
                {
                }
#endif

                reader.Close();
            }

#if DEBUG
            Log.Application.DebugFormat("Stream {0} enviu o arquivo {1}", this.GetHashCode(), pathSource);
#endif
        }

        /// <summary>
        /// Recebe arquivo do stream de rede
        /// </summary>
        /// <param name="pathDestination">Caminho de destino</param>
        /// <param name="fileSize">Tamanho do arquivo</param>
        public void ReceiveFile(string pathDestination, long fileSize)
        {
            Directories.CreateIfNotExist(pathDestination);

#if DEBUG
            Log.Application.DebugFormat("Stream {0} irá receber o arquivo {1} de {2} bytes", this.GetHashCode(), pathDestination, fileSize);
#endif

            using (var writer = File.Create(pathDestination))
            {
                var totalBytesRead = 0;

                while (fileSize > totalBytesRead)
                {
                    var bufferSize = this.GetBufferSize(fileSize, totalBytesRead);
                    var buffer = new byte[bufferSize];
                    var bytesRead = this.stream.Read(buffer, 0, buffer.Length);

                    writer.Write(buffer, 0, bytesRead);
                    totalBytesRead += bytesRead;

#if DEBUG
                    Log.Application.DebugFormat("Stream {0} recebeu {1} de {2} bytes. Restam {3} bytes", this.GetHashCode(), bytesRead, fileSize, fileSize - totalBytesRead);
#endif
                }

                writer.Flush();
                writer.Close();
            }

#if DEBUG
            Log.Application.DebugFormat("Stream {0} recebeu arquivo {1}", this.GetHashCode(), pathDestination);
#endif
        }

        /// <summary>
        /// Calcula tamanho do buffer
        /// </summary>
        /// <param name="fileSize">Tamanho do arquivo</param>
        /// <param name="totalBytesRead">Bytes lido do arquivo</param>
        /// <returns>Tamanho do buffer de leitura</returns>
        private long GetBufferSize(long fileSize, long totalBytesRead)
        {
            long bufferSize;

            if (fileSize > totalBytesRead + NetworkStream.BufferSize)
            {
                bufferSize = NetworkStream.BufferSize;
            }
            else
            {
                bufferSize = fileSize - totalBytesRead;
            }

            return bufferSize;
        }
    }
}
