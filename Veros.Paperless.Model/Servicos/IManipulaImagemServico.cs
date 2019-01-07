namespace Veros.Paperless.Model.Servicos
{
    using System.Drawing;
    using Entidades;

    public interface IManipulaImagemServico
    {
        string TratarParaPdf(string arquivoAtual, Bitmap bitmap, int bppAtual, Pagina pagina);

        bool ImagemEmBranco(ItemParaSeparacao itemParaSeparacao, int minWidthPixel, int minHeightPixel, int minMargemPixel);

        System.Drawing.Image ScaleImage(
            System.Drawing.Image image, 
            float maxWidth,
            float maxHeight);

        bool EhSeparador(Bitmap bitmapCrop1, Rectangle[] rects, string arquivo);

        ImagemProcessada ObterImagemParaProcessamento(string arquivo, bool usarMargem = true);
    }
}
