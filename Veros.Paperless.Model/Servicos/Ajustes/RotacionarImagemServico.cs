namespace Veros.Paperless.Model.Servicos.Ajustes
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using AForge.Imaging.Filters;
    using AForge.Imaging.Formats;

    public class RotacionarImagemServico : IRotacionarImagemServico
    {
        public string Executar(string arquivoOrigem, int angulo, ImageFormat formatoSaida = null)
        {
            ImageInfo imageInfo;

            var bitmap = ImageDecoder.DecodeFromFile(arquivoOrigem, out imageInfo);

            var rotationPaisagem = new RotateBilinear(angulo) { FillColor = Color.White };

            Bitmap imagemGirada;

            if (imageInfo.BitsPerPixel == 32)
            {
                var grayImage = Grayscale.CommonAlgorithms.Y.Apply(bitmap);
                imagemGirada = rotationPaisagem.Apply(grayImage);
                grayImage.Dispose();
            }
            else
            {
                imagemGirada = rotationPaisagem.Apply(bitmap);
            }

            bitmap.Dispose();
            
            var nomeArquivo = string.Format(
                "{0}{1}",
                Guid.NewGuid(),
                Path.GetExtension(arquivoOrigem));

            var nomeGirado = Path.Combine(
                ConfiguracaoAjusteImagem.CaminhoPadrao,
                nomeArquivo);

            if (formatoSaida == null)
            {
                formatoSaida = ImageFormat.Png;
            }

            imagemGirada.Save(nomeGirado, formatoSaida);
            imagemGirada.Dispose();

            return nomeGirado;
        }
    }
}