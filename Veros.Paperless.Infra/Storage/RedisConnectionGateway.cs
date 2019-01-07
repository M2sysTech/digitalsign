namespace Veros.Paperless.Infra.Storage
{
    using System;
    using System.Net.Sockets;
    using BookSleeve;
    using Veros.Framework;

    public class RedisConnectionGateway
    {
        private const string RedisConnectionFailed = "Redis connection failed.";
        private static readonly object syncLock = new object();
        private static readonly object syncConnectionLock = new object();
        private static volatile RedisConnectionGateway instance;
        private RedisConnection connection;

        private RedisConnectionGateway()
        {
            this.connection = GetNewConnection();
        }

        public static string Host
        {
            get;
            set;
        }
        
        public static RedisConnectionGateway Current
        {
            get
            {
                if (instance == null)
                {
                    lock (syncLock)
                    {
                        if (instance == null)
                        {
                            instance = new RedisConnectionGateway();
                        }
                    }
                }

                return instance;
            }
        }

        public static RedisConnection Reconnect()
        {
            instance = null;

            return RedisConnectionGateway.Current.GetConnection();
        }

        public RedisConnection GetConnection()
        {
            try
            {
                lock (syncConnectionLock)
                {
                    if (this.connection == null)
                    {
                        this.connection = GetNewConnection();
                    }

                    if (this.connection.State == RedisConnectionBase.ConnectionState.Opening)
                    {
                        return this.connection;
                    }

                    if (this.connection.State == RedisConnectionBase.ConnectionState.Closing || this.connection.State == RedisConnectionBase.ConnectionState.Closed)
                    {
                        try
                        {
                            this.connection = GetNewConnection();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(RedisConnectionFailed, ex);
                        }
                    }

                    if (this.connection.State == RedisConnectionBase.ConnectionState.Shiny)
                    {
                        try
                        {
                            var openAsync = this.connection.Open();
                            this.connection.Wait(openAsync);
                        }
                        catch (SocketException ex)
                        {
                            throw new Exception(RedisConnectionFailed, ex);
                        }
                    }

                    return this.connection;
                }
            }
            catch (Exception exception)
            {
                Log.Application.Error(
                    "Erro ao solicitar conexão com o redis.",
                    exception);

                return null;
            }
        }

        private static RedisConnection GetNewConnection()
        {
            return new RedisConnection(
                ContextoInfra.Redis.Host /* change with config value of course */,
                syncTimeout: 5000,
                ioTimeout: 5000, 
                allowAdmin: true);
        }
    }
}
