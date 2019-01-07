namespace Veros.Paperless.Model.Servicos.Aprovacao
{
    using System.Collections.Generic;
    using Entidades;

    public class Aprovacao
    {
        public Processo Processo
        {
            get;
            set;
        }

        public IList<RegraViolada> RegrasVioladas
        {
            get;
            set;
        }

        public Documento Pac
        {
            get; set;
        }

        public IList<Participante> Participantes
        {
            get;
            set;
        }

        public int ContadorProdutividadeUsuario
        {
            get; 
            set;
        }

        public int PaginaDocumento1
        {
            get;
            set;
        }

        public int PaginaDocumento2
        {
            get;
            set;
        }

        public int TipoDocumento1
        {
            get;
            set;
        }

        public int TipoDocumento2
        {
            get;
            set;
        }

        public bool PossuiRegrasDeFraude
        {
            get;
            set;
        }

        public bool SendoTratadaPorOutroUsuario
        {
            get;
            set;
        }
    }
}