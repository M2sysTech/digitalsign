namespace Veros.Paperless.Model.Servicos.SeparacaoServiceEngine
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using AForge.Imaging.Filters;
    using AForge.Imaging.Formats;
    using Entidades;
    using Framework;
    using Framework.IO;

    public class GerarThumbnailServico : IGerarThumbnailServico
    {
        private readonly IPostaArquivoFileTransferServico postaArquivoFileTransferServico;

        public GerarThumbnailServico(IPostaArquivoFileTransferServico postaArquivoFileTransferServico)
        {
            this.postaArquivoFileTransferServico = postaArquivoFileTransferServico;
        }

        public static string NomeThumbnail(string nomeArquivoOriginal, string extensaoThumb)
        {
            return string.Format("{0}_THUMB.{1}", Path.GetFileNameWithoutExtension(nomeArquivoOriginal), extensaoThumb);
        }

        public void Executar(List<List<Pagina>> todasAsPaginas, string cacheLocalImagens)
        {
            foreach (var documento in todasAsPaginas)
            {
                foreach (var pagina in documento)
                {
                    if (true)
                    {
                        try
                        {
                            var caminhoArquivoBaixado = Path.Combine(cacheLocalImagens, pagina.Id.ToString("000000000") + "." + pagina.TipoArquivo);
                            var caminhoGirado = Path.Combine(Path.GetDirectoryName(caminhoArquivoBaixado), Path.GetFileNameWithoutExtension(caminhoArquivoBaixado) + "_Girada.JPG");
                            if (File.Exists(caminhoGirado))
                            {
                                caminhoArquivoBaixado = caminhoGirado;
                            }

                            var arquivoThumb = this.GerarThumbnail(caminhoArquivoBaixado);
                            var extensao = Path.GetExtension(arquivoThumb).ToUpper().Replace(".", string.Empty);
                            var caminhoThumbNoFileTransfer = string.Format(
                                    @"/{0}/F/{1}",
                                    Files.GetEcmPath(pagina.Id),
                                    GerarThumbnailServico.NomeThumbnail(pagina.Id.ToString("000000000"), extensao))
                                    .Replace("\\", "/").ToUpper();
                            this.postaArquivoFileTransferServico.PostarArquivo(pagina.Id, arquivoThumb, caminhoThumbNoFileTransfer);
                            pagina.ImagemFrenteEmBranco = "S";
                        }
                        catch (Exception exception)
                        {
                            Log.Application.Error(string.Format("Erro ao gerar thumbnail da pagina [{0}]", pagina.Id), exception);
                            pagina.ImagemFrenteEmBranco = "N";
                        }
                    }
                }
            }
        }

        private string GerarThumbnail(string caminhoCompletoDoArquivo)
        {
            ImageInfo imageInfo;
            var nomeSaida = Path.Combine(Path.GetDirectoryName(caminhoCompletoDoArquivo), string.Format("{0}_THUMB.JPG", Path.GetFileNameWithoutExtension(caminhoCompletoDoArquivo)));

            var bitmapOriginal = ImageDecoder.DecodeFromFile(caminhoCompletoDoArquivo, out imageInfo);
            int width;
            int height;
            if (bitmapOriginal.Width > bitmapOriginal.Height)
            {
                width = Contexto.DimensoesThumbnail[1];
                height = Contexto.DimensoesThumbnail[0];
            }
            else
            {
                width = Contexto.DimensoesThumbnail[0];
                height = Contexto.DimensoesThumbnail[1];
            }

            var filter = new ResizeNearestNeighbor(width, height);
            var bmpThumb = filter.Apply(bitmapOriginal);
            this.SalvarComo(bmpThumb, nomeSaida, ImageFormat.Jpeg, 8, Contexto.QualidadeThumbnail);
            bmpThumb.Dispose();
            bitmapOriginal.Dispose();
            return nomeSaida;
        }

        private void SalvarComo(Bitmap imagemAtual, string nomeAlterado, ImageFormat imageFormat, long bpp, long quality)
        {
            var codecAtual = ImageCodecInfo.GetImageEncoders().Where(codec => codec.FormatID.Equals(imageFormat.Guid)).FirstOrDefault();
            if (codecAtual != null)
            {
                var parameters = new EncoderParameters(2);
                parameters.Param[0] = new EncoderParameter(Encoder.ColorDepth, bpp);
                parameters.Param[1] = new EncoderParameter(Encoder.Quality, quality);
                imagemAtual.Save(nomeAlterado, codecAtual, parameters);
            }
            else
            {
                throw new Exception(string.Format("Codec informado não foi localizado:{0}", imageFormat.Guid));
            }
        }
    }
}