namespace Veros.Paperless.Model.Entidades
{
    using System.Collections.Generic;

    public class SessaoDeCaptura
    {
        public int Id
        {
            get;
            set;
        }

        public string EnderecoIp
        {
            get;
            set;
        }

        public string Caminho
        {
            get;
            set;
        }

        public bool Enviado
        {
            get;
            set;
        }

        public bool Capturado
        {
            get;
            set;
        }

        public IList<ImagemDoLote> Imagens
        {
            get;
            set;
        }

        public string RequisicaoDe
        {
            get;
            set;
        }
    }
}