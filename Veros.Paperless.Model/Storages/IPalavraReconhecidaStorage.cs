namespace Veros.Paperless.Model.Storages
{
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;

    public interface IPalavraReconhecidaStorage
    {
        IList<PalavraReconhecida> Obter(int documentoId);

        void Adicionar(int documentoId, IList<PalavraReconhecida> palavrasReconhecidas);

        void Apagar(int documentoId);
    }
}