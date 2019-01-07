namespace Veros.Paperless.Infra.Repositorios
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Model.Consultas;
    using Model.ViewModel;
    using NHibernate.Criterion;
    using NHibernate.Transform;
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class LoteRepositorio : Repositorio<Lote>, ILoteRepositorio
    {
        public IList<Lote> ObterTodosPorData(DateTime data)
        {
            var listaStatus = new[]
            {
                LoteStatus.ParaTransmitir, 
                LoteStatus.AguardandoReconhecimento,
                LoteStatus.AguardandoIdentificacao, 
                LoteStatus.AguardandoMontagem
            };

            return this.Session.QueryOver<Lote>()
                ////.Where(x => x.DataCriacao == data)
                .WhereRestrictionOn(x => x.Status).IsIn(listaStatus)
                .List();
        }

        public void AlterarStatusParaOcrFinalizado(int loteId)
        {
            this.Session
               .CreateQuery("update Lote set Status = :status where Id = :id")
               .SetInt32("id", loteId)
               .SetParameter("status", LoteStatus.ReconhecimentoExecutado)
               .ExecuteUpdate();
        }

        public IList<TotalDocumentoPorFaseConsulta> ObterTotalDeDocumentosPorFase()
        {
            var statusConsulta = new[]
            {
                LoteStatus.ParaTransmitir,
                LoteStatus.ReconhecimentoExecutado,
                LoteStatus.AguardandoIdentificacao,
                LoteStatus.AguardandoMontagem,
                LoteStatus.AguardandoReconhecimento,
                LoteStatus.EmExportacao,
                LoteStatus.Finalizado,
                LoteStatus.EmExportacao
            };

            return this.Session.CreateCriteria(typeof(Lote))
                .Add(Restrictions.In("Status", statusConsulta))
                .SetProjection(Projections.ProjectionList()
                    .Add(Projections.Alias(Projections.GroupProperty("Status"), "Status"))
                    .Add(Projections.Alias(Projections.Sum("QuantidadeDocumentos"), "QuantidadeDocumentos")))
                .SetResultTransformer(Transformers.AliasToBean(typeof(TotalDocumentoPorFaseConsulta)))
                .List<TotalDocumentoPorFaseConsulta>();
        }

        public IList<Lote> ObterPorPacote(PacoteProcessado pack)
        {
            return this.Session.QueryOver<Lote>()
                .Where(x => x.PacoteProcessado.Id == pack.Id)
                .And(x => x.Status == LoteStatus.Finalizado)
                .List();
        }

        public IList<Lote> ObterPorPacoteId(int pacoteProcessadoId)
        {
            return this.Session.QueryOver<Lote>()
                .Where(x => x.PacoteProcessado.Id == pacoteProcessadoId)
                .List();
        }

        public IList<Lote> ObterPorLoteCefId(int lotecefId)
        {
            return this.Session.QueryOver<Lote>()
                .Fetch(x => x.Processos).Eager
                .Where(x => x.LoteCef.Id == lotecefId)
                .List();
        }

        public IList<Lote> ObterParaCertificadoQualidade(int lotecefId)
        {
            return this.Session.QueryOver<Lote>()
                .Where(x => x.LoteCef.Id == lotecefId)
                .Fetch(x => x.DossieEsperado).Eager
                .Fetch(x => x.Processos).Eager
                .Fetch(x => x.Pacote).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
        }

        public IList<Lote> ObterPorDataDeCadastro(
            DateTime dataInicio,
            DateTime dataFim)
        {
            return this.Session.QueryOver<Lote>()
                .Where(x => x.DataCriacao >= dataInicio)
                .And(x => x.DataCriacao < dataFim.AddDays(1))
                .OrderBy(x => x.Id).Asc
                .List();
        }

        public Lote ObterLoteParaProcessarNoWorkflow(int loteId)
        {
            return this.Session.QueryOver<Lote>()
                .Where(x => x.Id == loteId)
                .Fetch(x => x.Processos).Eager
                .Fetch(x => x.PacoteProcessado).Eager
                .Fetch(x => x.Processos.First().Documentos).Eager
                .Fetch(x => x.Processos.First().Documentos.First().Paginas).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .SingleOrDefault();
        }

        public void AlterarStatus(int loteId, LoteStatus status)
        {
            this.Session
               .CreateQuery("update Lote set Status = :status where Id = :id")
               .SetParameter("id", loteId)
               .SetParameter("status", status)
               .ExecuteUpdate();
        }

        public IList<int> ObterIdsParaProcessarNoWorkflow()
        {
            //// TODO: quando lote for montagem só trazer se processo tiver no status x
            return this.Session.QueryOver<Lote>()
                .WhereRestrictionOn(x => x.Status).IsIn(LoteStatus.ListaDeStatusParaWorklofowProcessar)
                .OrderBy(x => x.Id).Asc
                .Select(x => x.Id)
                .List<int>();
        }

        public IList<Lote> ObterPendentesDeRetransmissao()
        {
            return this.Session.QueryOver<Lote>()
            .Where(x => x.Status == LoteStatus.AguardandoRetransmissaoImagem)
            .Fetch(x => x.PacoteProcessado).Eager
            .List();
        }

        public void GravarHoraGeracaoArquivoXml(int loteId)
        {
            this.Session
               .CreateQuery("update Lote set DataFimArquivoXml = sysdate where Id = :id")
               .SetParameter("id", loteId)
               .ExecuteUpdate();
        }

        public IList<Lote> ObterLotesParaExpurgo(int quantidadeDeDias)
        {
            return this.Session.QueryOver<Lote>()
                .Where(x => x.Status == LoteStatus.Finalizado)
                .And(x => x.DataCriacao < DateTime.Now.AddDays(quantidadeDeDias * -1))
                .List();
        }

        public Lote ObterPorIdentificacao(string identificacao)
        {
            return this.Session.QueryOver<Lote>()
                .Where(x => x.Identificacao == identificacao)
                .Take(1)
                .SingleOrDefault();
        }

        public IList<string> ObterIdentificacoesParaExpurgo(int intervaloDeDias)
        {
            return this.Session.QueryOver<Lote>()
                .WhereRestrictionOn(x => x.Status).IsIn(LoteStatus.ListaDeStatusParaExpurgo)
                .And(x => x.DataCriacao < DateTime.Now.AddDays(intervaloDeDias * -1))
                .Inner.JoinQueryOver<Processo>(x => x.Processos.First())
                .Where(x => x.Decisao == ProcessoDecisao.Ok)
                .Select(x => x.Identificacao)
                .List<string>();

            /*
            return this.Session.QueryOver<Processo>()
                .Where(x => x.Decisao == ProcessoDecisao.Ok)
                .Inner.JoinQueryOver<Lote>(x => x.Lote)
                .WhereRestrictionOn(x => x.Status).IsIn(LoteStatus.ListaDeStatusParaExpurgo)
                .And(x => x.DataCriacao < DateTime.Now.AddDays(intervaloDeDias * -1))
                .Fetch(x => x.Lote).Eager
                .Select(x => x.Lote.Identificacao)
                .List<string>();
             */
        }

        public Lote ObterUltimo()
        {
            return this.Session.QueryOver<Lote>()
                .OrderBy(x => x.Id).Desc
                .Take(1)
                .SingleOrDefault();
        }

        public IList<int> ObterIdsComStatus(LoteStatus status)
        {
            return this.Session.QueryOver<Lote>()
                .Where(x => x.Status == status)
                .OrderBy(x => x.Id).Asc
                .Select(x => x.Id)
                .List<int>();
        }

        public IList<Lote> ObterPendentesDeImportacao()
        {
            return this.Session.QueryOver<Lote>()
                .Where(x => x.Status == LoteStatus.EmRecepcao)
                .OrderBy(x => x.Id).Asc
                .List();
        }

        public IList<Lote> ObterPendentesConsultaVertros()
        {
            return this.Session.QueryOver<Lote>()
              .Where(x => x.ConsultaVertrosRealizada == false)
              .OrderBy(x => x.Id).Asc
              .List();
        }

        public IList<Lote> ObterPendentesComplementacao()
        {
            return this.Session.QueryOver<Lote>()
               .Where(x => x.ConsultaPh3Realizada == false)
               .OrderBy(x => x.Id).Asc
               .List();
        }
        
        public void AtualizaParaLoteConsultado(int id)
        {
            this.Session
               .CreateQuery("update Lote set ConsultaPh3Realizada = true where Id = :id")
               .SetParameter("id", id)
               .ExecuteUpdate();
        }

        public void AtualizaVertrosOk(int id)
        {
            this.Session
              .CreateQuery("update Lote set ConsultaVertrosRealizada = true where Id = :id")
              .SetParameter("id", id)
              .ExecuteUpdate();
        }

        public IList<Lote> ObterComPaginasPendentesClassifier()
        {
            return this.Session.QueryOver<Lote>()
                .Where(x => x.Status == LoteStatus.Montado)
                .Fetch(x => x.Processos).Eager
                .Fetch(x => x.Processos.First().Documentos).Eager
                .Fetch(x => x.Processos.First().Documentos.First().TipoDocumento).Eager
                .Fetch(x => x.Processos.First().Documentos.First().Paginas).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .List<Lote>();
        }

        public IEnumerable<Lote> ObterUltimos()
        {
            return this.Session.QueryOver<Lote>()
                .Where(x => x.Status == LoteStatus.Montado)
                .Fetch(x => x.Processos).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .OrderBy(x => x.Id).Desc
                .Take(30)
                .List<Lote>();
        }

        public void AlterarParaRecepcaoFinalizada(int loteId)
        {
            this.Session
               .CreateQuery("update Lote set Status = :status, DataFimEnvio = :dateTime where Id = :id")
               .SetParameter("id", loteId)
               .SetParameter("status", LoteStatus.AguardandoTransmissao)
               .SetParameter("dateTime", DateTime.Now)
               .ExecuteUpdate();
        }

        public IList<Lote> ObterPendentesDeCaptura()
        {
            return this.Session.QueryOver<Lote>()
                .Where(x => x.Status == LoteStatus.EmCaptura)
                .Fetch(x => x.Processos).Eager
                .Fetch(x => x.Processos.First().TipoDeProcesso).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .List<Lote>();
        }

        public IList<Lote> ObterPorDossieId(int dossieId)
        {
            return this.Session.QueryOver<Lote>()
                .Where(x => x.DossieEsperado.Id == dossieId)
                .Fetch(x => x.DossieEsperado).Eager
                .Fetch(x => x.Processos.First().TipoDeProcesso).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .List<Lote>();
        }

        public void GravarResultadoQualidadeCef(int loteId, LoteStatus novoStatus, string resultadoAnalise)
        {
            this.Session
               .CreateQuery("update Lote set Status = :novoStatus, ResultadoQualidadeCef = :resultadoAnalise where Id = :loteId")
               .SetParameter("novoStatus", novoStatus)
               .SetParameter("resultadoAnalise", resultadoAnalise)
               .SetParameter("loteId", loteId)
               .ExecuteUpdate();
        }

        public void AlterarParaEncerrarDigitalizacao(int loteId)
        {
            this.Session
                .CreateQuery("update Lote set Status = :status where Id = :id")
                .SetParameter("id", loteId)
                .SetParameter("status", LoteStatus.EmTransmissao)
                .ExecuteUpdate();
        }

        public Lote ObterPorProcessoId(int processoId)
        {
            return this.Session.QueryOver<Lote>()
                .Where(x => x.Processos.First().Id == processoId)
                .SingleOrDefault();
        }

        /// <summary>
        /// Esse método somente utilizado para pegar os lotes que precisam gerar termos de forma avulsa
        /// isso foi devido a um erro na geração da quantidade de paginas identificado pelo loredi.
        /// a geração de termos avulso é acionado pelo comando m2 corrigir termos [parametros]
        /// Por Tiago Brito
        /// </summary>
        /// <returns>Lista de Ids de lotes para re-gerar termos</returns>
        public IList<int> ObterPendentesGeracaoTermosAvulsos()
        {
            var listaDeStatus = new[]
            {
                LoteStatus.AguardandoControleQualidadeCef, 
                LoteStatus.AguardandoPreparacaoAjustes,
                LoteStatus.AguardandoAjustes,
                LoteStatus.AjustesSolicitados,
                LoteStatus.Faturamento
            };

            return this.Session.QueryOver<Lote>()
                .WhereRestrictionOn(x => x.Status).IsIn(listaDeStatus)
                .And(x => x.FoiGeradoTermoAvulso == false || x.FoiGeradoTermoAvulso == null)
                .OrderBy(x => x.Id).Asc
                .Select(x => x.Id)
                .List<int>();
        }

        /// <summary>
        /// Esse método somente utilizado para pegar os lotes que precisam gerar termos de forma avulsa
        /// isso foi devido a um erro na geração da quantidade de paginas identificado pelo Loredi.
        /// a geração de termos avulso é acionado pelo comando m2 corrigir termos [parametros]
        /// Por Tiago Brito
        /// </summary>
        public void MarcarTermoAvulsoRealizado(int loteId)
        {
            this.Session
                .CreateQuery("update Lote set FoiGeradoTermoAvulso = true where Id = :loteId")
                .SetParameter("loteId", loteId)
                .ExecuteUpdate();
        }

        public void EnviarParaQualidadeCef(int loteId, int novaSituacaoAmostragem)
        {
            this.Session
                .CreateQuery("update Lote set QualidadeCef = :novaSituacaoAmostragem, Status = :status where Id = :loteId")
                .SetParameter("status", LoteStatus.AguardandoControleQualidadeCef)
                .SetParameter("novaSituacaoAmostragem", novaSituacaoAmostragem)
                .SetParameter("loteId", loteId)
                .ExecuteUpdate();
        }

        public void MarcarAvaliacaoBrancoOk(int loteId)
        {
            this.Session
                .CreateQuery("update Lote set AtualizacaoBrancoFinalizada = true where Id = :loteId")
                .SetParameter("loteId", loteId)
                .ExecuteUpdate();
        }

        public IList<int> ObterLotesParaReprocessamentoPrioritariosBrancos()
        {
            return this.Session.QueryOver<Lote>()
                .Where(x => x.Hash == "01")
                .And(x => x.AtualizacaoBrancoFinalizada == false || x.AtualizacaoBrancoFinalizada == null)
                .OrderBy(x => x.Id).Asc
                .Select(x => x.Id)
                .List<int>();
        }

        public void AlterarParaCapturaFinalizada(int loteId, PacoteProcessado pacoteProcessado)
        {
            this.Session
               .CreateQuery("update Lote set Status = :status, DataFimCaptura = :dateTime, PacoteProcessado.Id = :pacoteProcessadoId where Id = :id")
               .SetParameter("status", LoteStatus.CapturaFinalizada)
               .SetParameter("dateTime", DateTime.Now)
               .SetParameter("pacoteProcessadoId", pacoteProcessado.Id)
               .SetParameter("id", loteId)
               .ExecuteUpdate();
        }

        public void AlterarParaRecapturaFinalizada(int loteId)
        {
            this.Session
               .CreateQuery("update Lote set Status = :status where Id = :id")
               .SetParameter("status", LoteStatus.CapturaFinalizada)
               .SetParameter("id", loteId)
               .ExecuteUpdate();
        }

        public void MarcarParaRecaptura(int loteId)
        {
            this.Session
              .CreateQuery("update Lote set Recapturado = true where Id = :id")
              .SetParameter("id", loteId)
              .ExecuteUpdate();
        }

        public void SetarExcluido(int loteId, string motivo)
        {
            this.Session
              .CreateQuery("update Lote set Status = '*', Hash = :motivo where Id = :loteId")
              .SetParameter("motivo", motivo)
              .SetParameter("loteId", loteId)
              .ExecuteUpdate();
        }

        public void SetarPriorizacaoDeLote(int loteId, string resultadoQualidadeCef)
        {
            this.Session
              .CreateQuery("update Lote set ResultadoQualidadeCef = :resultadoQualidadeCef where Id = :loteId")
              .SetParameter("resultadoQualidadeCef", resultadoQualidadeCef)
              .SetParameter("loteId", loteId)
              .ExecuteUpdate();
        }        

        public IList<Lote> ObterLotesAbertosComQualiM2Sys()
        {
            return this.Session.QueryOver<Lote>()
                .Where(x => x.Status == LoteStatus.Faturamento)
                .Where(x => x.QualidadeM2sys == 1)
                .JoinQueryOver<LoteCef>(x => x.LoteCef)
                .Where(x => x.Status == LoteCefStatus.Aberto)
                .List();
        }

        public void EnviarParaQualidadeMudandoLote(int loteId, int lotecefId)
        {
            this.Session
                .CreateQuery("update Lote set QualidadeCef = 1, Status = :status, LoteCef = :lotecefId where Id = :loteId")
                .SetParameter("status", LoteStatus.AguardandoControleQualidadeCef)
                .SetParameter("lotecefId", lotecefId)
                .SetParameter("loteId", loteId)
                .ExecuteUpdate();
        }

        public void AtualizarLotecef(int loteId, int lotecefId)
        {
            this.Session
                .CreateQuery("update Lote set LoteCef = :lotecefId where Id = :loteId")
                .SetParameter("lotecefId", lotecefId)
                .SetParameter("loteId", loteId)
                .ExecuteUpdate();
        }

        public void SetarParaRecaptura(int loteId, string motivo)
        {
            this.Session
              .CreateQuery("update Lote set Status = 'R5', Recapturado = true, Hash = :motivo where Id = :id")
              .SetParameter("id", loteId)
              .SetParameter("motivo", motivo)
              .ExecuteUpdate();
        }

        public Lote ObterComPacote(int loteId)
        {
            return this.Session.QueryOver<Lote>()
                .Where(x => x.Id == loteId)
                .Fetch(x => x.PacoteProcessado).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .SingleOrDefault();
        }        

        public void SetarMarcaQualidade(int loteId, string loteMarcaQualidade)
        {
            this.Session
              .CreateQuery("update Lote set ProblemaQualidade = :loteMarcaQualidade where Id = :loteId")
              .SetParameter("loteMarcaQualidade", loteMarcaQualidade)
              .SetParameter("loteId", loteId)
              .ExecuteUpdate();
        }

        public void PriorizarLote(int loteId)
        {
            this.Session
              .CreateQuery("update Lote set ResultadoQualidadeCef = 'P' where Id = :loteId And ResultadoQualidadeCef = Null")
              .SetParameter("loteId", loteId)
              .ExecuteUpdate();
        }

        public IList<Lote> ObterPorLoteCef(int loteCefId)
        {
            return this.Session.QueryOver<Lote>()
                .Where(x => x.LoteCef.Id == loteCefId)
                .List();
        }

        public void AtualizarStatus(IEnumerable<Lote> lotes, LoteStatus novoStatus, int identificacaoNovaAmostra)
        {
            this.Session
              .CreateQuery("update Lote set Status = :status, QualidadeCef = :identificacaoNovaAmostra where Id in (:lotes)")
              .SetParameter("status", novoStatus)
              .SetParameter("identificacaoNovaAmostra", identificacaoNovaAmostra)
              .SetParameterList("lotes", lotes.Select(x => x.Id).ToArray())
              .ExecuteUpdate();
        }

        public void EnviarParaQualidadeM2(List<Lote> lotes)
        {
            this.Session
              .CreateQuery("update Lote set QualidadeM2sys = 1 where Id in (:lotes)")
              .SetParameterList("lotes", lotes.Select(x => x.Id).ToArray())
              .ExecuteUpdate();
        }

        public Lote ObterMaiorAmostraPorLoteCef(int loteCefId)
        {
            return this.Session.QueryOver<Lote>()
               .Where(x => x.LoteCef.Id == loteCefId)
               .And(x => x.QualidadeCef > 0)
               .OrderBy(x => x.QualidadeCef).Desc
               .Take(1)
               .SingleOrDefault();
        }

        public IList<int> ObterPendentesEnvioCloud()
        {
            return this.Session.QueryOver<Lote>()
                .Where(x => x.Status == LoteStatus.EmExportacaoParaCloud)
                .OrderBy(x => x.ResultadoQualidadeCef).Asc
                .ThenBy(x => x.Id).Asc
                .Select(x => x.Id)
                .List<int>();
        }

        public IList<int> ObterJpgsPendentesEnvioCloud()
        {
            return this.Session.QueryOver<Lote>()
                .Where(x => x.JpegsEnviadosParaCloud == false)
                .Where(x => x.CloudOk)
                .OrderBy(x => x.ResultadoQualidadeCef).Asc
                .ThenBy(x => x.Id).Asc
                .Select(x => x.Id)
                .List<int>();
        }

        public void MarcarComoEnviadoParaCloud(int loteId)
        {
            this.Session
               .CreateQuery("update Lote set Status = :status, CloudOk = true, PdfNoCloudEm = :data where Id = :id")
               .SetInt32("id", loteId)
               .SetParameter("data", DateTime.Now)
               .SetParameter("status", LoteStatus.ExportadoParaCloud)
               .ExecuteUpdate();
        }

        public void MarcarComoJpegsEnviadosParaCloud(int loteId)
        {
            this.Session
              .CreateQuery("update Lote set JpegsEnviadosParaCloud = true, JpegNoCloudEm = :data where Id = :id")
              .SetInt32("id", loteId)
              .SetParameter("data", DateTime.Now)
              .ExecuteUpdate();
        }

        public IList<int> ObterPendentesExpurgoFileTransfer()
        {
            return this.Session.QueryOver<Lote>()
               .Where(x => x.JpegsEnviadosParaCloud)
               .Where(x => x.CloudOk)
               .Where(x => x.RemovidoFileTransferM2 == false)
               .OrderBy(x => x.ResultadoQualidadeCef).Asc
               .ThenBy(x => x.Id).Asc
               .Select(x => x.Id)
               .List<int>();
        }

        public void MarcarComoRemovidoFileTransferM2(int loteId)
        {
            this.Session
              .CreateQuery("update Lote set RemovidoFileTransferM2 = true, RemovidoFileTransferEm = :data where Id = :id")
              .SetInt32("id", loteId)
              .SetParameter("data", DateTime.Now)
              .ExecuteUpdate();
        }

        public void MarcarParaEnviarParaFileTransfer(Lote lote)
        {
            this.Session
              .CreateQuery("update Lote set RemovidoFileTransferM2 = false, CloudOk = false, JpegsEnviadosParaCloud = false where Id = :id")
              .SetInt32("id", lote.Id)
              .ExecuteUpdate();
        }

        public void AtualizarTodosParaAprovadoPorLotecef(int loteCefId)
        {
            this.Session
              .CreateQuery("update Lote set Status = :status where LoteCef = :loteCefId and (Status = :statusH0 or Status = :statusQ5)")
              .SetParameter("status", LoteStatus.AprovadoCef)
              .SetParameter("loteCefId", loteCefId)
              .SetParameter("statusH0", LoteStatus.Faturamento)
              .SetParameter("statusQ5", LoteStatus.AguardandoControleQualidadeCef)
              .ExecuteUpdate();
        }

        public void AtualizarTodosParaFaturaEmitidaPorLotecef(int lotecefId)
        {
            this.Session
              .CreateQuery("update Lote set Status = :status where LoteCef = :loteCefId and (Status = :statusH0 or Status = :statusQ5 or Status = :statusH1)")
              .SetParameter("status", LoteStatus.FaturaEmitida)
              .SetParameter("loteCefId", lotecefId)
              .SetParameter("statusH0", LoteStatus.Faturamento)
              .SetParameter("statusQ5", LoteStatus.AguardandoControleQualidadeCef)
              .SetParameter("statusH1", LoteStatus.AprovadoCef)
              .ExecuteUpdate();
        }
    }
}
