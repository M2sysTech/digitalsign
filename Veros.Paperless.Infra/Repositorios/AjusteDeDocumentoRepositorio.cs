namespace Veros.Paperless.Infra.Repositorios
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NHibernate.Transform;
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class AjusteDeDocumentoRepositorio : Repositorio<AjusteDeDocumento>, IAjusteDeDocumentoRepositorio
    {
        public IList<AjusteDeDocumento> ObterPorDocumento(int documentoId)
        {
            return this.Session.QueryOver<AjusteDeDocumento>()
                .Where(x => x.Documento.Id == documentoId)
                .Fetch(x => x.Documento).Eager
                .Fetch(x => x.TipoDocumentoNovo).Eager
                .List();
        }

        public IList<AjusteDeDocumento> ObterPorProcesso(int processoId)
        {
            return this.Session.QueryOver<AjusteDeDocumento>()
                .Fetch(x => x.Documento).Eager
                .Fetch(x => x.TipoDocumentoNovo).Eager
                .JoinQueryOver(x => x.Documento)
                .Where(x => x.Processo.Id == processoId)
                .List();
        }

        public void GravarComoProcessado(AjusteDeDocumento ajuste)
        {
            this.Session
              .CreateQuery("update AjusteDeDocumento set Status = :status, DataFim = :dataFim where Id = :id")
              .SetParameter("status", AjusteDeDocumento.SituacaoFechado)
              .SetParameter("dataFim", DateTime.Now)
              .SetParameter("id", ajuste.Id)
              .ExecuteUpdate();
        }

        public void GravarComoErro(AjusteDeDocumento ajuste)
        {
            this.Session
              .CreateQuery("update AjusteDeDocumento set Status = :status, DataFim = :dataFim where Id = :id")
              .SetParameter("status", AjusteDeDocumento.SituacaoErro)
              .SetParameter("dataFim", DateTime.Now)
              .SetParameter("id", ajuste.Id)
              .ExecuteUpdate();
        }

        public void AlterarStatus(int ajusteId, string situacao)
        {
            this.Session
              .CreateQuery("update AjusteDeDocumento set Status = :status, DataFim = :dataFim where Id = :id")
              .SetParameter("status", situacao)
              .SetParameter("dataFim", DateTime.Now)
              .SetParameter("id", ajusteId)
              .ExecuteUpdate();
        }

        public IList<AjusteDeDocumento> ObterPendentes(int documentoId)
        {
            return this.Session.QueryOver<AjusteDeDocumento>()
                .Where(x => x.Documento.Id == documentoId)
                .Where(x => x.Status == AjusteDeDocumento.SituacaoAberto)
                .Fetch(x => x.Documento).Eager
                .Fetch(x => x.TipoDocumentoNovo).Eager
                .List();
        }

        public IList<AjusteDeDocumento> ObterPendentesPorProcessoComPaginas(int processoId)
        {
            return this.Session.QueryOver<AjusteDeDocumento>()
                .Where(x => x.Status == AjusteDeDocumento.SituacaoAberto)
                .Fetch(x => x.Documento).Eager
                .JoinQueryOver(x => x.Documento)
                .Where(x => x.Processo.Id == processoId)
                .Fetch(y => y.Documento.Paginas.First()).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
        }

        public void GravarTodosComoProcessado(Pagina pagina)
        {
            this.Session
             .CreateQuery("update AjusteDeDocumento set Status = :status, DataFim = :dataFim where Pagina = :paginaId")
             .SetParameter("status", AjusteDeDocumento.SituacaoFechado)
             .SetParameter("dataFim", DateTime.Now)
             .SetParameter("paginaId", pagina.Id)
             .ExecuteUpdate();
        }

        public IList<AjusteDeDocumento> ObterPendentes(Lote lote)
        {
            return this.Session.QueryOver<AjusteDeDocumento>()
                .Where(x => x.Status == AjusteDeDocumento.SituacaoAberto)
                .JoinQueryOver(x => x.Documento)
                .Where(x => x.Lote.Id == lote.Id)
                .List();
        }

        public bool PossuiRegistro(Processo processo)
        {
            return this.Session.QueryOver<AjusteDeDocumento>()
                .JoinQueryOver(x => x.Documento)
                .Where(x => x.Processo == processo)
                .RowCount() > 0;
        }

        public IList<AjusteDeDocumento> ObterPendentesPorPagina(int paginaId)
        {
            return this.Session.QueryOver<AjusteDeDocumento>()
              .Where(x => x.Pagina == paginaId)
              .Where(x => x.Status == AjusteDeDocumento.SituacaoAberto)
              .Fetch(x => x.Documento).Eager
              .Fetch(x => x.TipoDocumentoNovo).Eager
              .List();
        }
    }
}