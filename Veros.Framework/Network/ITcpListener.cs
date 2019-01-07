//-----------------------------------------------------------------------
// <copyright file="ITcpListener.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Network
{
    using System;
    using System.Net;

    /// <summary>
    /// Contrato para listener de tcp
    /// </summary>
    public interface ITcpListener
    {
        /// <summary>
        /// Gets end point da escuta
        /// </summary>
        EndPoint LocalEndpoint
        {
            get;
        }

        int Port
        {
            get;
            set;
        }

        IPAddress IpAddress
        {
            get;
            set;
        }

        /// <summary>
        /// Inicia escuta
        /// </summary>
        void Start();

        /// <summary>
        /// Pára a escuta
        /// </summary>
        void Stop();

        /// <summary>
        /// Begins an asynchronous operation to accept an incoming connection attempt.
        /// </summary>
        /// <param name="callback"> An System.AsyncCallback delegate that references the method to invoke when the operation is complete.</param>
        /// <param name="state">A user-defined object containing information about the accept operation. This object is passed to the callback delegate when the operation is complete.</param>
        void BeginAcceptTcpClient(System.AsyncCallback callback, object state);

        /// <summary>
        /// Asynchronously accepts an incoming connection attempt and creates a new System.Net.Sockets.TcpClient to handle remote host communication.
        /// </summary>
        /// <param name="asyncResult">An System.IAsyncResult returned by a call to the System.Net.Sockets.TcpListener.BeginAcceptTcpClient(System.AsyncCallback,System.Object) method.</param>
        /// <returns>Cliente tcp</returns>
        ITcpClient EndAcceptTcpClient(System.IAsyncResult asyncResult);
    }
}
