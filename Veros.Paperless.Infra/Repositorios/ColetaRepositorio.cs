namespace Veros.Paperless.Infra.Repositorios
{
    using System;
    using System.Collections.Generic;
    using NHibernate.Transform;
    using NHibernate.Util;
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class ColetaRepositorio : Repositorio<Coleta>, IColetaRepositorio
    {
        public IList<Coleta> ObterPorUsuarioRegistro(int usuarioId)
        {
            return this.Session.QueryOver<Coleta>()
                .Where(x => x.UsuarioCadastro.Id == usuarioId)
                .List();
        }

        public IList<Coleta> ObterPendentes()
        {
            return this.Session.QueryOver<Coleta>()
                .Where(x => x.Status == ColetaStatus.Agendado 
                    || x.Status == ColetaStatus.EtiquetasGeradas)
                .List();
        }

        public IList<Coleta> ObterParaImportar()
        {
            return this.Session.QueryOver<Coleta>()
                .Where(x => x.Status == ColetaStatus.ImportacaoDeArquivo)
                .List();
        }

        public Coleta ObterComPacotesPorId(int id)
        {
            return this.Session.QueryOver<Coleta>()
                .Fetch(x => x.Pacotes).Eager
                .Where(x => x.Status == ColetaStatus.Agendado 
                    || x.Status == ColetaStatus.EtiquetasGeradas
                    || x.Status == ColetaStatus.ImportacaoDeArquivo
                    || x.Status == ColetaStatus.Coleta
                    || x.Status == ColetaStatus.Recebido)
                .Where(x => x.Id == id)
                .TransformUsing(Transformers.DistinctRootEntity)
                .SingleOrDefault();
        }

        public IList<Coleta> ObterAgendadaComTransportadora()
        {
            return this.Session.QueryOver<Coleta>()
                .Fetch(x => x.Transportadora).Eager
                .Where(x => x.Status == ColetaStatus.Agendado
                    || x.Status == ColetaStatus.ImportacaoDeArquivo
                    || x.Status == ColetaStatus.Coleta
                    || x.Status == ColetaStatus.ErroNaImportacao)
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
        }

        public void AtualizarArquivo(int coletaId, string arquivo)
        {
            this.Session
              .CreateQuery("update Coleta set Arquivo = :arquivo, Status = :status where Id = :id")
              .SetParameter("arquivo", arquivo)
              .SetParameter("status", ColetaStatus.ImportacaoDeArquivo)
              .SetParameter("id", coletaId)
              .ExecuteUpdate();
        }

        public void AtualizarStatus(int coletaId, ColetaStatus status)
        {
            this.Session
              .CreateQuery("update Coleta set Status = :status where Id = :id")
              .SetParameter("status", status)
              .SetParameter("id", coletaId)
              .ExecuteUpdate();
        }        

        ////        public IList<Coleta> ObterPorPeriodo(DateTime dataInicio, DateTime dataFim)
        ////        {
        ////            return this.Session.QueryOver<Coleta>()                
        ////                .JoinQueryOver(x => x.Pacotes)
        ////                .Inner.JoinQueryOver<Lote>(x => x.Pacotes)
        ////                .JoinQueryOver(x => x.)
        ////                .Where(x => x. >= dataInicio &&
        ////                    x.ArquivoImportadoEm <= dataFim)
        ////                .List<Processo>();
        ////            SELECT C.COLETA_DATA, C.COLETA_CODE, PR.PROC_CODE, TP.TYPEPROC_DESC, 
        ////PR.PROC_IDENTIFICACAO, C.COLETA_DTDEVOLUCAO
        ////FROM COLETA C 
        ////INNER JOIN PACK P ON C.COLETA_CODE = P.COLETA_CODE
        ////INNER JOIN BATCH B ON P.PACK_CODE = B.PACK_CODE
        ////INNER JOIN PROC PR ON B.BATCH_CODE = PR.BATCH_CODE
        ////INNER JOIN TYPEPROC TP ON PR.TYPEPROC_CODE = TP.TYPEPROC_CODE
        ////        }
    }
}
