namespace Veros.Paperless.Model.Entidades
{
    using System.Collections.Generic;

    public class ImagemReconhecida
    {
        public IList<ValorReconhecido> ValoresReconhecidos
        {
            get;
            set;
        }

        public IList<PalavraReconhecida> Palavras
        {
            get;
            set;
        }
    }
}
