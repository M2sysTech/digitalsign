namespace Veros.Paperless.Model.Servicos.TriagemPreOcr
{
    public interface ISalvaTriagemPreOcrServico
    {
        void Executar(int processoId);

        void Executar(int loteId, int processoId, string textoDeAcoes);

        void ExecutarExclusoes(int loteId, int processoId, string textoDeAcoes);
    }
}
