namespace Veros.Paperless.Infra.Storage
{
    using System;
    using System.IO;
    using Model.Entidades;
    using Model.Servicos.FileTransferBalance;
    using Veros.Paperless.Model.Repositorios;
    using Veros.Paperless.Model.Servicos;
    using Veros.Framework;
    using Veros.Framework.IO;
    using Veros.Storage.FileTransfer;

    public class BaixaArquivoFileTransferServico : IBaixaArquivoFileTransferServico
    {
        private readonly IConfiguracaoIpRepositorio configuracaoIpRepositorio;
        private readonly IObtemFileTransferDaPaginaServico obtemFileTransferDaPaginaServico;
        private readonly IObtemFileTransferParaArquivoServico obtemFileTransferParaArquivoServico;

        public BaixaArquivoFileTransferServico(IConfiguracaoIpRepositorio configuracaoIpRepositorio, 
            IObtemFileTransferDaPaginaServico obtemFileTransferDaPaginaServico, 
            IObtemFileTransferParaArquivoServico obtemFileTransferParaArquivoServico)
        {
            this.configuracaoIpRepositorio = configuracaoIpRepositorio;
            this.obtemFileTransferDaPaginaServico = obtemFileTransferDaPaginaServico;
            this.obtemFileTransferParaArquivoServico = obtemFileTransferParaArquivoServico;
        }

        public string BaixarArquivo(int id, string fileType, Veros.Paperless.Model.Entidades.FileTransfer fileTransfer, bool usarCache = true, bool baixarThumbnail = false)
        {
            return this.BaixarArquivo(fileTransfer, id, fileType, usarCache: usarCache, baixarThumbnail: baixarThumbnail);
        }

        public string BaixarArquivo(
            int id, 
            string fileType, 
            bool baixarArquivoOriginal = false, 
            bool manterNomeOriginal = false, 
            bool usarCache = true, 
            int dataCenterId = -1)
        {
            var fileTransferDoArquivo = this.obtemFileTransferDaPaginaServico.Obter(id, dataCenterId);

            return this.BaixarArquivo(fileTransferDoArquivo, id, fileType, baixarArquivoOriginal, manterNomeOriginal, usarCache);
        }

        public string BaixarArquivoGenerico(string caminhoFileTransfer, string caminhoDestinoLocal, int dataCenterId = -1)
        {
            var configuracaoIp = this.configuracaoIpRepositorio.ObterConfiguracaoFileTransfer(dataCenterId);

            Log.Application.DebugFormat("Filetransfer: arquivo solicitado: {0}", caminhoFileTransfer);

            Directories.CreateIfNotExist(Path.GetDirectoryName(caminhoDestinoLocal));

            try
            {
                using (var fileTransfer = FileTransferCliente.Obter())
                {
                    fileTransfer.Conectar(configuracaoIp.Host, configuracaoIp.Porta);
                    fileTransfer.Baixar(caminhoDestinoLocal, caminhoFileTransfer);
                }
            }
            catch (System.Exception exception)
            {
                if (exception.Message.Equals("Erro : No such file or directory"))
                {
                    throw new FileNotFoundException("Arquivo nao encontrado");
                }

                throw;
            }

            Log.Application.DebugFormat(
                "Filetransfer: baixou arquivo de {0} para {1}",
                caminhoFileTransfer,
                caminhoDestinoLocal);

            return caminhoDestinoLocal;
        }

        public string BaixarPacCortada(int documentoId)
        {
            var configuracaoIp = this.configuracaoIpRepositorio.ObterConfiguracaoFileTransfer();

            var imagemFileTransfer = string.Format(
                @"/PAC/{0}/F/{1}.TIF",
                Files.GetEcmPath(documentoId),
                documentoId.ToString("000000000")).ToUpper().Replace("\\", "/");

            var arquivoLocal = Path.Combine(
                CacheDeImagensLocal.CaminhoPadrao,
                documentoId.ToString("000000000") + ".TIF");

            try
            {
                using (var fileTransfer = FileTransferCliente.Obter())
                {
                    fileTransfer.Conectar(configuracaoIp.Host, configuracaoIp.Porta);
                    fileTransfer.Baixar(arquivoLocal, imagemFileTransfer);
                }
            }
            catch (System.Exception exception)
            {
                if (exception.Message.Equals("Erro : No such file or directory"))
                {
                    throw new FileNotFoundException("Recorte de Pac não encontrado");
                }

                throw;
            }

            return arquivoLocal;
        }

        public string BaixarFotoUsuario(string nomeArquivo)
        {
            var configuracaoIp = this.configuracaoIpRepositorio.ObterConfiguracaoFileTransfer();

            var imagemFileTransfer = string.Format(
              @"/USERFOTO/{0}",
              nomeArquivo);

            var arquivoLocal = Path.Combine(
                CacheDeImagensLocal.CaminhoFotosDeUsuario, 
                nomeArquivo);

            ////if (File.Exists(arquivoLocal))
            ////{
            ////    return arquivoLocal;
            ////}

            Directories.CreateIfNotExist(Path.GetDirectoryName(arquivoLocal));

            try
            {
                using (var fileTransfer = FileTransferCliente.Obter())
                {
                    fileTransfer.Conectar(configuracaoIp.Host, configuracaoIp.Porta);
                    fileTransfer.Baixar(arquivoLocal, imagemFileTransfer);
                }
            }
            catch (System.Exception exception)
            {
                if (exception.Message.Equals("Erro : No such file or directory"))
                {
                    throw new FileNotFoundException("Foto não encontrada");
                }

                throw;
            }

            return arquivoLocal;
        }

        public string BaixarPadraoDeFace(int faceId, string caminhoTxt)
        {
            var configuracaoIp = this.configuracaoIpRepositorio.ObterConfiguracaoFileTransfer();
    
            var fileName = string.Format(
                "{0}.{1}",
                caminhoTxt,
                "TXT");

            var arquivoLocal = Path.Combine(
                CacheDeImagensLocal.CaminhoPadrao,
                fileName);
            
            Log.Application.DebugFormat(
                "Filetransfer: arquivo solicitado: {0}",
                arquivoLocal);

            if (File.Exists(arquivoLocal))
            {
                return arquivoLocal;
            }

            var imagemFileTransfer = string.Format(@"/{0}/F/{1}",
                                                   Files.GetEcmPath(faceId),
                                                   fileName).ToUpper().Replace("\\", "/");

            Directories.CreateIfNotExist(Path.GetDirectoryName(arquivoLocal));

            try
            {
                using (var fileTransfer = FileTransferCliente.Obter())
                {
                    fileTransfer.Conectar(configuracaoIp.Host, configuracaoIp.Porta);
                    fileTransfer.Baixar(arquivoLocal, imagemFileTransfer);
                }
            }
            catch (System.Exception exception)
            {
                if (exception.Message.Equals("Erro : No such file or directory"))
                {
                    throw new FileNotFoundException("Arquivo nao encontrado");
                }

                throw;
            }

            Log.Application.DebugFormat(
                "Filetransfer: baixou arquivo de {0} para {1}",
                imagemFileTransfer,
                arquivoLocal);

            return arquivoLocal;
        }

        public string BaixarArquivoNaPasta(int id, string fileType, string pathDestino, bool baixarArquivoOriginal = false, int dataCenterId = -1)
        {
            var fileTransferDoArquivo = this.obtemFileTransferDaPaginaServico.Obter(id, dataCenterId);
            
            var fileName = baixarArquivoOriginal ?
                string.Format("{0}ORIGINAL.{1}", id.ToString("000000000"), fileType) :
                string.Format("{0}.{1}", id.ToString("000000000"), fileType);

            var diretorioDesejado = Path.GetDirectoryName(pathDestino);

            this.InternalBaixarArquivo(id, pathDestino, diretorioDesejado, fileName, fileTransferDoArquivo.ConfiguracaoIp);
           
            return pathDestino;
        }

        public string BaixarCertificadoGarantia(string arquivoOrigem, string caminhoDestinoLocal)
        {
            Directories.CreateIfNotExist(Path.GetDirectoryName(caminhoDestinoLocal));

            try
            {
                var configuracaoIp = this.obtemFileTransferParaArquivoServico.ObterMaisRecente();
                var fileTransferCerto = this.configuracaoIpRepositorio.ObterPorTag(configuracaoIp.Tag);

                using (var fileTransfer = FileTransferCliente.Obter())
                {
                    fileTransfer.Conectar(fileTransferCerto.Host, fileTransferCerto.Porta);
                    arquivoOrigem = string.Format(@"/CERTIFICADOS/{0}", arquivoOrigem);
                    fileTransfer.Baixar(caminhoDestinoLocal, arquivoOrigem);
                }
            }
            catch (System.Exception exception)
            {
                if (exception.Message.Equals("Erro : No such file or directory"))
                {
                    throw new FileNotFoundException("Arquivo nao encontrado");
                }

                throw;
            }

            Log.Application.DebugFormat("Filetransfer: baixou certificado de {0} para {1}", arquivoOrigem, caminhoDestinoLocal);
            return caminhoDestinoLocal;
        }

        public string BaixarArquivo(
            Veros.Paperless.Model.Entidades.FileTransfer fileTransferDoArquivo,
            int id,
            string fileType,
            bool baixarArquivoOriginal = false,
            bool manterNomeOriginal = false,
            bool usarCache = true,
            bool baixarThumbnail = false)
        {
            ////var fileTransferDoArquivo = this.obtemFileTransferDaPaginaServico.Obter(id, dataCenterId);
            var fileName = string.Format("{0}.{1}", id.ToString("000000000"), fileType);

            if (baixarArquivoOriginal)
            {
                fileName = string.Format("{0}ORIGINAL.{1}", id.ToString("000000000"), fileType);
            }

            if (baixarThumbnail)
            {
                fileName = string.Format("{0}_THUMB.{1}", id.ToString("000000000"), fileType);
            }

            var arquivoLocal = manterNomeOriginal ?
                    Path.Combine(CacheDeImagensLocal.CaminhoPadrao, fileName) :
                    Path.Combine(CacheDeImagensLocal.CaminhoPadrao,
                        fileName.Replace("ORIGINAL", string.Empty));

            Log.Application.DebugFormat(
                "Filetransfer: arquivo solicitado: {0}. UsarCache: {1}",
                arquivoLocal, usarCache);

            if (fileType.ToUpper().Equals("PDF"))
            {
                Log.Application.DebugFormat("Pagina #{0} não usará o cache pq é PDF", id);
                usarCache = false;
            }

            if (usarCache)
            {
                if (File.Exists(arquivoLocal))
                {
                    Log.Application.DebugFormat("Pagina #{0} recuperada do cache", id);
                    return arquivoLocal;
                }
            }

            var imagemFileTransfer = string.Format(@"/{0}/F/{1}",
                                                   Files.GetEcmPath(id),
                                                   fileName).ToUpper().Replace("\\", "/");

            Directories.CreateIfNotExist(Path.GetDirectoryName(arquivoLocal));

            try
            {
                using (var fileTransfer = FileTransferCliente.Obter())
                {
                    fileTransfer.Conectar(
                        fileTransferDoArquivo.ConfiguracaoIp.Host,
                        fileTransferDoArquivo.ConfiguracaoIp.Porta);

                    fileTransfer.Baixar(arquivoLocal, imagemFileTransfer);
                }
            }
            catch (System.Exception exception)
            {
                if (exception.Message.Equals("Erro : No such file or directory"))
                {
                    throw new FileNotFoundException("Arquivo nao encontrado");
                }

                if (exception.Message.Contains("Arquivo não encontrado no file transfer"))
                {
                    throw new FileNotFoundException("Arquivo nao encontrado");
                }

                throw;
            }

            Log.Application.DebugFormat(
                "Filetransfer[{2}]: baixou arquivo de {0} para {1}",
                imagemFileTransfer,
                arquivoLocal,
                fileTransferDoArquivo.ConfiguracaoIp.DataCenterId);

            return arquivoLocal;
        }

        private void InternalBaixarArquivo(int id, string pathDestino, string diretorioDesejado, string fileName, ConfiguracaoIp configuracaoIp)
        {
            Directories.CreateIfNotExist(Path.GetDirectoryName(diretorioDesejado));

            if (File.Exists(pathDestino))
            {
                try
                {
                    File.Delete(pathDestino);
                }
                catch (Exception exception)
                {
                    Log.Application.Error(string.Format("Não foi possivel excluir arquivo '{0}' antes de baixar", pathDestino), exception);
                }
            }

            var imagemFileTransfer = string.Format(@"/{0}/F/{1}", Files.GetEcmPath(id), fileName).ToUpper().Replace("\\", "/");
            Log.Application.DebugFormat("Filetransfer: arquivo solicitado: {0}", imagemFileTransfer);

            try
            {
                using (var fileTransfer = FileTransferCliente.Obter())
                {
                    fileTransfer.Conectar(configuracaoIp.Host, configuracaoIp.Porta);
                    fileTransfer.Baixar(pathDestino, imagemFileTransfer);
                }
            }
            catch (System.Exception exception)
            {
                if (exception.Message.Equals("Erro : No such file or directory"))
                {
                    throw new FileNotFoundException("Arquivo nao encontrado");
                }

                throw;
            }

            Log.Application.DebugFormat("Filetransfer[{2}] - Baixar na Pasta: baixou arquivo de {0} para {1}", imagemFileTransfer, pathDestino, configuracaoIp.DataCenterId);
        }
    }
}