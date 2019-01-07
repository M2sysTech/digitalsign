namespace Veros.Paperless.Model.Servicos
{
    using Entidades;

    public interface IGravaLogDaOcorrenciaServico
    {
        void Executar(string acao, Ocorrencia ocorrencia, string observacao);

        void Executar(string acao, int ocorrenciaId, string observacao); 
    }
}