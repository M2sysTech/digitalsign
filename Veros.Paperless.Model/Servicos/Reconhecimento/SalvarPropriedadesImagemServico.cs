namespace Veros.Paperless.Model.Servicos.Reconhecimento
{
    using System.IO;
    using Entidades;
    using Framework;
    using Framework.IO;
    using Repositorios;

    public class SalvarPropriedadesImagemServico : ISalvarPropriedadesImagemServico
    {
        private readonly dynamic arquivoImagem;
        private readonly IImagemRepositorio imagemRepositorio;
        private readonly IFileSystem fileSystem;

        public SalvarPropriedadesImagemServico(
            dynamic arquivoImagem, 
            IImagemRepositorio imagemRepositorio, 
            IFileSystem fileSystem)
        {
            this.arquivoImagem = arquivoImagem;
            this.imagemRepositorio = imagemRepositorio;
            this.fileSystem = fileSystem;
        }

        public void Executar(string caminhoImagem, Pagina pagina)
        {
            if (this.fileSystem.Exists(caminhoImagem) == false)
            {
                throw new FileNotFoundException(string.Format("Não foi possível carregar propriedades da imagem. Arquivo {0} não existe.", caminhoImagem));
            }

            this.arquivoImagem.AbrirDeArquivo(caminhoImagem);

            var imagem = new Imagem
            {
                Altura = this.arquivoImagem.DimensaoFisica.Height,
                Largura = this.arquivoImagem.DimensaoFisica.Width,
                Formato = this.arquivoImagem.FormatoPixel.ToInt(),
                ResolucaoHorizontal = this.arquivoImagem.ResolucaoHorizontal,
                ResolucaoVertical = this.arquivoImagem.ResolucaoVertical,
                Tamanho = this.arquivoImagem.Tamanho,
                QuantidadeCores = this.arquivoImagem.PaletaCores.Entries.Length,
                Pagina = pagina
            };

            this.imagemRepositorio.Salvar(imagem);
        }
    }
}