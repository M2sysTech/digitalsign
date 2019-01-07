namespace Veros.Paperless.Infra.Repositorios
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Model.Consultas;
    using NHibernate.Criterion;
    using NHibernate.Transform;
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class DocumentoRepositorio : Repositorio<Documento>, IDocumentoRepositorio
    {
        /// <summary>
        /// TODO: escrever teste
        /// </summary>
        public IList<Documento> ObterTodosPorLoteComPaginasEIndexacao(int loteId)
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.Lote.Id == loteId)
                .Fetch(x => x.Paginas).Eager
                .Fetch(x => x.Indexacao).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
        }

        public Documento ObterPacDoLote(Lote lote)
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.Lote == lote)
                .JoinQueryOver(x => x.TipoDocumento)
                .Where(x => x.Id == TipoDocumento.CodigoFichaDeCadastro)
                .SingleOrDefault<Documento>();
        }

        public Documento ObterPacDoProcesso(Processo processo)
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.Processo == processo)
                .JoinQueryOver(x => x.TipoDocumento)
                .Where(x => x.Id == TipoDocumento.CodigoFichaDeCadastro)
                .SingleOrDefault<Documento>();
        }

        public void AtualizaStatusDocumento(int documentoId, DocumentoStatus status)
        {
            this.Session
              .CreateQuery("update Documento set Status = :status where Id = :id")
              .SetParameter("id", documentoId)
              .SetParameter("status", status)
              .ExecuteUpdate();
        }

        public IList<Documento> ObterTodosPorProcesso(Processo processo)
        {
            return this.Session.QueryOver<Documento>()
                .Fetch(x => x.TipoDocumento).Eager
                .Where(x => x.Processo == processo)
                .OrderBy(x => x.Ordem).Asc
                .List();
        }

        public IList<Documento> ObterDocumentosPendentesDeMontagem()
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.Status == DocumentoStatus.AguardandoMontagem)
                .List();
        }

        public void AlterarIndicioDeFraude(int documentoId, string indicioDeFraude)
        {
            this.Session
              .CreateQuery("update Documento set IndicioDeFraude = :indicioDeFraude where Id = :id")
              .SetString("indicioDeFraude", indicioDeFraude)
              .SetInt32("id", documentoId)
              .ExecuteUpdate();
        }

        public IList<Documento> ObterDocumentosPorParticipante(int processoId, string cpf, string sequencial)
        {
            string query = @"
select 
    indexacao.Documento 
from 
    Indexacao indexacao
inner join indexacao.Documento documento
inner join documento.Processo processo
inner join indexacao.Campo campo
where 
    processo.Id = :processoId and
    indexacao.SegundoValor = :cpf and
    campo.ReferenciaArquivo = :referencia";

            return this.Session.CreateQuery(query)
                .SetParameter("processoId", processoId)
                .SetParameter("cpf", cpf)
                .SetParameter("referencia", Campo.ReferenciaDeArquivoCpf)
                .List<Documento>();

            ////// TODO: Verificar o migrate - está criando a coluna VALOR2 como CLOB isso está impedindo o uso da query
            ////////Indexacao indexacao = null;

            ////var documentos = this.Session.QueryOver<Documento>()
            ////    .Where(x => x.Processo.Id == processoId)
            ////    //.JoinQueryOver(x => x.Indexacao, () => indexacao)
            ////    //.Where(() => indexacao.SegundoValor == cpfParticipante)
            ////    //.JoinQueryOver(() => indexacao.Campo)
            ////    //.Where(c => c.ReferenciaArquivo == Campo.ReferenciaDeArquivoCpf)
            ////    .List();

            ////// TODO: Remover esse código quando conseguir resolver a query
            ////return documentos.Where(documento => documento.Indexacao.Any(x => x.SegundoValor == cpfParticipante) 
            ////    && documento.Indexacao.FirstOrDefault(x => x.SegundoValor == cpfParticipante).Campo.ReferenciaArquivo == Campo.ReferenciaDeArquivoCpf)
            ////    .ToList();
        }

        public IList<Documento> ObterPendentesDeConsulta()
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.StatusDeConsulta == DocumentoStatus.StatusDeConsultaPendente)
                .And(x => x.Status == DocumentoStatus.ParaDigitacao)
                .Fetch(x => x.Indexacao).Eager
                .Fetch(x => x.Indexacao.First().Campo).Eager
                .List();
        }

        public void AlterarStatusDeConsulta(int documentoId, DocumentoStatus statusDeConsulta)
        {
            this.Session
              .CreateQuery("update Documento set StatusDeConsulta = :statusDeConsulta where Id = :id")
              .SetParameter("statusDeConsulta", statusDeConsulta)
              .SetParameter("id", documentoId)
              .ExecuteUpdate();
        }

        public void AlterarStatusPorLote(int loteId, DocumentoStatus status)
        {
            this.Session
              .CreateQuery("update Documento set Status = :status where Lote.Id = :lote")
              .SetParameter("status", status)
              .SetParameter("lote", loteId)
              .ExecuteUpdate();
        }        

        public bool TemNaoIdentificadosPorLote(Lote lote)
        {
            const string Hql = @"
select 
    count(Id)  
from 
    Documento 
where 
    Lote.Id = :loteId and 
    TipoDocumento.Id = :tipoDocumentoId";

            return this.Session.CreateQuery(Hql)
                .SetParameter("loteId", lote.Id)
                .SetParameter("tipoDocumentoId", TipoDocumento.CodigoNaoIdentificado)
                .UniqueResult<long>() > 0;
        }

        public IList<TotalDocumentoPorFaseConsulta> ObterTotalDeDocumentosPorFase()
        {
            var statusConsulta = new List<DocumentoStatus>
            {
                DocumentoStatus.StatusValidado,
                DocumentoStatus.StatusParaReconhecimento,
                DocumentoStatus.StatusParaProvaZero,
                DocumentoStatus.AguardandoMontagem,
                DocumentoStatus.AguardandoAprovacao,
                DocumentoStatus.ParaDigitacao,
                DocumentoStatus.ParaValidacao,
                DocumentoStatus.StatusParaExportacao,
                DocumentoStatus.StatusFinalizado
            };

            return this.Session.CreateCriteria(typeof(Documento))
                .Add(Restrictions.In("Status", statusConsulta))
                .SetProjection(Projections.ProjectionList()
                                    .Add(Projections.Alias(Projections.GroupProperty("Status"), "Status"))
                                    .Add(Projections.Alias(Projections.Count("Id"), "QuantidadeDocumentos")))
                .SetResultTransformer(Transformers.AliasToBean(typeof(TotalDocumentoPorFaseConsulta)))
                .List<TotalDocumentoPorFaseConsulta>();
        }

        public void ConcluirMontagemDocumento(int documentoId, string templates)
        {
            this.Session
              .CreateQuery("update Documento set Templates = :templates, Status = :status where Id = :id and Status = :statusAtual")
              .SetParameter("id", documentoId)
              .SetParameter("templates", templates)
              .SetParameter("status", DocumentoStatus.StatusMontagemConcluida)
              .SetParameter("statusAtual", DocumentoStatus.AguardandoMontagem)
              .ExecuteUpdate();
        }

        public void LimparHoraInicio(int documentoId)
        {
            this.Session
              .CreateQuery("update Documento set HoraInicio = null where Id = :id")
              .SetInt32("id", documentoId)
              .ExecuteUpdate();
        }

        public void LimparHoraInicioEResponsavel(int documentoId)
        {
            this.Session
              .CreateQuery("update Documento set HoraInicio = null, UsuarioResponsavelId =  null where Id = :id")
              .SetInt32("id", documentoId)
              .ExecuteUpdate();
        }
        
        public void Reclassificar(int documentoId, int tipoDocumentoId, DocumentoStatus status)
        {
            this.Session
                .CreateQuery(@"
update Documento set HoraInicio = null, 
Reclassificado = :reclassificado,
TipoDocumento.Id = :tipoDocumentoId,
StatusDeConsulta = :statusDeConsulta 
where Id = :id")
                .SetParameter("reclassificado", true) 
                .SetParameter("tipoDocumentoId", tipoDocumentoId)
                .SetParameter("statusDeConsulta", status)
                .SetParameter("id", documentoId)
                .ExecuteUpdate();
        }

        public IList<Documento> ObterDocumentosDoLotePorTipo(int loteId, int tipoDocumentoId)
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.TipoDocumento.Id == tipoDocumentoId)
                .And(x => x.Lote.Id == loteId)
                .Fetch(x => x.Lote).Eager
                .List();
        }

        public IList<Documento> ObterDocumentosDoProcessoComCpf(int processoId, string cpf)
        {
            const string Hql = @"
select
    documento
from 
    Documento documento
inner join 
    documento.Indexacao as indexacao
inner join
    indexacao.Campo as campo    
where 
    campo.ReferenciaArquivo = 'CPFBAS'
    and documento.Processo.Id = :processoId    
    and indexacao.SegundoValor = : cpf";
            /*
             SELECT TD.* 
FROM MDOC M
INNER JOIN MDOCDADOS MD ON M.MDOC_CODE = MD.MDOC_CODE
INNER JOIN TDCAMPOS TC ON MD.TDCAMPOS_CODE = TC.TDCAMPOS_CODE
INNER JOIN TYPEDOC TD ON M.TYPEDOC_ID = TD.TYPEDOC_ID
WHERE TC.TDCAMPOS_REFARQUIVO = 'CPFBAS'
AND MD.MDOCDADOS_VALOR2 = '12297120702'
AND M.PROC_CODE = " & mvarProcCode
             */
            return this.Session.CreateQuery(Hql)
                .SetParameter("processoId", processoId)
                .SetParameter("cpf", cpf)
                .SetResultTransformer(Transformers.DistinctRootEntity)
                .List<Documento>();
        }

        public IList<Documento> ObterPorProcessoEParticipante(int processoId, string cpfDoParticipante, string sequencialDoParticipante)
        {
            const string Hql = @"
select
    documento
from 
    Documento documento
inner join 
    documento.Indexacao as indexacaoCpf
inner join
    indexacaoCpf.Campo as campoCpf    

inner join 
    documento.Indexacao as indexacaoSeq
inner join
    indexacaoSeq.Campo as campoSeq
where 
    documento.Processo.Id = :processoId    
    and campoCpf.ReferenciaArquivo = 'CPFBAS'    
    and indexacaoCpf.SegundoValor = : cpf
    and campoSeq.ReferenciaArquivo = 'SEQTIT'    
    and indexacaoSeq.SegundoValor = : seq";

            return this.Session.CreateQuery(Hql)
                .SetParameter("processoId", processoId)
                .SetParameter("cpf", cpfDoParticipante)
                .SetParameter("seq", sequencialDoParticipante)
                .SetResultTransformer(Transformers.DistinctRootEntity)
                .List<Documento>();
        }

        public void AlterarTipo(int documentoId, int tipoDocumentoId)
        {
            this.Session
    .CreateQuery(@"
update Documento set 
TipoDocumento.Id = :tipoDocumentoId 
where Id = :id")
    .SetParameter("tipoDocumentoId", tipoDocumentoId)
    .SetParameter("id", documentoId)
    .ExecuteUpdate();
        }

        public void GravarComoFichaVirtual(int documentoId)
        {
            this.Session
    .CreateQuery(@"
update Documento set 
Virtual = :virtual 
where Id = :id")
    .SetParameter("virtual", true)
    .SetParameter("id", documentoId)
    .ExecuteUpdate();
        }

        public IList<Documento> ObterComPaginasPendentesClassifier()
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.Status == DocumentoStatus.AguardandoClassifier)
                .Fetch(x => x.Paginas).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
        }

        public IList<Documento> ObterTodosPorLote(int loteId)
        {
            return this.Session.QueryOver<Documento>()
            .Where(x => x.Lote.Id == loteId)
            .Fetch(x => x.Lote).Eager
            .Fetch(x => x.Paginas).Eager
            .TransformUsing(Transformers.DistinctRootEntity)
            .List();
        }

        public void AlterarStatus(int documentoId, DocumentoStatus documentoStatus)
        {
            this.Session
              .CreateQuery("update Documento set Status = :documentoStatus where Id = :id")
              .SetParameter("documentoStatus", documentoStatus)
              .SetParameter("id", documentoId)
              .ExecuteUpdate();
        }

        public IList<int> ObterListaDePendentesDeAntiFraude()
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.TipoDocumento.Id == TipoDocumento.CodigoRg || x.TipoDocumento.Id == TipoDocumento.CodigoCnh)
                .Where(x => x.Status != DocumentoStatus.Excluido && x.Status != DocumentoStatus.Erro)
                .Where(x => x.StatusDeFraude == null)
                .Fetch(x => x.Paginas).Eager
                .Fetch(x => x.Indexacao).Eager
                .Select(x => x.Id)
                .Take(100)
                .List<int>();
        }

        public IList<int> ObterListaDePendentesDeAjuste()
        {
            return this.Session.QueryOver<AjusteDeDocumento>()
                .Where(x => x.Status == AjusteDeDocumento.SituacaoAberto)
                .Select(x => x.Id)
                .List<int>();
        }

        public Documento ObterParaAntiFraude(int documentoId)
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.Id == documentoId)
                .Fetch(x => x.TipoDocumento).Eager
                .Fetch(x => x.Paginas).Eager
                .Fetch(x => x.Indexacao).Eager
                .SingleOrDefault();
        }

        public void AlterarStatusFraude(int documentoId, string status)
        {
            this.Session
              .CreateQuery("update Documento set StatusDeFraude = :status where Id = :id")
              .SetParameter("status", status)
              .SetParameter("id", documentoId)
              .ExecuteUpdate();
        }

        public IList<Documento> ObterPorProcessoETipo(int processoId, int tipoDoc)
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.Processo.Id == processoId)
                .Where(x => x.TipoDocumento.Id == tipoDoc)
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
        }

        public Documento ObterComPaginas(int documentoId)
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.Id == documentoId)
                .Fetch(x => x.TipoDocumento).Eager
                .Fetch(x => x.Paginas).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .SingleOrDefault();
        }

        public IList<Documento> ObterComPaginas(Lote lote)
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.Lote == lote)
                .Fetch(x => x.TipoDocumento).Eager
                .Fetch(x => x.Paginas).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
        }

        public void AlterarTemplate(Lote lote, int tipoDocumentoId, string template)
        {
            this.Session
              .CreateQuery("update Documento set Templates = :template where Lote.Id = :loteId and TipoDocumento.Id = :tipoDocumentoId")
              .SetParameter("template", template)
              .SetParameter("loteId", lote.Id)
              .SetParameter("tipoDocumentoId", tipoDocumentoId)
              .ExecuteUpdate();
        }

        public IList<Documento> ObterParaClassificacao(int usuarioResponsavelId)
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.TipoDocumento.Id == TipoDocumento.CodigoNaoIdentificado)
                .Where(x => x.UsuarioResponsavelId == usuarioResponsavelId)
                .And(x => x.Status != DocumentoStatus.Excluido)
                .Fetch(x => x.Processo).Eager
                .Fetch(x => x.Lote).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .OrderBy(x => x.Id).Asc
                .List();
        }

        public IList<Documento> ObterTodosPorLoteComTipoDocumento(int loteId)
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.Lote.Id == loteId)
                .Fetch(x => x.Lote).Eager
                .Fetch(x => x.TipoDocumento).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
        }

        public Documento ObterComPacote(int documentoId)
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.Id == documentoId)
                .Fetch(x => x.Lote).Eager
                .Fetch(x => x.Lote.Pacote).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .List()
                .FirstOrDefault();
        }

        public Documento ObterComPacoteProcessado(int documentoId)
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.Id == documentoId)
                .Fetch(x => x.Lote).Eager
                .Fetch(x => x.Lote.PacoteProcessado).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .List()
                .FirstOrDefault();
        }

        public IList<Documento> ObterComTipoPorLote(int loteId)
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.Lote.Id == loteId)
                .Fetch(x => x.TipoDocumento).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .List<Documento>();
        }

        public bool TodasOsPdfsEstaoAssinados(int loteId)
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.Virtual)
                .And(x => x.Status != DocumentoStatus.Assinado)
                .And(x => x.Status != DocumentoStatus.Excluido)
                .And(x => x.Lote.Id == loteId)
                .RowCount() == 0;
        }

        public Documento ObterDocumentoPaiComPaginas(int documentoId)
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.DocumentoPaiId == documentoId)
                .And(x => x.Status != DocumentoStatus.Excluido)
                .Fetch(x => x.TipoDocumento).Eager
                .Fetch(x => x.Paginas).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .SingleOrDefault();
        }

        public IList<Documento> ObterPendentesDeRecognitionServer()
        {
            const string Hql = @"
select
    documento
from 
    Documento documento
inner join 
    documento.Paginas as paginas
inner join 
    documento.Lote as lote
where 
    documento.Status = '35'
    and paginas.Status = '55'
    and lote.Status = '55'
order by lote.ResultadoQualidadeCef, documento.Id";

            return this.Session.CreateQuery(Hql)
                .SetResultTransformer(Transformers.DistinctRootEntity)
                .SetMaxResults(100)
                .List<Documento>();
        }

        public IList<Documento> ObterProcessadosPeloRecognitionServer()
        {
            const string Hql = @"
select
    documento
from 
    Documento documento
inner join 
    documento.Paginas as paginas
inner join 
    documento.Lote as lote
where 
    documento.Status = '55'
    and paginas.Status = '55'
    and lote.Status = '55'
    and (documento.RecognitionService = false or documento.RecognitionService = null)
order by lote.ResultadoQualidadeCef, documento.Id";

            return this.Session.CreateQuery(Hql)
                .SetResultTransformer(Transformers.DistinctRootEntity)
                .List<Documento>();
        }

        public void AtualizarResponsavel(int documentoId, int usuarioId)
        {
            this.Session
              .CreateQuery("update Documento set UsuarioResponsavelId = :usuarioId, HoraInicio = :horaInicio where Id = :documentoId")
              .SetParameter("usuarioId", usuarioId)
              .SetParameter("horaInicio", DateTime.Now)
              .SetParameter("documentoId", documentoId)
              .ExecuteUpdate();
        }

        public int ObterQuantidadePorLote(int loteId)
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.Lote.Id == loteId)
                .RowCount();
        }

        public IList<Documento> ObterPreparacaoParaAjustePorLote(int loteId)
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.Lote.Id == loteId)
                .And(x => x.IndicioDeFraude == Documento.MarcaDeProblema)
                .Fetch(x => x.Paginas).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .List<Documento>();
        }

        public void AtualizarStatusPorProcesso(int processoId, DocumentoStatus statusAtual, DocumentoStatus novoStatus)
        {
            this.Session
              .CreateQuery(@"
update Documento set 
    Status = :novoStatus
where Processo.Id = :processoId
and Status = :statusAtual")
              .SetParameter("novoStatus", novoStatus)
              .SetParameter("statusAtual", statusAtual)
              .SetParameter("processoId", processoId)
              .ExecuteUpdate();
        }

        public IList<int> ObterListaIdsEmPreparacaoParaAjuste()
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.IndicioDeFraude == Documento.MarcaDeProblema)
                .And(x => x.Status == DocumentoStatus.AjustePreparacao)
                .And(x => x.Virtual)
                .Select(x => x.Id)
                .List<int>();
        }

        public IList<Documento> ObterDocumentosEmPreparacaoParaAjuste(int documentoId)
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.Id == documentoId)
                .And(x => x.Status == DocumentoStatus.AjustePreparacao)
                .Fetch(x => x.Paginas).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .List<Documento>();
        }

        public IList<Documento> ObterPendentesDeRecognitionServerPosAjuste()
        {
            const string Hql = @"
select
    documento
from 
    Documento documento
inner join 
    documento.Lote as lote
where 
    lote.Status = 'J6'
    and documento.Status = :documentoStatus
order by lote.ResultadoQualidadeCef, documento.Id";

            return this.Session.CreateQuery(Hql)
                .SetParameter("documentoStatus", DocumentoStatus.StatusParaReconhecimentoPosAjuste)
                .SetResultTransformer(Transformers.DistinctRootEntity)
                .List<Documento>();
        }

        public IList<Documento> ObterProcessadosPeloRecognitionServerPosAjuste()
        {
            const string Hql = @"
select
    documento
from 
    Documento documento
inner join 
    documento.Paginas as paginas
where 
    documento.Status = :documentoStatus
    and (documento.RecognitionPosAjusteServiceFinalizado = false or documento.RecognitionPosAjusteServiceFinalizado = null)";

            return this.Session.CreateQuery(Hql)
                .SetParameter("documentoStatus", DocumentoStatus.StatusParaAguardandoReconhecimentoPosAjuste)
                .SetResultTransformer(Transformers.DistinctRootEntity)
                .List<Documento>();
        }

        public void AlterarTipoOriginal(Documento documento, TipoDocumento tipoDocumentoNovo)
        {
            this.Session
                .CreateQuery(@"update Documento set TipoDocumentoOriginal.Id = :tipoDocumentoId where Id = :id")
                .SetParameter("tipoDocumentoId", tipoDocumentoNovo.Id)
                .SetParameter("id", documento.Id)
                .ExecuteUpdate();
        }

        public Documento ObterFolhaDeRosto(int processoId)
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.TipoDocumento.Id == TipoDocumento.CodigoFolhaDeRosto)
                .And(x => x.Processo.Id == processoId)
                .SingleOrDefault();
        }

        public Documento ObterTermoAutuacao(int processoId)
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.TipoDocumento.Id == TipoDocumento.CodigoTermoAutuacaoDossie)
                .And(x => x.Processo.Id == processoId)
                .SingleOrDefault();
        }

        public IList<Documento> ObterPdfsPorProcesso(int processoId)
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.TipoDocumento.Id != TipoDocumento.CodigoFolhaDeRosto && x.TipoDocumento.Id != TipoDocumento.CodigoTermoAutuacaoDossie)
                .And(x => x.Virtual)
                .And(x => x.Status != DocumentoStatus.Excluido)
                .And(x => x.Processo.Id == processoId)
                .List();
        }

        public void AtualizarQuantidadeDePaginas(int id, int totalPaginasDoDocumento)
        {
            this.Session
                .CreateQuery("update Documento set QuantidadeDePaginas = :qtde where Id = :id")
                .SetParameter("qtde", totalPaginasDoDocumento)
                .SetParameter("id", id)
                .ExecuteUpdate();
        }

        public IList<Documento> ObterTermos(int processoId)
        {
            return this.Session.QueryOver<Documento>()
               .Where(x => x.TipoDocumento.Id == TipoDocumento.CodigoFolhaDeRosto || x.TipoDocumento.Id == TipoDocumento.CodigoTermoAutuacaoDossie)
               .And(x => x.Virtual)
               .And(x => x.Processo.Id == processoId)
               .List();
        }

        public void AlterarOrdem(int documentoId, int novaOrdem)
        {
            this.Session
                .CreateQuery(@"update Documento set Ordem = :novaOrdem where Id = :documentoId")
                .SetParameter("novaOrdem", novaOrdem)
                .SetParameter("documentoId", documentoId)
                .ExecuteUpdate();
        }

        public void AlterarMarca(int documentoId, string marca)
        {
            this.Session
                .CreateQuery(@"update Documento set Marca = :marca where Id = :documentoId")
                .SetParameter("marca", marca)
                .SetParameter("documentoId", documentoId)
                .ExecuteUpdate();
        }

        public void MarcarDocumentoRecontado(Documento documento, int totalPaginas)
        {
            this.Session
                .CreateQuery("update Documento set QuantidadeDePaginas = :qtde, Recontado = true where Id = :id")
                .SetParameter("qtde", totalPaginas)
                .SetParameter("id", documento.Id)
                .ExecuteUpdate();
        }

        public Documento ObterDocumentacaoGeral(int loteId)
        {
            return this.Session.QueryOver<Documento>()
               .Where(x => x.TipoDocumento.Id == TipoDocumento.CodigoDocumentoGeral)
               .And(x => x.Lote.Id == loteId)
               .And(x => x.Virtual == false)
               .OrderBy(x => x.Id).Desc
               .Take(1)
               .SingleOrDefault();
        }

        public Documento ObterPdfFilho(Documento documentoPai)
        {
            return this.Session.QueryOver<Documento>()
               .Where(x => x.DocumentoPaiId == documentoPai.Id)
               .And(x => x.Virtual)
               .And(x => x.Status != DocumentoStatus.Excluido)
               .OrderBy(x => x.Id).Desc
               .Take(1)
               .SingleOrDefault();
        }

        public void AlterarOrdemPais(int documentoPaiId, int novaOrdem)
        {
            this.Session
                .CreateQuery(@"update Documento set Ordem = :novaOrdem where DocumentoPaiId = :documentoPaiId")
                .SetParameter("novaOrdem", novaOrdem)
                .SetParameter("documentoPaiId", documentoPaiId)
                .ExecuteUpdate();
        }

        public IList<Documento> ObterPendentesDeRecognitionServerAjusteDeBrancos()
        {
            const string Hql = @"
select
    documento
from 
    Documento documento
inner join 
    documento.Paginas as paginas
where 
    documento.Status = :documentoStatus";

            return this.Session.CreateQuery(Hql)
                .SetParameter("documentoStatus", DocumentoStatus.StatusParaReconhecimentoAjusteDeBrancos)
                .SetResultTransformer(Transformers.DistinctRootEntity)
                .List<Documento>();
        }

        public IList<Documento> ObterProcessadosPeloRecognitionServerAjusteBrancos()
        {
            const string Hql = @"
select
    documento
from 
    Documento documento
inner join 
    documento.Paginas as paginas
where 
    documento.Status = :documentoStatus";

            return this.Session.CreateQuery(Hql)
                .SetParameter("documentoStatus", DocumentoStatus.StatusParaAguardandoReconhecimentoAjusteDeBrancos)
                .SetResultTransformer(Transformers.DistinctRootEntity)
                .List<Documento>();
        }

        public Documento ObterPdfFilhoExcluido(Documento documento)
        {
            return this.Session.QueryOver<Documento>()
               .Where(x => x.DocumentoPaiId == documento.Id)
               .And(x => x.Virtual)
               .OrderBy(x => x.Id).Desc
               .Take(1)
               .SingleOrDefault();
        }

        public IList<Documento> ObterPorProcesso(int processoId)
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.Processo.Id == processoId)
                .Fetch(x => x.Processo).Eager
                .List();
        }

        public IList<Documento> ObterFilhos(int loteId, int documentoPaiId)
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.Lote.Id == loteId)
                .And(x => x.DocumentoPaiId == documentoPaiId)
                .List();
        }

        public void LimparFraudes(int loteId)
        {
            this.Session
                .CreateQuery(@"update Documento set IndicioDeFraude = null where Lote.Id = :loteId")
                .SetParameter("loteId", loteId)
                .ExecuteUpdate();
        }

        public void SetarExcluidoAjusteTemporario(int loteId)
        {
            this.Session
                .CreateQuery(@"update Documento set Status = :documentoStatus where Lote.Id = :loteId and TipoDocumento.Id = :tipoAjuste")
                .SetParameter("loteId", loteId)
                .SetParameter("documentoStatus", DocumentoStatus.Excluido)
                .SetParameter("tipoAjuste", TipoDocumento.CodigoEmAjuste)
                .ExecuteUpdate();
        }

        public void MarcarConcluidoRecognitionService(int documentoId)
        {
            this.Session
                .CreateQuery(@"update Documento set RecognitionService = 1, RecognitionEm = :dataHoraFim where Id = :documentoId")
                .SetParameter("documentoId", documentoId)
                .SetParameter("dataHoraFim", DateTime.Now)
                .ExecuteUpdate();
        }

        public void MarcarInicioRecognitionService(int documentoId)
        {
            this.Session
               .CreateQuery(@"update Documento set RecognitionInicioEm = :dataHoraInicio where Id = :documentoId")
               .SetParameter("documentoId", documentoId)
               .SetParameter("dataHoraInicio", DateTime.Now)
               .ExecuteUpdate();
        }

        public void AlterarRecognitionService(int documentoId, DocumentoStatus novoStatus, bool recognitionService)
        {
            this.Session
               .CreateQuery(@"update Documento set Status = :novoStatus, RecognitionService = :recognitionService where Id = :documentoId")
               .SetParameter("documentoId", documentoId)
               .SetParameter("novoStatus", novoStatus)
               .SetParameter("recognitionService", recognitionService)
               .ExecuteUpdate();
        }

        public void AlterarStatus(int processoId, int tipoDocumentoId, DocumentoStatus novoStatus)
        {
            this.Session
               .CreateQuery(@"
update Documento set 
Status = :novoStatus 
where Processo.Id = :processoId
and TipoDocumento.Id = :tipoDocumento")
               .SetParameter("processoId", processoId)
               .SetParameter("novoStatus", novoStatus)
               .SetParameter("tipoDocumento", tipoDocumentoId)
               .ExecuteUpdate();
        }

        public void MarcarInicioPosRecognitionService(int documentoId)
        {
            this.Session
               .CreateQuery(@"update Documento set RecognitionPosAjusteInicioEm = :dataHoraInicio where Id = :documentoId")
               .SetParameter("documentoId", documentoId)
               .SetParameter("dataHoraInicio", DateTime.Now)
               .ExecuteUpdate();
        }

        public void MarcarConcluidoRecognitionPosAjusteService(int documentoId)
        {
            this.Session
                .CreateQuery(@"update Documento set RecognitionPosAjusteServiceFinalizado = 1, RecognitionPosAjusteTerminoEm = :dataHoraFim where Id = :documentoId")
                .SetParameter("documentoId", documentoId)
                .SetParameter("dataHoraFim", DateTime.Now)
                .ExecuteUpdate();
        }

        public void ApagarPorLote(Lote lote)
        {
            this.Session.CreateQuery(
                "delete from Documento where Lote.Id = :loteId")
                .SetParameter("loteId", lote.Id)
                .ExecuteUpdate();
        }

        public void ApagarVirtualPorLote(int loteId)
        {
            this.Session.CreateQuery(
                "delete from Documento where Virtual = 1 and Lote.Id = :loteId")
                .SetParameter("loteId", loteId)
                .ExecuteUpdate();
        }

        public void AtualizarAposClassificacao(DocumentoStatus status, TipoDocumento tipoDocumento, int documentoId)
        {
            this.Session
                .CreateQuery(@"update Documento set Status = :status, TipoDocumento = :tipoDocumento, Reclassificado = true where Id = :documentoId")
                .SetParameter("status", status)
                .SetParameter("tipoDocumento", tipoDocumento)
                .SetParameter("documentoId", documentoId)
                .ExecuteUpdate();
        }

        public void AtualizarAposIdentificacaoManual(TipoDocumento tipoDocumento, int documentoId)
        {
            this.Session
                .CreateQuery(@"update Documento set TipoDocumento = :tipoDocumento where Id = :documentoId")
                .SetParameter("tipoDocumento", tipoDocumento)
                .SetParameter("documentoId", documentoId)
                .ExecuteUpdate();
        }

        public IList<Documento> ObterPorLoteComTipo(Lote lote)
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.Lote == lote)
                .Fetch(x => x.TipoDocumento).Eager
                .List();
        }

        public void LimparHoraInicioEResponsavel(Lote lote)
        {
            this.Session
              .CreateQuery("update Documento set HoraInicio = null, UsuarioResponsavelId =  null where Lote.Id = :loteId")
              .SetInt32("loteId", lote.Id)
              .ExecuteUpdate();
        }

        public IList<Documento> ObterDocumentosComErroDeAssinatura()
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.Status == DocumentoStatus.ErroAoAssinar)
                .Fetch(x => x.Lote).Eager
                .JoinQueryOver<Lote>(x => x.Lote)
                .Where(x => x.Status == LoteStatus.AguardandoAssinatura)
                .List();
        }

        public void AjusteFinalizado(int documentoId)
        {
            this.Session
              .CreateQuery("update Documento set RecognitionPosAjusteServiceFinalizado = null where Id = :id")
              .SetParameter("id", documentoId)
              .ExecuteUpdate();
        }

        public void ExcluirCapaTermoAutenticacaoPorLote(int loteId)
        {
            this.Session
              .CreateQuery("update Documento set Status = :status where Lote.Id = :loteId and (TipoDocumento.Id = :tipoCapa or TipoDocumento.Id = :tipoTermo ) and Status != :statusExcluido ")
              .SetParameter("status", DocumentoStatus.Excluido)
              .SetParameter("loteId", loteId)
              .SetParameter("tipoCapa", TipoDocumento.CodigoFolhaDeRosto)
              .SetParameter("tipoTermo", TipoDocumento.CodigoTermoAutuacaoDossie)
              .SetParameter("statusExcluido", DocumentoStatus.Excluido)
              .ExecuteUpdate();
        }
    }
}
