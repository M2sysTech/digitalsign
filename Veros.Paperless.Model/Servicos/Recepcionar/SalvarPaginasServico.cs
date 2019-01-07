namespace Veros.Paperless.Model.Servicos.Recepcionar
{
    using System.IO;
    using Entidades;
    using Framework;
    using Importacao;
    using Repositorios;
    using System;
    using System.Collections.Generic;

    public class SalvarPaginasServico : ISalvarPaginasServico
    {
        private readonly IPaginaFabrica criaPaginaFabrica;
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IPostaArquivoFileTransferServico postaArquivoFileTransferServico;
        private readonly IConversorFormatoImgServico conversorFormatoImgServico;

        public SalvarPaginasServico(
            IPaginaFabrica criaPaginaFabrica,
            IPaginaRepositorio paginaRepositorio,
            IPostaArquivoFileTransferServico postaArquivoFileTransferServico, 
            IConversorFormatoImgServico conversorFormatoImgServico)
        {
            this.criaPaginaFabrica = criaPaginaFabrica;
            this.paginaRepositorio = paginaRepositorio;
            this.postaArquivoFileTransferServico = postaArquivoFileTransferServico;
            this.conversorFormatoImgServico = conversorFormatoImgServico;
        }

        public void Executar(string[] imagens, Documento documento)
        {
            var contador = 0;
            foreach (var imagem in imagens)
            {
                contador++;
                this.Executar(imagem, documento, contador);
            }
        }

        public void Executar(string imagem, Documento documento, int contador = 1)
        {
            List<string> imagensConvertidas = null;
           
            imagensConvertidas = new List<string>() { imagem };

            foreach (var imagemConvertida in imagensConvertidas)
            {
                var pagina = this.CriarPagina(imagemConvertida, documento, contador);
                var extension = Path.GetExtension(imagemConvertida);

                if (extension == null)
                {
                    Log.Application.Info("Imagem está sem extenção >> " + imagemConvertida);
                    continue;
                }

                var extensao = extension
                    .Replace(".", string.Empty);

                this.postaArquivoFileTransferServico
                    .PostarPagina(pagina, imagemConvertida);
            }
        }

        private Pagina CriarPagina(string imagem, Documento documento, int paginaOrdem)
        {
            var pagina = this.criaPaginaFabrica.Criar(documento, paginaOrdem, imagem);
            this.paginaRepositorio.Salvar(pagina);
            documento.Paginas.Add(pagina);
            return pagina;
        }
    }
}