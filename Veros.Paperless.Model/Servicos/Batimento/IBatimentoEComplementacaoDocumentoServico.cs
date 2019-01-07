namespace Veros.Paperless.Model.Servicos.Batimento
{
    using Entidades;
    using System.Collections.Generic;

    public interface IBatimentoEComplementacaoDocumentoServico
    {
        void Execute(Documento documento, ImagemReconhecida valoresReconhecidos, List<int> camposBatidosId = null);
    }
}