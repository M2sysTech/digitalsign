namespace Veros.Paperless.Model.Servicos.Batimento
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class AreaRetangularDeBusca
    {
        private readonly int pontoXLeft;

        private readonly int pontoYTop;

        private readonly int pontoYBottom;

        public AreaRetangularDeBusca(OcorrenciaCampoDataNaCnh listaDatas)
        {
            this.Retangulo = new Rectangle();

            if (listaDatas.QuantidadeDatasEncontradas == 3)
            {
                this.pontoXLeft = listaDatas.ObterPosicao(1).Localizacao.Left;
                this.pontoYTop = (int)Math.Floor(listaDatas.ObterPosicao(0).Localizacao.Top + (0.21 * Math.Abs(listaDatas.ObterPosicao(0).Localizacao.Top - listaDatas.ObterPosicao(2).Localizacao.Top)));
                this.pontoYBottom = (int)Math.Floor(listaDatas.ObterPosicao(0).Localizacao.Top + (0.6666 * Math.Abs(listaDatas.ObterPosicao(0).Localizacao.Top - listaDatas.ObterPosicao(2).Localizacao.Top)));

                var width = Math.Abs(listaDatas.ObterPosicao(0).Localizacao.Right - this.pontoXLeft);
                var height = Math.Abs(this.pontoYBottom - this.pontoYTop);
                this.Retangulo = new Rectangle(this.pontoXLeft, 
                        this.pontoYTop,
                        width,
                        height);
            }

            if (listaDatas.QuantidadeDatasEncontradas == 2)
            {
                this.pontoXLeft = Math.Abs(listaDatas.ObterPosicao(0).Localizacao.Right - 288);
                this.pontoYTop = (int)Math.Floor(listaDatas.ObterPosicao(0).Localizacao.Top + (0.21 * Math.Abs(listaDatas.ObterPosicao(0).Localizacao.Top - listaDatas.ObterPosicao(1).Localizacao.Top)));
                this.pontoYBottom = (int)Math.Floor(listaDatas.ObterPosicao(0).Localizacao.Top + (0.6666 * Math.Abs(listaDatas.ObterPosicao(0).Localizacao.Top - listaDatas.ObterPosicao(1).Localizacao.Top)));

                var width = Math.Abs(listaDatas.ObterPosicao(0).Localizacao.Right - this.pontoXLeft);
                var height = Math.Abs(this.pontoYBottom - this.pontoYTop);
                this.Retangulo = new Rectangle(this.pontoXLeft, 
                    this.pontoYTop, 
                    width, 
                    height);
            }
        }

        public Rectangle Retangulo
        {
            get;
            set;
        }
    }
}
