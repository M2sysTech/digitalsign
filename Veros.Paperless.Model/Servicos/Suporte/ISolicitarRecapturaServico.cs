namespace Veros.Paperless.Model.Servicos.Suporte
{
    public interface ISolicitarRecapturaServico
    {
        void Executar(int loteId, string motivo);
    }
}
