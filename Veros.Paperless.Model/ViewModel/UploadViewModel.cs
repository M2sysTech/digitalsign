namespace Veros.Paperless.Model.ViewModel
{
    using Castle.Components.Validator;
    using System.Collections.Generic;
    using Veros.Data.Validation;
    using Veros.Paperless.Model.Entidades;

    public class UploadViewModel : ViewModel
    {
        [ValidateRange(30, 60, "Tipo de documento inválido")]
        public int TypedocId
        {
            get;
            set;
        }

        public IList<TipoDocumento> TipoDocumentos
        {
            get;
            set;
        }

        public int LoteId
        {
            get;
            set;
        }

        public int ProcessoId
        {
            get;
            set;
        }

        public int DocumentoIdParaSubstituir
        {
            get;
            set;
        }
    }
}