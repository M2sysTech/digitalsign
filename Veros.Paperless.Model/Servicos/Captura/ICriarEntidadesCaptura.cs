namespace Veros.Paperless.Model.Servicos.Captura
{
    using Veros.Paperless.Model.ViewModel;
    using Veros.Paperless.Model.Entidades;

    public interface ICriarEntidadesCaptura
    {
        Lote Executar(CapturaViewModel model);
    }
}
