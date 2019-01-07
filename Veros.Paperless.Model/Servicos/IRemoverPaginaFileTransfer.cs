namespace Veros.Paperless.Model.Servicos
{
    using System.Collections.Generic;
    using Entidades;

    public interface IRemoverPaginaFileTransfer
    {
        void Executar(Dictionary<Pagina, string> arquivos);
    }
}