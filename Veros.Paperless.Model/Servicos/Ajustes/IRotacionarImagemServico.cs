namespace Veros.Paperless.Model.Servicos.Ajustes
{
    using System.Drawing.Imaging;

    public interface IRotacionarImagemServico
    {
        string Executar(string arquivoOrigem, int angulo, ImageFormat formatoSaida = null);
    }
}