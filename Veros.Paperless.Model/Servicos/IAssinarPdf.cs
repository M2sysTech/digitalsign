namespace Veros.Paperless.Model.Servicos
{
    public interface IAssinarPdf
    {
        string Execute(string pdfMontado);
    }
}