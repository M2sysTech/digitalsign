namespace Veros.Paperless.Model.Servicos.Documentos
{
    using System.Collections.Generic;
    using System.Linq;

    public class CamposDeDocumento
    {
        public IList<DetalhamentoDoDocumentoCampo> Campos { get; set; }

        public DetalhamentoDoDocumentoCampo ObterCampo(string refArquivo)
        {
            return this.Campos.FirstOrDefault(x => x.Campo.ReferenciaArquivo == refArquivo);
        }
    }
}
