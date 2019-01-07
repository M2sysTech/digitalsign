namespace Veros.Paperless.Model.Boot
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using Data;
    using Entidades;
    using HumbleNetwork;
    using Repositorios;

    public class InicializacaoDeServicoWindows
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguracaoIpRepositorio configuracaoIpRepositorio;

        public InicializacaoDeServicoWindows(
            IUnitOfWork unitOfWork,
            IConfiguracaoIpRepositorio configuracaoIpRepositorio)
        {
            this.unitOfWork = unitOfWork;
            this.configuracaoIpRepositorio = configuracaoIpRepositorio;
        }

        public bool Validar(TagConfiguracaoIp servico)
        {
            var configuracaoIp = this.unitOfWork
                .Obter(() => this.configuracaoIpRepositorio.ObterPorTag(servico.Value));

            if (configuracaoIp == null)
            {
                throw new Exception("Serviço não Iniciado. Serviço não está configurado para rodar nesse servidor. Verifique os parametros na tabela ipconfig que deve ter a tag [" + servico.Value + "]");
            }

            var ip = this.GetLocalIpAddress();

            if (ip != configuracaoIp.Host)
            {
                throw new Exception("Serviço não Iniciado. Este servidor não está autorizado a rodar este serviço. Verifique parametros na tabela ipconfig -> tag :: [" + servico.Value + "]");
            }

            var server = new HumbleServer();
            server.Start(configuracaoIp.Porta);

            return true;
        }

        private string GetLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            throw new Exception("Local IP Address Não Encontrado!");
        }
    }
}