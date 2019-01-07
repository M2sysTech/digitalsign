namespace Veros.Paperless.Model.Servicos.SeparacaoServiceEngine
{
    using System.Drawing;

    public static class RectangleExtensions
    {
        public static Point Centro(this Rectangle rectangle)
        {
            return new Point(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2);
        }
    }
}
