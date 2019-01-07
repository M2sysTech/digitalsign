namespace Veros.Paperless.Model.Servicos.Exportacao
{
    public interface IEnviaPdfsDoLoteParaCloud
    {
        void Executar(int loteId);
    }
}