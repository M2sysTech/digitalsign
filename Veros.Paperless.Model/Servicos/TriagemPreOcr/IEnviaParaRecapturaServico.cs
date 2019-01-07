namespace Veros.Paperless.Model.Servicos.TriagemPreOcr
{
    public interface IEnviaParaRecapturaServico
    {
        void Executar(int processoId, string observacao, string fase);
    }
}
