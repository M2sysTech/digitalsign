namespace Veros.Paperless.Model.Servicos.Ajustes
{
    using Entidades;

    public interface IAplicarAjusteDeExclusaoDePagina
    {
        void Executar(Pagina pagina);
    }
}