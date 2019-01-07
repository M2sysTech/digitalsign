namespace Veros.Paperless.Model.Entidades
{
    using System.Drawing;

    public class ImagemProcessada
    {
        public Rectangle[] Retangulos
        {
            get;
            set;
        }

        public Bitmap BitmapProcessado
        {
            get;
            set;
        }

        public Bitmap BitmapTopo
        {
            get;
            set;
        }

        public Bitmap BitmapDireita
        {
            get;
            set;
        }

        public Bitmap BitmapEsquerda
        {
            get;
            set;
        }

        public Bitmap BitmapBaixo
        {
            get;
            set;
        }
    }
}