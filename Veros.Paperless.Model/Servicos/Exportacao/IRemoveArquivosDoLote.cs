namespace Veros.Paperless.Model.Servicos.Exportacao
{
    public interface IRemoveArquivosDoLote
    {
        void Executar(int loteId);
    }
}