namespace Veros.Paperless.Model.Entidades
{
    using System.Drawing;
    using Framework.Modelo;

    public class PalavraReconhecida : Entidade
    {
        private Rectangle localizacao = Rectangle.Empty;

        public virtual string Texto
        {
            get;
            set;
        }

        public virtual int Esquerda
        {
            get;
            set;
        }

        public virtual int Topo
        {
            get;
            set;
        }

        public virtual int Largura
        {
            get;
            set;
        }

        public virtual int Altura
        {
            get;
            set;
        }

        public virtual int Direita
        {
            get;
            set;
        }

        public virtual Rectangle Localizacao
        {
            get
            {
                if (this.localizacao == Rectangle.Empty)
                {
                    this.localizacao = new Rectangle(this.Esquerda, this.Topo, this.Largura, this.Altura);
                }

                return this.localizacao;
            }
        }

        public virtual Pagina Pagina
        {
            get;
            set;
        }
    }
}