namespace Veros.Paperless.Model.Servicos.IdentificacaoManual
{
    using Entidades;

    public interface IReclassificaDocumentoSeparadoServico
    {
        void Executar(int documentoId, int tipoDocumentoId);

        void Executar(Documento documento, int tipoDocumentoId);
    }
}
