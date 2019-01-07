namespace Veros.Paperless.Model.Servicos.Documentos
{
    using Entidades;

    public interface ICriaDocumentoPdfServico
    {
        Documento CriarNovoDocumentoPdf(Processo processo, Documento documentoPai, string pathArquivoPdf, int orderm = 1);

        int DefinirTipoDocumento(Documento documento, string pathArquivoPdf, int tipoPdfAnterior);
    }
}
