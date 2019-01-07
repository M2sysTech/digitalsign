namespace Veros.Paperless.Model.Servicos.Captura
{
    using Veros.Paperless.Model.Entidades;

    public interface IObtemPacotePorBarcodeServico
    {
        Pacote Executar(string barcodeCaixa);
    }
}
