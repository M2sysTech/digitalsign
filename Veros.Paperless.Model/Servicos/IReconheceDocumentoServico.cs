namespace Veros.Paperless.Model.Servicos
{
    using Entidades;

    public interface IReconheceDocumentoServico
    {
        ImagemReconhecida Execute(Documento documento);

        ImagemReconhecida ExecutePaginaEspecifica(
            Documento documento, int pagina, dynamic tipoDocumento);
    }
}
