namespace Veros.Paperless.Model.Servicos.Reconhecimento
{
    using System.Collections.Generic;
    using Entidades;

    public interface IOrdenarPaginasPacServico
    {
        void Executar(IList<Pagina> paginas);
    }
}