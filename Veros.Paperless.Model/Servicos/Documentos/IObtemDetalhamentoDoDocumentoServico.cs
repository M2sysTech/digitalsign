namespace Veros.Paperless.Model.Servicos.Documentos
{
    public interface IObtemDetalhamentoDoDocumentoServico
    {
        DetalhamentoDoDocumento Obter(int documentoId);
    }
}