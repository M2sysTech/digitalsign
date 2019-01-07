namespace Veros.Paperless.Model.Servicos
{
    public interface IVerificarPdf
    {
        bool EstaAssinado(string pdfMontado);
    }
}