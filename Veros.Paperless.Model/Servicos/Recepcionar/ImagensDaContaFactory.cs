namespace Veros.Paperless.Model.Servicos.Recepcionar
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Entidades;
    using Framework.IO;
    using Newtonsoft.Json;

    public class ImagensDaContaFactory : IImagensDaContaFactory
    {
        private readonly IFileSystem fileSystem;
        private readonly ObterImagensCapturadas obterImagensCapturadas;

        public ImagensDaContaFactory(
            IFileSystem fileSystem,
            ObterImagensCapturadas obterImagensCapturadas)
        {
            this.fileSystem = fileSystem;
            this.obterImagensCapturadas = obterImagensCapturadas;
        }

        public IList<ImagemConta> Criar(Lote lote)
        {
            var diretorioImagens = Path.Combine(Configuracao.CaminhoDePacotesRecebidos, lote.Id.ToString());

            if (Directory.Exists(diretorioImagens) == false)
            {
                throw new FileNotFoundException("Diretorio de imagens do cpf não existe.");
            }

            var arquivos = this.fileSystem.GetFiles(diretorioImagens, "*.json");
            var arquivosDeImagens = arquivos.Where(x => x.Contains("capturafinalizada.json") == false);

            var arquivosCapturados = new List<ArquivoCapturado>();

            foreach (var arquivo in arquivosDeImagens)
            {
                var dataCriacao = this.fileSystem.GetFileCreationTime(arquivo);
                var json = this.fileSystem.ReadFile(arquivo);
                
                var imagemConta = JsonConvert.DeserializeObject<ImagemConta>(json);

                var imagem = new ArquivoCapturado
                {
                    DataCriacaoArquivo = dataCriacao,
                    TipoDocumentoId = imagemConta.TipoDocumentoId,
                    ImagemConta = imagemConta
                };

                arquivosCapturados.Add(imagem);
            }

            var imagensCapturadas = this.obterImagensCapturadas.Executar(arquivosCapturados);

            return imagensCapturadas;
        }
    }
}