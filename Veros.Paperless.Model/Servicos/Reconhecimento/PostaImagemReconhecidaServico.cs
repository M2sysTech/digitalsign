namespace Veros.Paperless.Model.Servicos.Reconhecimento
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using AForge.Imaging.Formats;
    using Entidades;
    using Framework;
    using Framework.IO;
    using Image.Reconhecimento;
    using Repositorios;
    using TrataImagem;

    public class PostaImagemReconhecidaServico : IPostaImagemReconhecidaServico
    {
        private readonly IPostaArquivoFileTransferServico postaArquivoFileTransferServico;
        private readonly IFileSystem fileSystem;
        private readonly IPaginaRepositorio paginaRepositorio;

        public PostaImagemReconhecidaServico(
            IPostaArquivoFileTransferServico postaArquivoFileTransferServico, 
            IFileSystem fileSystem,
            IPaginaRepositorio paginaRepositorio)
        {
            this.postaArquivoFileTransferServico = postaArquivoFileTransferServico;
            this.fileSystem = fileSystem;
            this.paginaRepositorio = paginaRepositorio;
        }

        public void Executar(int paginaId, Imagem imagem, string imagemProcessada, ResultadoReconhecimento resultadoReconhecimento)
        {
            //// avalia o que deve ser postado, de acordo com a tag OCR_POSTAGEM_IMG (TipoPostagemOcr)
            switch (Contexto.TipoPostagemOcr)
            {
                case -1:
                    //// Não deve postar nada
                    return;
                case 0:
                    //// TIF padrão
                    break;
                case 1:
                    //// JPG original + ajustes pre-processado
                    imagemProcessada = this.ExecutarAjustesNaOriginal(imagem, imagemProcessada, resultadoReconhecimento);
                    break;
                default:
                    //// TIF padrão
                    break;
            }

            var extension = Path.GetExtension(imagem.Caminho);
            if (extension == null)
            {
                return;
            }

            if (this.fileSystem.Exists(imagemProcessada) == false)
            {
                return;
            }

            var extensao = extension
                    .Replace(".", string.Empty)
                    .ToUpper();

            var extensaoNovaImagem = Path.GetExtension(imagemProcessada);
            if (extensaoNovaImagem == null)
            {
                return;
            }

            extensaoNovaImagem = extensaoNovaImagem.Replace(".", string.Empty).ToUpper();
            var novaImagem = Path.Combine(
                Path.GetDirectoryName(imagemProcessada),
                paginaId + "." + extensaoNovaImagem);
            //// so continua se nova extensão for TIF
            if (extensaoNovaImagem != "TIF" && Contexto.TipoPostagemOcr == 0)
            {
                Log.Application.DebugFormat("Imagem não será postada no FT. Extensão diferente de TIF. Arquivo: {0}", novaImagem);
                return;
            }

            try
            {
                this.fileSystem.Copy(imagemProcessada, novaImagem);
            }
            catch (Exception exception)
            {
                Log.Application.Error(exception);
                return;
            }

            var caminhoImagemOriginalNoFileTransfer = string.Format(
                @"/{0}/F/{1}.{2}",
                Files.GetEcmPath(paginaId),
                paginaId.ToString("000000000"),
                extensao).Replace("\\", "/").ToUpper();

            this.EnviarImagensParaFileTransfer(
                paginaId, 
                imagem.Caminho,
                extensaoNovaImagem,
                novaImagem,
                caminhoImagemOriginalNoFileTransfer);

            this.ApagarImagensLocais(imagem.Caminho, imagemProcessada, novaImagem);

            ////try
            ////{
            ////    if (extensao.ToLower() != extensaoNovaImagem.ToLower())
            ////    {
            ////        this.paginaRepositorio.AtualizarPaginaTipoDocumento(paginaId, extensaoNovaImagem);    
            ////    }
            ////}
            ////catch (Exception exception)
            ////{
            ////    Log.Application.ErrorFormat(exception, "Erro ao tentar atualizar tipoArquivo para formato {0} na pagina {1}", extensaoNovaImagem, paginaId);
            ////}
        }

        public void SubstituirImagemMantendoOriginal(int paginaId, string caminhoImgOriginal, string imagemProcessada)
        {
            var extension = Path.GetExtension(caminhoImgOriginal);

            if (extension == null)
            {
                return;
            }

            if (this.fileSystem.Exists(imagemProcessada) == false)
            {
                return;
            }

            var extensao = extension
                    .Replace(".", string.Empty)
                    .ToUpper();

            var extensaoNovaImagem = Path.GetExtension(imagemProcessada);
            if (extensaoNovaImagem == null)
            {
                return;
            }

            extensaoNovaImagem = extensaoNovaImagem.Replace(".", string.Empty).ToUpper();

            var novaImagem = Path.Combine(
                Path.GetDirectoryName(imagemProcessada),
                paginaId + "." + extensaoNovaImagem);

            var caminhoImagemOriginalNoFileTransfer = string.Format(
                @"/{0}/F/{1}ORIGINAL.{2}",
                Files.GetEcmPath(paginaId),
                paginaId.ToString("000000000"),
                extensao).Replace("\\", "/").ToUpper();

            this.postaArquivoFileTransferServico.PostarArquivo(paginaId, caminhoImgOriginal, caminhoImagemOriginalNoFileTransfer);
            Log.Application.InfoFormat("Imagem Original reenviada: {0}", caminhoImgOriginal);

            this.postaArquivoFileTransferServico.PostarPagina(paginaId, extensaoNovaImagem, novaImagem);
            Log.Application.InfoFormat("Imagem processada enviada: {0}", novaImagem);

            try
            {
                var tamanhoImagem = new FileInfo(novaImagem).Length;
                this.paginaRepositorio.AtualizarTipoEtamanho(paginaId, extensaoNovaImagem, tamanhoImagem);
            }
            catch (Exception exception)
            {
                Log.Application.ErrorFormat(exception, "Erro ao tentar atualizar tipoArquivo para formato {0} na pagina {1}", extensaoNovaImagem, paginaId);
            }
        }

        private void EnviarImagensParaFileTransfer(int paginaId, string imagemOriginal, string extensao, string novaImagem, string imagemFileTransfer)
        {
            if (this.fileSystem.Exists(novaImagem) == false)
            {
                Log.Application.Error("Erro ao tentar salvar e postar imagem do engine. será mantida a imagem original. ");
                return;
            }

            if (this.fileSystem.Exists(imagemOriginal) == false)
            {
                Log.Application.Error("Erro ao tentar salvar e postar imagem original. serão mantidas as imagens originais de importação. ");
                return;
            }

            if (Path.GetExtension(novaImagem).ToUpper() == Path.GetExtension(imagemFileTransfer).ToUpper())
            {
                //// se for mesma extensao, precisa renomear para original
                imagemFileTransfer = imagemFileTransfer.Replace(Path.GetExtension(imagemFileTransfer), "ORIGINAL" + Path.GetExtension(imagemFileTransfer));
                this.postaArquivoFileTransferServico.PostarArquivo(paginaId, imagemOriginal, imagemFileTransfer);
                Log.Application.InfoFormat("Imagem Original reenviada: {0}", imagemOriginal);
            }
            
            this.postaArquivoFileTransferServico.PostarPagina(paginaId, extensao, novaImagem);
            Log.Application.InfoFormat("Imagem processada enviada: {0}", novaImagem);
        }

        private void ApagarImagensLocais(string imagemOriginal, string imagemProcessada, string novaImagem)
        {
            try
            {
                this.fileSystem.DeleteFile(novaImagem);
                this.fileSystem.DeleteFile(imagemOriginal);
                this.fileSystem.DeleteFile(imagemProcessada);
            }
            catch (Exception exception)
            {
                Log.Application.Error(exception);
            }
        }

        private string ExecutarAjustesNaOriginal(Imagem imagem, string imagemProcessada, ResultadoReconhecimento resultadoReconhecimento)
        {
            var funcoesAforge = IoC.Current.Resolve<IFuncoesAforge>();
            var novoNomeOriginalReprocessada = Path.Combine(Path.GetDirectoryName(imagemProcessada), Path.GetFileNameWithoutExtension(imagemProcessada) + "_reproc.JPG");

            var bitmap = ImageDecoder.DecodeFromFile(imagem.Caminho);
            Bitmap bitmapGirado;
            switch (resultadoReconhecimento.OrientacaoDetecada)
            {
                case 1:
                    bitmapGirado = funcoesAforge.GirarAngulo(bitmap, -90.0);
                    break;
                case 2:
                    bitmapGirado = funcoesAforge.GirarAngulo(bitmap, 90.0);
                    break;
                case 3:
                    bitmapGirado = funcoesAforge.GirarAngulo(bitmap, 180.0);
                    break;
                default:
                    //// faz nada (0)
                    bitmapGirado = (Bitmap)bitmap.Clone();
                    break;
            }

            bitmap.Dispose();
            var skew = resultadoReconhecimento.SkewDetectado;
            if (skew != 0.0)
            {
                var angulo = Math.Atan(skew);
                var bitmapComSkew = funcoesAforge.GirarAngulo(bitmapGirado, angulo);
                bitmapGirado.Dispose();
                bitmapGirado = bitmapComSkew;
            }

            try
            {
                funcoesAforge.SalvarComo(bitmapGirado, novoNomeOriginalReprocessada, ImageFormat.Jpeg, 8);
            }
            catch (Exception exception)
            {
                Log.Application.ErrorFormat(exception, "Erro ao tentar executar ajustes na imagem {0} ", novoNomeOriginalReprocessada);
            }

            bitmapGirado.Dispose();
            return novoNomeOriginalReprocessada;
        }
    }
}