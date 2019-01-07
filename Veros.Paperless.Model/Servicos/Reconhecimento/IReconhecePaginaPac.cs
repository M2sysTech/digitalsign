namespace Veros.Paperless.Model.Servicos.Reconhecimento
{
    using System.Collections.Generic;
    using Entidades;

    public interface IReconhecePaginaPac
    {
        void Executar(int numeroPagina, IList<Pagina> paginas);
    }
}