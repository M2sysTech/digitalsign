namespace Veros.Paperless.Model.Servicos.Separacao
{
    public interface ISalvaSeparacaoServico
    {
        void Executar(int loteId, int processoId, string textoDeAcoes);
    }
}
