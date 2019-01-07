namespace Veros.Paperless.Model.Servicos.Importacao
{
    public interface IConverterImagem
    {
        string ParaJpeg(string caminhoArquivo, string tipoArquivo);
    }
}