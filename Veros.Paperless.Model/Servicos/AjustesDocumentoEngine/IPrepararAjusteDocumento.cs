namespace Veros.Paperless.Model.Servicos.AjustesDocumentoEngine
{
    using Veros.Paperless.Model.Entidades;

    public interface IPrepararAjusteDocumento
    {
        void Executar(Documento documento);
    }
}