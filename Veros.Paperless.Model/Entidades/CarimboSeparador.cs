namespace Veros.Paperless.Model.Entidades
{
    using System.Drawing;

    ////     D         B A     
    ////     ▀         ▀ ▀
    ////        □ ■ □
    ////                 ■ C

    public class CarimboSeparador
    {
        public bool Localizado { get; set; }

        public Rectangle Posicao { get; set; }

        public bool Anulado { get; set; }

        public Rectangle PontoA { get; set; }

        public Rectangle PontoB { get; set; }

        public Rectangle PontoC { get; set; }

        public Rectangle PontoD { get; set; }

        public Rectangle AreaLocalizada()
        {
            var masterRect2 = Rectangle.Union(this.PontoA, this.PontoD);
            var masterRect3 = Rectangle.Union(masterRect2, this.PontoC);
            return masterRect3;
        }
    }
}