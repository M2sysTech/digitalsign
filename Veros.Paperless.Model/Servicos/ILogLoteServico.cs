namespace Veros.Paperless.Model.Servicos
{
    public interface ILogLoteServico
    {
        /// <summary>
        /// Grava log na logbatch usando outra thread/transacao. Ex: Gravar(loteId, "DD", "Lote {0} criado", loteId);
        /// </summary>
        void Gravar(
            int loteId, 
            string acao, 
            string mensagem, 
            string token,
            params object[] args);

        void Gravar(int loteId,
            string acao,
            string mensagem,
            params object[] args);
    }
}