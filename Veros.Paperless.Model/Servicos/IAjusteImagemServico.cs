namespace Veros.Paperless.Model.Servicos
{
    using System.Collections.Generic;
    using Entidades;

    public interface IAjusteImagemServico
    {
        void Aplicar(IList<AjusteDeDocumento> ajusteDeDocumento);
    }
}