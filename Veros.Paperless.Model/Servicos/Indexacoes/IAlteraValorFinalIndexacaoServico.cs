namespace Veros.Paperless.Model.Servicos.Indexacoes
{
    public interface IAlteraValorFinalIndexacaoServico
    {
        void Executar(int indexacaoId, string valor);
    }
}