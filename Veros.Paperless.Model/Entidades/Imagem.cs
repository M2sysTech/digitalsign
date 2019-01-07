namespace Veros.Paperless.Model.Entidades
{
    using Veros.Framework.Modelo;

    public class Imagem : Entidade
    {
        public virtual double Altura
        {
            get;
            set;
        }

        public virtual double Largura
        {
            get;
            set;
        }

        public virtual double ResolucaoHorizontal
        {
            get;
            set;
        }

        public virtual double ResolucaoVertical
        {
            get;
            set;
        }

        public virtual long Tamanho
        {
            get;
            set;
        }

        public virtual Pagina Pagina
        {
            get;
            set;
        }

        public virtual int Formato
        {
            get;
            set;
        }

        public virtual int QuantidadeCores
        {
            get;
            set;
        }

        public virtual string Caminho
        {
            get;
            set;
        }

        public virtual bool Original
        {
            get;
            set;
        }

        public virtual string Base64
        {
            get;
            set;
        }
    }
}