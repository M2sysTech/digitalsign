//-----------------------------------------------------------------------
// <copyright file="INetworkStream.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Network
{
    using System;

    /// <summary>
    /// Contrato para stream de rede
    /// </summary>
    public interface INetworkStream : IDisposable
    {
        /// <summary>
        /// Envia dados no stream de rede
        /// </summary>
        /// <param name="data">Dados para ser enviado no stream de rede</param>
        void Send(long data);

        /// <summary>
        /// Envia dados no stream de rede
        /// </summary>
        /// <param name="data">Dados para ser enviado no stream de rede</param>
        void Send(string data);

        /// <summary>
        /// Envia dados no stream de rede
        /// </summary>
        /// <param name="data">Dados para ser enviado no stream de rede</param>
        void Send(byte[] data);

        /// <summary>
        /// Envia dados no stream da rede
        /// </summary>
        /// <param name="data">Dados para ser enviado no stream de rede</param>
        /// <param name="length">Tamanho da mensagem</param>
        void Send(string data, int length);

        /// <summary>
        /// Envia um arquivo no stream de rede
        /// </summary>
        /// <param name="pathSource">Caminho do arquivo a ser enviado</param>
        /// <param name="fileSize">Tamanho do arquivo</param>
        void SendFile(string pathSource, long fileSize);

        /// <summary>
        /// Recebe dados do stream de rede
        /// </summary>
        /// <param name="size">Tamanho a ser lido</param>
        /// <typeparam name="T">Tipo do dado a ser recebido</typeparam>
        /// <returns>Dado recebido</returns>
        T Receive<T>(int size);

        /// <summary>
        /// Recebe arquivo do stream de rede
        /// </summary>
        /// <param name="pathDestination">Caminho de destino</param>
        /// <param name="fileSize">Tamanho do arquivo</param>
        void ReceiveFile(string pathDestination, long fileSize);
    }
}
