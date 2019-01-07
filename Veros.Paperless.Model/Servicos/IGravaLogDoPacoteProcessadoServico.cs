namespace Veros.Paperless.Model.Servicos
{
    public interface IGravaLogDoPacoteProcessadoServico
    {
        void Executar(string acao,
            int pacoteProcessadoId,
            string observacao);
    }
}