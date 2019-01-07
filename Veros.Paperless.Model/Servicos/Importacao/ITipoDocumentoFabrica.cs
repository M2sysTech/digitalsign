namespace Veros.Paperless.Model.Servicos.Importacao
{
    using Entidades;

    public interface ITipoDocumentoFabrica
    {
        TipoDocumento Criar(CodigoTipoDocumentoDominio codigoTipoDocumento);
    }
}