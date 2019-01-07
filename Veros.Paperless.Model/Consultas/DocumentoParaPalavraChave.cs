namespace Veros.Paperless.Model.Consultas
{
    using System.Collections.Generic;
    using Entidades;

    public class DocumentoParaPalavraChave
    {
        public int DocumentoId
        {
            get;
            set;
        }

        public int DocumentoPaiId
        {
            get;
            set;
        }

        public int TipoDocumentoId
        {
            get;
            set;
        }

        public int TipoDocumentoAposClassificacao
        {
            get;
            set;
        }

        public List<PalavraTipo> PalavrasTipos
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }
    }
}
