namespace Veros.Paperless.Model.Servicos.Batimento
{
    using System.Collections.Generic;
    using Entidades;

    public interface IClassificaDocumentoServico
    {
        void Execute(Documento documento, ImagemReconhecida imagemReconhecida);
    }
}