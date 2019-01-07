namespace Veros.Paperless.Model.Servicos.Reconhecimento
{
    public interface IRecortaCabecalhoRodapeDocumentoPacServico
    {
        string[] Executar(int documentoId, string imagemPaginaPac);
    }
}