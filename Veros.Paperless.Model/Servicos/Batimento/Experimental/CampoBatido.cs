namespace Veros.Paperless.Model.Servicos.Batimento.Experimental
{
    using Entidades;

    public class CampoBatido
    {
        public Indexacao Indexacao
        {
            get;
            set;
        }

        public bool Batido
        {
            get;
            set;
        }

        public TipoBatimento TipoBatimento
        {
            get;
            set;
        }
    }
}