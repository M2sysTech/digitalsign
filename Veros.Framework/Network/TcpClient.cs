//-----------------------------------------------------------------------
// <copyright file="TcpClient.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Network
{
    using System.Net.Sockets;
    using Sockets = System.Net.Sockets;

    /// <summary>
    /// Cliente tcp
    /// </summary>
    public class TcpClient : ITcpClient
    {
        /// <summary>
        /// Implementação de tcp cliente do .NET Framework
        /// </summary>
        private Sockets.TcpClient tcpClient;

        /// <summary>
        /// Initializes a new instance of the TcpClient class
        /// </summary>
        public TcpClient()
        {
            this.tcpClient = new Sockets.TcpClient();
        }
        
        /// <summary>
        /// Initializes a new instance of the TcpClient class
        /// </summary>
        /// <param name="tcpClient">Implementação de tcp cliente do .NET Framework</param>
        public TcpClient(Sockets.TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
        }

        /// <summary>
        /// Gets socket do cliente
        /// </summary>
        public Socket Client
        {
            get { return this.tcpClient.Client; }
        }

        /// <summary>
        /// Retorna stream da conexão
        /// </summary>
        /// <returns>Stream da conexão</returns>
        public INetworkStream GetStream()
        {
            return new NetworkStream(this.tcpClient.GetStream());
        }

        /// <summary>
        /// Conecta a um servidor
        /// </summary>
        /// <param name="host">Nome/ip do servidor</param>
        /// <param name="port">Porta da conexão</param>
        public void Connect(string host, int port)
        {
            if (this.tcpClient.Connected == false)
            {
                this.tcpClient.Connect(host, port);    
            }
        }

        public void Disconnect()
        {
            if (this.Client != null)
            {
                this.Client.Disconnect(false);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            if (this.tcpClient.Client != null && this.tcpClient.Client.Connected)
            {
                this.tcpClient.Client.Shutdown(SocketShutdown.Both);
                
                if (this.tcpClient.Client.Connected)
                {
                    this.tcpClient.Client.Disconnect(false);
                }
                
                this.tcpClient.Client.Close();
            }

            if (this.tcpClient != null && this.tcpClient.Connected)
            {
                this.tcpClient.Close();
            }

#if DEBUG
            Log.Application.DebugFormat("Cliente {0} desconectado", this.GetHashCode());
#endif
        }
    }
}
