//-----------------------------------------------------------------------
// <copyright file="ITcpClient.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Network
{
    using System;
    using System.Net.Sockets;

    /// <summary>
    /// Contrato para cliente tcp
    /// </summary>
    public interface ITcpClient : IDisposable
    {
        /// <summary>
        /// Gets socket do cliente
        /// </summary>
        Socket Client
        {
            get;
        }

        /// <summary>
        /// Retorna stream da conexão
        /// </summary>
        /// <returns>Stream da conexão</returns>
        INetworkStream GetStream();

        /// <summary>
        /// Conecta a um servidor
        /// </summary>
        /// <param name="host">Nome/ip do servidor</param>
        /// <param name="port">Porta da conexão</param>
        void Connect(string host, int port);

        void Disconnect();
    }
}
