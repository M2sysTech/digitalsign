namespace Veros.Paperless.Model.Servicos.SeparacaoServiceEngine
{
    using System.Drawing;
    using Entidades;

    public interface ILocalizadorCarimboService
    {
        CarimboSeparador ExecutarPorRects(Bitmap bitmap, Rectangle[] rects);

        CarimboSeparador ExecutarPorBitmaps(ItemParaSeparacao itemParaSeparacao);
    }
}
