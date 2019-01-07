namespace Veros.Paperless.Infra.Repositorios
{
    using System.Linq;
    using NHibernate.Transform;
    using Veros.Paperless.Model;
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class PaginaRepositorio : Repositorio<Pagina>, IPaginaRepositorio
    {
        public IList<Pagina> ObterPorDocumento(Documento documento)
        {
            return this.Session.QueryOver<Pagina>()
                .Fetch(x => x.Documento).Eager
                .Where(x => x.Documento == documento)
                .List<Pagina>();
        }

        public IList<Pagina> ObterPorDocumentoId(int documentoId)
        {
            return this.Session.QueryOver<Pagina>()
            .Where(x => x.Documento.Id == documentoId)
            .Fetch(x => x.Documento).Eager
            .List<Pagina>();
        }

        public IEnumerable<Pagina> ObterPorLote(Lote lote)
        {
            return this.Session.QueryOver<Pagina>()
                .Where(x => x.Lote == lote)
                .List<Pagina>();
        }

        public void AtualizarPaginaComoNaoReconhecido(int paginaId)
        {
            if (Contexto.GargaloDeOcr)
            {
                this.Session.CreateQuery(
                    "update from Pagina p set p.Status = :status where p.Id = :id")
                    .SetParameter("status", PaginaStatus.StatusReconhecimentoExecutado)
                    .SetParameter("id", paginaId)
                    .ExecuteUpdate();
            }
            else
            {
                var sqlQuery = this.Session.CreateSQLQuery(string.Format("UPDATE DOC SET doc_fimocr = SYSDATE WHERE DOC_CODE = {0}", paginaId));
            }
        }

        public void AtualizarPaginaComoReconhecido(int paginaId)
        {
            var minhaSql = string.Empty;
            if (Contexto.GargaloDeOcr)
            {
                minhaSql = string.Format("UPDATE DOC SET doc_status = '{1}', doc_fimocr = SYSDATE WHERE DOC_CODE = {0}", paginaId, PaginaStatus.StatusReconhecimentoExecutado.Value);
            }
            else
            {
                minhaSql = string.Format("UPDATE DOC SET doc_fimocr = SYSDATE WHERE DOC_CODE = {0}", paginaId);
            }

            var sqlQuery = this.Session.CreateSQLQuery(minhaSql);
            var teste = sqlQuery.ExecuteUpdate();

            ////this.Session.CreateQuery(
            ////    "update from Pagina p set p.Status = :status, p.FimOcr = dATETIME.NOW where p.Id = :id")
            ////    .SetInt32("id", paginaId)
            ////    .SetParameter("status", PaginaStatus.StatusReconhecimentoExecutado)
            ////    .ExecuteUpdate();
        }

        public void AtualizarPaginaInicioOcr(int paginaId)
        {
            var sqlQuery = this.Session.CreateSQLQuery(
                string.Format("UPDATE DOC SET doc_tlibsoljpgdigit = SYSDATE WHERE DOC_CODE = {0}", paginaId));

            var teste = sqlQuery.ExecuteUpdate();
        }

        public void AtualizarPaginaTipoDocumento(int paginaId, string tipoArquivo)
        {
            this.Session.CreateQuery(
                "update from Pagina p set p.TipoArquivoOriginal = p.TipoArquivo, p.TipoArquivo = :tipoArquivo  where p.Id = :id")
                .SetInt32("id", paginaId)
                .SetParameter("tipoArquivo", tipoArquivo)
                .ExecuteUpdate();
        }

        public void AtualizarTudoComoReconhecido()
        {
            this.Session.CreateQuery(
                "update from Pagina p set p.Status = :status")
                 .SetParameter("status", PaginaStatus.StatusReconhecimentoExecutado)
                .ExecuteUpdate();
        }

        public void AtualizarTipoEtamanho(int paginaId, string extensaoNovaImagem, long tamanhoImagem)
        {
            this.Session.CreateQuery(
                "update from Pagina p set p.TipoArquivoOriginal = p.TipoArquivo, p.TipoArquivo = :tipoArquivo, p.TamanhoImagemFrente = :tamanhoImagem  where p.Id = :id")
                .SetInt32("id", paginaId)
                .SetParameter("tipoArquivo", extensaoNovaImagem)
                .SetParameter("tamanhoImagem", tamanhoImagem)
                .ExecuteUpdate();
        }

        public void AtualizarPaginaFaceExtractor(int paginaId, string novoStatus)
        {
            this.Session.CreateQuery(
                "update from Pagina p set p.StatusFace = :novoStatus where p.Id = :id")
                .SetInt32("id", paginaId)
                .SetParameter("novoStatus", novoStatus)
                .ExecuteUpdate();
        }

        public IList<Pagina> ObterPorStatus(PaginaStatus paginaStatus)
        {
            return this.Session.QueryOver<Pagina>()
                .Where(x => x.Status == paginaStatus)
                .List<Pagina>();
        }

        public Pagina ObterPdfDossier(int processoId)
        {
            return this.Session.QueryOver<Pagina>()
                .Inner.JoinQueryOver(x => x.Documento)
                .Where(x => x.Processo.Id == processoId)
                .And(x => x.TipoDocumento.Id == TipoDocumento.CodigoDossiePdf)
                .And(x => x.Status != DocumentoStatus.Excluido)
                .SingleOrDefault<Pagina>();
        }

        public IList<Pagina> ObterPorOrdem(int documentoId, int ordem)
        {
            return this.Session.QueryOver<Pagina>()
                .Where(x => x.Documento.Id == documentoId)
                .Where(x => x.Ordem == ordem)
                .List();
        }

        public Pagina ObterPdfDocumento(int documentoId)
        {
            return this.Session.QueryOver<Pagina>()
                .Fetch(x => x.Lote).Eager
                .Inner.JoinQueryOver(x => x.Documento)
                .Where(x => x.Id == documentoId)
                .And(x => x.Virtual)
                .And(x => x.Status != DocumentoStatus.Excluido)
                .SingleOrDefault<Pagina>();
        }

        public IList<Pagina> ObterPorProcesso(int processoId)
        {
            return this.Session.QueryOver<Pagina>()
                .OrderBy(x => x.Id)
                .Asc
                .Fetch(x => x.Documento).Eager
                .Inner.JoinQueryOver(x => x.Documento)
                .Where(x => x.Processo.Id == processoId)
                .Where(x => x.Status != DocumentoStatus.Excluido)
                .TransformUsing(Transformers.DistinctRootEntity)
                .List<Pagina>();
        }

        public void AlterarStatus(Documento documento, int ordem, PaginaStatus paginaStatus)
        {
            this.Session.CreateQuery(
                "update from Pagina set Status = :status where Documento.Id = :documentoId and Ordem = :ordem")
                .SetParameter("status", paginaStatus)
                .SetParameter("documentoId", documento.Id)
                .SetParameter("ordem", ordem)
                .ExecuteUpdate();
        }

        public void AlterarStatusPdfaProcessados(int documentoId)
        {
            this.Session.CreateQuery(
                "update from Pagina set Status = :status where Documento.Id = :documentoId and Status = :statusWhere")
                .SetParameter("status", PaginaStatus.StatusReconhecimentoExecutado)
                .SetParameter("documentoId", documentoId)
                .SetParameter("statusWhere", PaginaStatus.StatusParaReconhecimento)
                .ExecuteUpdate();
        }

        public void AlterarStatusDaPagina(int paginaId, PaginaStatus statusParaReconhecimentoPosAjuste)
        {
            this.Session.CreateQuery(
                "update from Pagina set Status = :status where Id = :paginaId")
                .SetParameter("status", statusParaReconhecimentoPosAjuste)
                .SetParameter("paginaId", paginaId)
                .ExecuteUpdate();
        }

        public IList<Pagina> ObterPaginasAjustadas(Documento documento)
        {
            return this.Session.QueryOver<Pagina>()
                .Where(x => x.Status == PaginaStatus.StatusParaReconhecimentoPosAjuste)
                .Inner.JoinQueryOver(x => x.Documento)
                .List<Pagina>();
        }

        public void AlterarStatusDaPaginaPorDocumento(int documentoId, PaginaStatus statusPagina)
        {
            this.Session.CreateQuery(
                "update from Pagina set Status = :status where Documento.Id = :documentoId")
                .SetParameter("status", statusPagina)
                .SetParameter("documentoId", documentoId)
                .ExecuteUpdate();
        }

        public IList<Pagina> ObterTodosJpegsValidos(int loteId)
        {
            return this.Session.QueryOver<Pagina>()
                .Where(x => x.TipoArquivo == "JPG")
                .And(x => x.Lote.Id == loteId)
                .Fetch(x => x.Documento).Eager
                .Inner.JoinQueryOver(x => x.Documento)
                .Where(x => x.TipoDocumento.Id != TipoDocumento.CodigoDocumentoGeral)
                .And(x => x.Virtual == false)
                .TransformUsing(Transformers.DistinctRootEntity)
                .List<Pagina>();
        }

        public void RestaurarPaginaExcluida(int paginaId, int documentoDestinoId)
        {
            this.Session.CreateQuery(
                "update Pagina set Documento.Id = :documentoId where Id = :paginaId")
                .SetParameter("paginaId", paginaId)
                .SetParameter("documentoId", documentoDestinoId)
                .ExecuteUpdate();
        }

        public void AtualizarDataCenter(int paginaId, int dataCenterId)
        {
            this.Session.CreateQuery(
                "update Pagina set DataCenter = :dataCenterId where Id = :paginaId")
                .SetParameter("paginaId", paginaId)
                .SetParameter("dataCenterId", dataCenterId)
                .ExecuteUpdate();
        }

        public IList<Pagina> ObterTipoPng(Documento documento)
        {
            return this.Session.QueryOver<Pagina>()
                .Where(x => x.Documento.Id == documento.Id)
                .And(x => x.TipoArquivo == "PNG")
                .List<Pagina>();
        }

        public Pagina ObterComDocumento(int paginaId)
        {
            return this.Session.QueryOver<Pagina>()
                .Where(x => x.Id == paginaId)
                .Fetch(x => x.Documento).Eager
                .SingleOrDefault();
        }

        public void ApagarPorLote(Lote lote)
        {
            this.Session.CreateQuery(
                "delete from Pagina where Lote.Id = :loteId")
                .SetParameter("loteId", lote.Id)
                .ExecuteUpdate();
        }

        public void AlterarOrdem(int paginaId, int novaOrdem)
        {
            this.Session.CreateQuery(
                "update from Pagina set Ordem = :novaOrdem where Id = :paginaId")
                .SetParameter("novaOrdem", novaOrdem)
                .SetParameter("paginaId", paginaId)
                .ExecuteUpdate();
        }

        public void AlterarDocumento(int paginaId, int documentoId)
        {
            this.Session.CreateQuery(
                "update from Pagina set Documento.Id = :documentoId where Id = :paginaId")
                .SetParameter("documentoId", documentoId)
                .SetParameter("paginaId", paginaId)
                .ExecuteUpdate();
        }

        public void AlterarStatus(int paginaId, PaginaStatus status)
        {
            this.Session.CreateQuery(
                "update from Pagina set Status = :status where Id = :paginaId")
                .SetParameter("paginaId", paginaId)
                .SetParameter("status", status)
                .ExecuteUpdate();
        }

        public IList<Pagina> ObterPorEstatisticaFaturamento(int faturamentoId)
        {
            Lote lote = null;
            return this.Session.QueryOver<Pagina>()
                .JoinQueryOver(x => x.Lote, () => lote)
                .Where(() => lote.PacoteProcessado.Id == faturamentoId)
                .List<Pagina>();
        }

        public IList<Pagina> ObterPorDiaProcessado(string ddmmaaaa)
        {
            ////todo: filtrar por data
            return this.Session.QueryOver<Pagina>()
                .List<Pagina>();
        }

        public void SalvarOrdemDaPagina(int paginaId, int ordemPagina)
        {
            this.Session.CreateQuery(
                "update from Pagina p set p.Ordem = :ordem where p.Id = :paginaId")
                .SetInt32("ordem", ordemPagina)
                .SetInt32("paginaId", paginaId)
                .ExecuteUpdate();
        }

        public IList<Pagina> ObterPorLoteParaRetransmissao(int loteId)
        {
            return this.Session.QueryOver<Pagina>()
                .Where(x => x.Status == PaginaStatus.StatusAguardandoRetransmissaoImagem)
                .And(x => x.Lote.Id == loteId)
                .List<Pagina>();
        }

        public Pagina ObterPorIdComDocumento(int paginaId)
        {
            return this.Session.QueryOver<Pagina>()
                .Where(x => x.Id == paginaId)
                .Fetch(x => x.Documento).Eager
                .Fetch(x => x.Documento.TipoDocumento).Eager
                .SingleOrDefault<Pagina>(); 
        }

        public IList<Pagina> ObterPaginaNaoExcluida(int documentoId)
        {
            return this.Session.QueryOver<Pagina>()
                .Where(x => x.Status != PaginaStatus.StatusExcluida)
                .Where(x => x.Documento.Id == documentoId)
                .List<Pagina>(); 
        }

        public IList<Pagina> ObterPdfsDoLote(int loteId)
        {
            return this.Session.QueryOver<Pagina>()
                .Where(x => x.Lote.Id == loteId)
                .And(x => x.TipoArquivo == "PDF")
                .List<Pagina>();
        }

        public void MarcarComoEnviadaCloud(int paginaId)
        {
            this.Session.CreateQuery(
                "update from Pagina p set CloudOk = true where p.Id = :paginaId")
                .SetInt32("paginaId", paginaId)
                .ExecuteUpdate();
        }

        public IList<Pagina> ObterJpegsDoLote(int loteId)
        {
            return this.Session.QueryOver<Pagina>()
                .Where(x => x.Lote.Id == loteId)
                .And(x => x.TipoArquivo == "JPG")
                .List<Pagina>();
        }

        public void MarcarComoRemovidoFileTransferM2(int paginaId)
        {
            this.Session.CreateQuery(
                "update from Pagina p set RemovidoFileTransferM2 = true where p.Id = :paginaId")
                .SetInt32("paginaId", paginaId)
                .ExecuteUpdate();
        }

        public void AtualizarDataCenterAntesCloud(int paginaId, int dataCenter)
        {
            this.Session.CreateQuery(
                "update Pagina set DataCenterAntesCloud = :dataCenterId where Id = :paginaId")
                .SetParameter("paginaId", paginaId)
                .SetParameter("dataCenterId", dataCenter)
                .ExecuteUpdate();
        }

        public void AlterarStatusDaPaginaNaoExcluidaPorLote(int loteId, PaginaStatus status)
        {
            this.Session
              .CreateQuery("update Pagina set Status = :status where Lote.Id = :lote And Status != :statusExcluido")
              .SetParameter("status", status)
              .SetParameter("lote", loteId)
              .SetParameter("statusExcluido", PaginaStatus.StatusExcluida)
              .ExecuteUpdate();
        }

        public void ExcluirPaginasPorLote(int loteId)
        {
            this.Session
              .CreateQuery("delete from Pagina where Documento.Id in (select Id from Documento where Virtual = true and Lote.Id = :lote)")
              .SetParameter("lote", loteId)
              .ExecuteUpdate();      
        }
    }
}