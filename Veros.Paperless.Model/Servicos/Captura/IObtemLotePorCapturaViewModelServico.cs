namespace Veros.Paperless.Model.Servicos.Captura
{
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.ViewModel;

    public interface IObtemLotePorCapturaViewModelServico
    {
        Lote Executar(CapturaViewModel model, Pacote pacote);
    }
}
