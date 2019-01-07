namespace Veros.Paperless.Model.Servicos.Documentos
{
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;

    public class DetalhamentoDoDocumentoGrupo
    {
        public GrupoCampo GrupoCampo
        {
            get;
            set;
        }

        public IList<DetalhamentoDoDocumentoCampo> Campos
        {
            get;
            set;
        }
    }
}