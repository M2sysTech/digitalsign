namespace Veros.Paperless.Model.Servicos
{
    using System;
    using System.Threading.Tasks;
    using Veros.Data;

    public class LogLoteServico : ILogLoteServico
    {
        private readonly GravaLogDoLoteServico gravaLogDoLoteServico;
        private readonly IUnitOfWork unitOfWork;

        public LogLoteServico(GravaLogDoLoteServico gravaLogDoLoteServico, IUnitOfWork unitOfWork)
        {
            this.gravaLogDoLoteServico = gravaLogDoLoteServico;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Grava log na logbatch usando outra thread/transacao. Ex: Gravar(loteId, "DD", "Lote {0} criado", loteId);
        /// </summary>
        public void Gravar(
            int loteId, 
            string acao, 
            string mensagem, 
            string token,
            params object[] args)
        {
            Task.Factory.StartNew(() =>
            {
                using (this.unitOfWork.Begin())
                {
                    this.gravaLogDoLoteServico.Executar(acao, loteId, string.Format(mensagem, args), token);
                }
            }).Wait(TimeSpan.FromSeconds(10));
        }

        public void Gravar(
            int loteId,
            string acao,
            string mensagem,
            params object[] args)
        {
            Task.Factory.StartNew(() =>
            {
                using (this.unitOfWork.Begin())
                {
                    this.gravaLogDoLoteServico.Executar(acao, loteId, string.Format(mensagem, args));
                }
            }).Wait(TimeSpan.FromSeconds(10));
        }
    }
}
