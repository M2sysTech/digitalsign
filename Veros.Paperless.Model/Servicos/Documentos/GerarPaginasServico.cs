namespace Veros.Paperless.Model.Servicos.Documentos
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Entidades;
    using Framework;
    using Importacao;
    using Repositorios;

    public class GerarPaginasServico : IGerarPaginasServico
    {
        private readonly IPaginaFabrica paginaFabrica;
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IPostaArquivoFileTransferServico postaArquivoFileTransferServico;
        private readonly IPostaArquivoAmazonS3 postaArquivoAmazonS3;

        public GerarPaginasServico(
            IPaginaFabrica paginaFabrica,
            IPaginaRepositorio paginaRepositorio,
            IPostaArquivoFileTransferServico postaArquivoFileTransferServico, 
            IPostaArquivoAmazonS3 postaArquivoAmazonS3)
        {
            this.paginaFabrica = paginaFabrica;
            this.paginaRepositorio = paginaRepositorio;
            this.postaArquivoFileTransferServico = postaArquivoFileTransferServico;
            this.postaArquivoAmazonS3 = postaArquivoAmazonS3;
        }
        
        public void Executar(Documento documento, string caminhoImagens, bool apagarArquivosLocais = true)
        {
            var diretorio = new DirectoryInfo(caminhoImagens);
            var arquivos = diretorio.GetFiles("*", SearchOption.TopDirectoryOnly).ToList();
            var ordem = 0;

            foreach (var arquivo in arquivos)
            {
                this.CriarPaginaEPostarNoFileTransfer(documento, ordem, arquivo.FullName);
                ordem++;
            }

            if (apagarArquivosLocais)
            {
                this.ApagarImagens(caminhoImagens);
            }
        }

        public IEnumerable<Pagina> AdicionarPaginas(
            Documento documento, 
            string caminhoImagens, 
            string nomeArquivos,
            int ordem, 
            bool apagarArquivosLocais = true)
        {
            var paginas = new List<Pagina>();

            foreach (var nomeArquivo in nomeArquivos.Split(';'))
            {
                if (string.IsNullOrEmpty(nomeArquivo) || string.IsNullOrEmpty(nomeArquivo.Trim()))
                {
                    continue;
                }

                var caminhoArquivo = Path.Combine(caminhoImagens, nomeArquivo);
                paginas.Add(this.CriarPaginaEPostarNoFileTransfer(documento, ordem, caminhoArquivo));
            }

            if (apagarArquivosLocais)
            {
                this.ApagarImagens(caminhoImagens);
            }

            return paginas;
        }

        public void InserirArquivoUnico(Documento documento, string caminhoArquivo, bool cloudOk)
        {
            if (File.Exists(caminhoArquivo) == false)
            {
                throw new Exception(string.Format("InserirArquivoUnico: Arquivo não encontrado: {0}", caminhoArquivo));
            }

            this.CriarPaginaEPostarNoFileTransfer(documento, 0, caminhoArquivo, cloudOk);
            this.ApagarImagens(Path.GetDirectoryName(caminhoArquivo));
        }

        private Pagina CriarPaginaEPostarNoFileTransfer(Documento documento, int ordem, string arquivo, bool cloudOk = false)
        {
            var pagina = this.paginaFabrica.Criar(documento, ordem, arquivo);

            pagina.CloudOk = cloudOk;
            pagina.Status = PaginaStatus.StatusParaReconhecimento;
            pagina.FimOcr = null;
            
            this.paginaRepositorio.Salvar(pagina);

            if (cloudOk)
            {
                Log.Application.Debug("Enviando novo documento para cloudStorage");
                this.postaArquivoAmazonS3
                    .PostarPagina(pagina, arquivo);
            }
            else
            {
                Log.Application.Debug("Enviando novo documento para filetransfer");
                this.postaArquivoFileTransferServico
                    .PostarPagina(pagina, arquivo);
            }

            documento.Paginas.Add(pagina);

            return pagina;
        }

        private void ApagarImagens(string caminhoImagens)
        {
            try
            {
                Directory.Delete(caminhoImagens, true);
            }
            catch (Exception exception)
            {
                Log.Application.Error(string.Format("Erro ao excluir pasta de imagens [{0}]", caminhoImagens), exception);
            }
        }
    }
}
