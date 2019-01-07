namespace Veros.Paperless.Model.Servicos.Captura
{
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.ViewModel;

    public interface IObtemProcessoPorCapturaViewModelServico
    {
        Processo Executar(CapturaViewModel model, Lote lote);
    }
}
