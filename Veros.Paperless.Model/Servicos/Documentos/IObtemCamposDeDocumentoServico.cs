namespace Veros.Paperless.Model.Servicos.Documentos
{
    public interface IObtemCamposDeDocumentoServico
    {
        CamposDeDocumento Obter(int documentoId);

        CamposDeDocumento ObterPorProcesso(int processoId);
    }
}