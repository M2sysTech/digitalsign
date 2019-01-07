namespace Veros.Paperless.Model.Servicos.Documentos
{
    using System.Collections.Generic;

    public class DetalhamentoDoDocumento
    {
        public IList<DetalhamentoDoDocumentoGrupo> Grupos
        {
            get;
            set;
        }
    }
}