namespace Veros.Paperless.Model.Servicos.PreparaAjuste
{
    using Entidades;

    public interface IExisteDocumentoPngAtivoServico
    {
        bool Existe(Documento documento);
    }
}
