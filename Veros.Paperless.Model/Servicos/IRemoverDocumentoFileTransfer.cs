namespace Veros.Paperless.Model.Servicos
{
    using Entidades;

    public interface IRemoverDocumentoFileTransfer
    {
        void Executar(Documento documento);
    }
}