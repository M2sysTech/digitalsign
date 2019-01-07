namespace Veros.Paperless.Model.Servicos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using FileTransferBalance;
    using Framework;
    using Framework.IO;
    using Repositorios;

    public class RemoverPaginaFileTransfer : IRemoverPaginaFileTransfer
    {
        private readonly ISsh ssh;
        private readonly IObtemFileTransferDaPaginaServico obtemFileTransferDaPaginaServico;
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IGravaLogDaPaginaServico gravaLogDaPaginaServico;

        public RemoverPaginaFileTransfer(
            ISsh ssh, 
            IObtemFileTransferDaPaginaServico obtemFileTransferDaPaginaServico, 
            IPaginaRepositorio paginaRepositorio, 
            IGravaLogDaPaginaServico gravaLogDaPaginaServico)
        {
            this.ssh = ssh;
            this.obtemFileTransferDaPaginaServico = obtemFileTransferDaPaginaServico;
            this.paginaRepositorio = paginaRepositorio;
            this.gravaLogDaPaginaServico = gravaLogDaPaginaServico;
        }

        public void Executar(Dictionary<Pagina, string> arquivos)
        {
            var primeiroArquvo = arquivos.FirstOrDefault(x => x.Key.DataCenterAntesCloud != 3);
            var primeiraPagina = primeiroArquvo.Key;

            if (primeiraPagina == null)
            {
                return;
            }

            var fileTransferDoArquivo = this.obtemFileTransferDaPaginaServico.Obter(primeiraPagina.Id, primeiraPagina.DataCenterAntesCloud);

            if (fileTransferDoArquivo.ConfiguracaoIp.ConfiguracaoSshValida() == false)
            {
                Log.Application.WarnFormat(
                    "Impossivel exclusão do arquivo da pagina #{0}. Configuração para conexão SSH não foi definida na tabela IpConfig do filetransfer",
                    primeiraPagina.Id);

                throw new InvalidOperationException("Impossivel exclusão do arquivo da pagina. Configuração para conexão SSH não foi definida na tabela IpConfig do filetransfer");
            }

            var sshContext = new SshContext
            {
                Host = fileTransferDoArquivo.ConfiguracaoIp.Host,
                PassPhrase = fileTransferDoArquivo.ConfiguracaoIp.PassPhrase,
                RemotePath = fileTransferDoArquivo.ConfiguracaoIp.RemotePath,
                Senha = fileTransferDoArquivo.ConfiguracaoIp.SenhaSsh,
                Usuario = fileTransferDoArquivo.ConfiguracaoIp.UsuarioSsh,
                UtilizarSshKeys = fileTransferDoArquivo.ConfiguracaoIp.UtilizaParDeChaves,
                CaminhoChavePrivada = fileTransferDoArquivo.ConfiguracaoIp.CaminhoChavePrivada
            };

            this.ssh.Conectar(sshContext);

            foreach (var arquivo in arquivos)
            {
                this.ssh.RemoverArquivo(arquivo.Value);
                var pagina = arquivo.Key;

                if (pagina.TipoArquivo == "JPG")
                {
                    var nomeArquivo = string.Format(
                        "{0:000000000}_THUMB.{1}",
                        pagina.Id,
                        pagina.TipoArquivo);

                    var caminhoRemoto = string.Format(
                        "{0}/F/{1}",
                        Files.GetEcmPath(pagina.Id),
                        nomeArquivo);

                    caminhoRemoto = caminhoRemoto.Replace("\\", "/").ToUpper();

                    this.ssh.RemoverArquivo(caminhoRemoto);

                    this.paginaRepositorio.MarcarComoRemovidoFileTransferM2(pagina.Id);

                    this.gravaLogDaPaginaServico.Executar(
                        LogPagina.AcaoPostadoNoCloud,
                        pagina.Id,
                        pagina.Documento.Id,
                        "Arquivo removido do filetransfer m2sys");
                }
            }

            this.ssh.Desconectar();
        }
    }
}