//-----------------------------------------------------------------------
// <copyright file="TcpListener.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Network
{
    using System;
    using System.Net;
    using Sockets = System.Net.Sockets;

    public class TcpListener : ITcpListener
    {
        private Sockets.TcpListener listener;

        public EndPoint LocalEndpoint
        {
            get { return this.listener.LocalEndpoint; }
        }

        public int Port
        {
            get;
            set;
        }

        public IPAddress IpAddress
        {
            get;
            set;
        }

        public void Start()
        {
            this.listener = new Sockets.TcpListener(this.IpAddress, this.Port);
            this.listener.Start();
        }

        public void Stop()
        {
            if (this.listener != null)
            {
                this.listener.Stop();
            }
        }

        public void BeginAcceptTcpClient(AsyncCallback callback, object state)
        {
            this.listener.BeginAcceptTcpClient(callback, state);
        }

        public ITcpClient EndAcceptTcpClient(IAsyncResult asyncResult)
        {
            return new TcpClient(this.listener.EndAcceptTcpClient(asyncResult));
        }
    }
}
