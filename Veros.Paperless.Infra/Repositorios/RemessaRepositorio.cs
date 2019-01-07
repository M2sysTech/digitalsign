namespace Veros.Paperless.Infra.Repositorios
{
    using System;
    using Framework;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class RemessaRepositorio : Repositorio<Remessa>, IRemessaRepositorio
    {
        public void AlterarStatusPorProcesso(int processoId, RemessaStatus status)
        {
            Log.Application.InfoFormat("Não é pra ser feito");

            ////this.Session
            ////    .CreateQuery("update Remessa set Status = :status where Processo.Id = :processoId")
            ////    .SetParameter("processoId", processoId)
            ////    .SetParameter("status", status)
            ////    .ExecuteUpdate();
        }

        public void FinalizaRemessaAposRetorno(string nomeArquivo, string extensao)
        {
            this.Session
                .CreateQuery("update Remessa set Status = :status, DataHoraGeracao = :dataHoraGeracao, Extensao = : extensao where Arquivo = :nomeArquivo and Status not in(:statusFinalizado)")
                .SetParameter("status", RemessaStatus.StatusFinalizada)
                .SetParameter("dataHoraGeracao", DateTime.Now)
                .SetParameter("extensao", extensao)
                .SetParameter("nomeArquivo", nomeArquivo)
                .SetParameter("statusFinalizado", RemessaStatus.StatusFinalizada)
                .ExecuteUpdate(); 
        }

        public void FinalizaRemessaAposExport(int processoId)
        {
            this.Session
                .CreateQuery("update Remessa set Status = :status where Processo.Id = :processoId AND Status = :statusDoneExport ")
                .SetParameter("processoId", processoId)
                .SetParameter("status", RemessaStatus.StatusFinalizada)
                .SetParameter("statusDoneExport", RemessaStatus.ExportFinalizado)
                .ExecuteUpdate();
        }
    }
}
