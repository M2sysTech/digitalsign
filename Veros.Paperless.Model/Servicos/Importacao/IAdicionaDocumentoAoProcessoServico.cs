namespace Veros.Paperless.Model.Servicos.Importacao
{
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;

    public interface IAdicionaDocumentoAoProcessoServico
    {
        void Adicionar(int loteId, IList<ImagemConta> imagem);
    }
}
