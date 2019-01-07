namespace Veros.Paperless.Model.Servicos.AcompanhamentoDeOcorrencia
{
    using Entidades;

    public interface IAtualizaOcorrenciaServico
    {
        void Executar(Ocorrencia ocorrencia, string observacao, string novoTipoDocumento, int grupoId);
        
        bool VerificaTipoExistente(string novoTipoDocumento);
    }
}
