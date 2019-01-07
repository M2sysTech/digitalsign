namespace Veros.Paperless.Infra.Repositorios
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework;
    using Iesi.Collections.Generic;
    using Model.ViewModel;
    using NHibernate.Criterion;
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using NHibernate.Transform;
    using Veros.Paperless.Model.Repositorios;

    public class ProcessoRepositorio : Repositorio<Processo>, IProcessoRepositorio
    {
        public IList<Processo> ObterTodosComStatusEmValidacao()
        {
            return this.Session.QueryOver<Processo>()
                .Where(x => x.Status == ProcessoStatus.AguardandoValidacao)
                .Fetch(x => x.Documentos).Eager
                .Fetch(x => x.Documentos.First().TipoDocumento).Eager
                .Fetch(x => x.Documentos.First().Indexacao.First()).Eager
                .Fetch(x => x.Documentos.First().Indexacao.First().Campo).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .List<Processo>();
        }

        public IList<Processo> ObterPorAgenciaEConta(string agencia, string conta)
        {
            return this.Session.QueryOver<Processo>()
                .Where(x => x.Agencia == agencia)
                .Where(x => x.Conta == conta)
                .List();
        }
        
        public Processo ObterPorAgenciaEContaAguardandoRetorno(string agencia, string conta)
        {
            return this.Session.QueryOver<Processo>()
                .Where(x => x.Agencia == agencia)
                .Where(x => x.Conta == conta)
                .Where(x => x.Status == ProcessoStatus.AguardandoRetorno || x.Status == ProcessoStatus.SetaRetorno)
                .Take(1)
                .SingleOrDefault(); 
        }

        public void DevolverProcesso(int processoId, string parecerDoBanco)
        {
            this.Session
              .CreateQuery("update Processo set Status = :status, ObservacaoProcesso = :parecerDoBanco, Decisao = :decisao where Id = :id")
              .SetParameter("id", processoId)
              .SetParameter("status", ProcessoStatus.Aprovado)
              .SetParameter("parecerDoBanco", parecerDoBanco)
              .SetParameter("decisao", ProcessoDecisao.Devolvido)
              .ExecuteUpdate();
        }

        public Processo ObterPorAgenciaEContaParaAprovacao(string agencia, string conta)
        {
            return this.Session.QueryOver<Processo>()
                .Where(x => x.Agencia == agencia)
                .Where(x => x.Conta == conta)
                .Where(x => x.Status == ProcessoStatus.AguardandoAprovacao || x.Status == ProcessoStatus.AguardandoAprovacaoEspecial)
                .Inner.JoinQueryOver<RegraViolada>(x => x.RegrasVioladas)
                .Where(x => x.Status == RegraVioladaStatus.Pendente || x.Status == RegraVioladaStatus.Marcada)
                .Fetch(x => x.UsuarioResponsavel).Eager
                .Take(1)
                .SingleOrDefault();
        }

        public bool ExisteDocumentoComFraude(int processoId)
        {
            return this.Session.QueryOver<Documento>()
                .Fetch(x => x.Processo).Eager
                .Where(x => x.IndicioDeFraude != null)
                .Where(x => x.Processo.Id == processoId)
                .List().Count > 0;
        }

        public IList<Processo> ObterPorPacoteProcessado(int pacoteId)
        {
            return this.Session.QueryOver<Processo>()
                .Fetch(l => l.Lote).Eager
                .JoinQueryOver(x => x.Lote.PacoteProcessado)
                .Where(x => x.Id == pacoteId)
                .List();
        }

        public IList<Processo> ObterPorPacoteProcessado(int pacoteId, int tipoProcessoId)
        {
            return this.Session.QueryOver<Processo>()
                .Where(x => x.TipoDeProcesso.Id == tipoProcessoId || tipoProcessoId == 0)
                .OrderBy(x => x.Agencia).Asc
                .ThenBy(x => x.Conta).Asc
                .Fetch(l => l.Lote).Eager
                .JoinQueryOver(x => x.Lote.PacoteProcessado)
                .Where(x => x.Id == pacoteId)
                .List();
        }

        public void AlterarStatus(int id, string status)
        {
            this.Session
              .CreateQuery("update Processo set Status = :status where Id = :id")
              .SetInt32("id", id)
              .SetString("status", status)
              .ExecuteUpdate();
        }

        public IList<Processo> ObterTodosParaExportacao(int take = 50)
        {
            return this.Session.QueryOver<Processo>()
                .Fetch(processo => processo.Lote).Eager
                .Fetch(processo => processo.UsuarioResponsavel).Eager
                .Fetch(processo => processo.Documentos).Eager
                .Fetch(processo => processo.Documentos.First().Paginas.First()).Eager
                .Fetch(processo => processo.Documentos.First().Indexacao).Eager
                .Fetch(processo => processo.Documentos.First().Indexacao.First().Campo).Eager
                .Fetch(processo => processo.RegrasVioladas).Eager
                .Fetch(processo => processo.RegrasVioladas.First().Regra).Eager
                .Where(x => x.Status == ProcessoStatus.AguardandoExportacao)
                .TransformUsing(Transformers.DistinctRootEntity)
                .OrderBy(x => x.Id).Asc
                .List<Processo>();
        }

        public Processo ObterPorIdParaExportacao(int id)
        {
            var processo = this.Session.QueryOver<Processo>()
                .Fetch(x => x.Documentos.First()).Eager
                .Fetch(x => x.Documentos.First().TipoDocumento).Eager
                .Fetch(x => x.Documentos.First().Indexacao.First().Campo).Eager
                .Fetch(x => x.Documentos.First().Indexacao).Eager
                ////.Fetch(x => x.RegrasVioladas.First()).Eager
                .Where(x => x.Id == id)
                .TransformUsing(Transformers.DistinctRootEntity)
                .SingleOrDefault();

            processo.RegrasVioladas = new List<RegraViolada>(this.Session.QueryOver<RegraViolada>()
                .Where(x => x.Processo.Id == id)
                .Fetch(x => x.Regra).Eager
                .List());

            return processo;
        }

        public void LimparResponsavelEHoraInicio(int processoId)
        {
            this.Session
              .CreateQuery("update Processo set UsuarioResponsavel = null, HoraInicio = null, HoraInicioAjuste = null where Id = :id")
              .SetInt32("id", processoId)
              .ExecuteUpdate();
        }

        public void LimparResponsavelEHoraInicio(int processoId, ProcessoStatus processoStatus)
        {
            this.Session
              .CreateQuery("update Processo set UsuarioResponsavel = null, HoraInicio = null, HoraInicioAjuste = null where Id = :id and Status = :status")
              .SetInt32("id", processoId)
              .SetParameter("status", processoStatus)
              .ExecuteUpdate();
        }

        public IList<Processo> ObterPorCaixa(int caixaId)
        {
            return this.Session.QueryOver<Processo>()
                .Where(x => x.Status != ProcessoStatus.Excluido)
                .JoinQueryOver(x => x.Lote)                
                .Where(x => x.Pacote.Id == caixaId)
                .OrderBy(x => x.Id).Asc                
                .List<Processo>();
        }

        public IList<Processo> ObterPorPacote(int pacoteId)
        {
            return this.Session.QueryOver<Processo>()
                .Where(x => x.Status != ProcessoStatus.Excluido)
                .JoinQueryOver(x => x.Lote)
                .Where(x => x.Pacote.Id == pacoteId)
                .List<Processo>();
        }

        public void AlterarResponsavel(int processoId, int usuarioId)
        {
            this.Session
              .CreateQuery(@"
update Processo set UsuarioResponsavel = :usuarioId, HoraInicio = :dataAtual 
where Id = :id")
              .SetInt32("usuarioId", usuarioId)
              .SetParameter("dataAtual", DateTime.Now)
              .SetInt32("id", processoId)
              .ExecuteUpdate();
        }

        public IList<int> ObterIdsDosProcessosParaExpurgar()
        {
            return this.Session.QueryOver<Processo>()
                .JoinQueryOver(x => x.Lote)
                .JoinQueryOver(x => x.PacoteProcessado.StatusPacote == StatusPacote.Processado)
                .Select(x => x.Id)
                .Take(30)
                .List<int>();
        }

        public IList<int> ObterIdsDosProcessosParaMontagem()
        {
            return this.Session.QueryOver<Processo>()
                .Where(x => x.Status == ProcessoStatus.AguardandoMontagem)
                .OrderBy(x => x.Id).Asc
                .Select(x => x.Id)
                .List<int>();
        }

        public Processo ObterProcessoParaProcessarNaMontagem(int processoId)
        {
            return this.Session.QueryOver<Processo>()
                .Where(x => x.Id == processoId)
                .Fetch(x => x.Lote).Eager
                .Fetch(x => x.Documentos).Eager
                .Fetch(x => x.Documentos.First().TipoDocumento).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .SingleOrDefault();
        }

        public IList<Processo> ObterPorPacoteProcessadoSemPaginas(PacoteProcessado pacoteProcessado)
        {
            const string Hql = @"
select
    processo
from 
    Processo processo
inner join 
    processo.Documentos as documento
inner join
    processo.Lote as lote
where 
    not exists elements(documento.Paginas)
    and lote.PacoteProcessado.Id = :idPacoteProcessado";

            return this.Session.CreateQuery(Hql)
                .SetParameter("idPacoteProcessado", pacoteProcessado.Id)
                .SetResultTransformer(Transformers.DistinctRootEntity)
                .List<Processo>();
        }

        public Documento ObterDocumentoPac(int processoId)
        {
            return this.Session.QueryOver<Documento>()
                .Where(x => x.Processo.Id == processoId)
                .Inner.JoinQueryOver(x => x.TipoDocumento)
                .Where(x => x.IsPac)
                .Take(1)
                .SingleOrDefault();
        }

        public void GravarAprovacao(int processoId)
        {
            this.Session
              .CreateQuery("update Processo set Status = :status, Decisao = :decicao where Id = :id")
              .SetParameter("id", processoId)
              .SetParameter("status", ProcessoStatus.Aprovado)
              .SetParameter("decicao", ProcessoDecisao.LiberadoPeloBanco)
              .ExecuteUpdate();
        }

        public Processo ObterPorAgenciaParaAprovacao(string agencia)
        {
            return this.Session.QueryOver<Processo>()
                 .Where(x => x.Agencia == agencia)
                 .Where(x => x.Status == ProcessoStatus.AguardandoAprovacao)
                 .Where(x => x.HoraInicio == null || x.HoraInicio <= DateTime.Now.AddMinutes(-30))
                 .OrderBy(x => x.Conta).Asc
                 .Inner.JoinQueryOver<RegraViolada>(x => x.RegrasVioladas)
                 .Where(x => x.Status == RegraVioladaStatus.Pendente)
                 .Take(1)
                 .SingleOrDefault();
        }

        public IList<int> ObterIdsParaProcessarNoWorkflow()
        {
            return this.Session.QueryOver<Processo>()
                .WhereRestrictionOn(x => x.Status).IsIn(ProcessoStatus.ListaDeStatusParaWorklofowProcessar)
                .OrderBy(x => x.Id).Asc
                .Select(x => x.Id)
                .List<int>();
        }

        public IList<Processo> ObterPorLote(Lote lote)
        {
            return this.Session.QueryOver<Processo>()
                .Fetch(x => x.Lote).Eager
                .Fetch(x => x.TipoDeProcesso).Eager
                .Where(x => x.Lote == lote)
                .List();
        }

        public Processo ObterDetalheDossieComDocumentos(int processoId)
        {
            return this.Session.QueryOver<Processo>()
                .Where(x => x.Id == processoId)
                .Fetch(x => x.Lote).Eager
                .Fetch(x => x.Lote.Pacote).Eager
                .Fetch(x => x.Documentos).Eager
                .Fetch(x => x.Documentos.First().TipoDocumento).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .SingleOrDefault();
        }

        public Processo ObterPorLote(int loteId)
        {
            return this.Session.QueryOver<Processo>()
                .Fetch(x => x.Lote).Eager
                .Fetch(x => x.TipoDeProcesso).Eager
                .Where(x => x.Lote.Id == loteId)
                .TransformUsing(Transformers.DistinctRootEntity)
                .Take(1)
                .SingleOrDefault();
        }

        public IList<Processo> ObterTodosDoPacote(Pacote pacote)
        {
            const string Hql = @"
select
    processo
from 
    Pacote pacote,
    Lote lote,
    Processo processo
where 
    pacote.Id = lote.Pacote.Id
    and processo.Lote.Id = lote.Id
    and pacote.Id = :pacoteId";

            return this.Session.CreateQuery(Hql)
                .SetParameter("pacoteId", pacote.Id)
                .SetResultTransformer(Transformers.DistinctRootEntity)
                .List<Processo>();
        }

        public IList<Processo> ObterRelatorioDeDevolucao(DateTime? dataInicio, DateTime? dataFim)
        {
            return this.Session.QueryOver<Processo>()
                .Fetch(x => x.TipoDeProcesso).Eager
                .Fetch(x => x.Lote).Eager
                .Fetch(x => x.Lote.Pacote).Eager
                .Fetch(x => x.Lote.Pacote.Coleta).Eager
                .Where(x => x.Status != ProcessoStatus.Excluido)
                .JoinQueryOver(x => x.Lote)
                .JoinQueryOver(x => x.Pacote)
                .JoinQueryOver(x => x.Coleta)
                .Where(x => x.DataDevolucao >= dataInicio.GetValueOrDefault())
                .And(x => x.DataDevolucao <= dataFim.GetValueOrDefault().AddDays(1))
                .And(x => x.Status == ColetaStatus.Devolvido)
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
        }

        public IList<Processo> ObterPorPeriodoDeColeta(DateTime? dataInicio, DateTime? dataFim)
        {
            return this.Session.QueryOver<Processo>()
                .Fetch(x => x.TipoDeProcesso).Eager
                .Fetch(x => x.Lote).Eager
                .Fetch(x => x.Lote.Pacote).Eager
                .Fetch(x => x.Lote.Pacote.Coleta).Eager
                .Where(x => x.Status != ProcessoStatus.Excluido)
                .JoinQueryOver(x => x.Lote)
                .JoinQueryOver(x => x.Pacote)
                .JoinQueryOver(x => x.Coleta)
                .Where(x => x.Data >= dataInicio.GetValueOrDefault())
                .And(x => x.Data <= dataFim.GetValueOrDefault().AddDays(1))
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
        }

        public IList<int> ObterProcessosParaSeparacao()
        {
            return this.Session.QueryOver<Processo>()
               .Select(x => x.Id)
               .OrderBy(x => x.Id).Asc
               .Inner.JoinQueryOver(x => x.Lote)
               .Where(x => x.Status == LoteStatus.AguardandoSeparacaoClassifier)
               .Take(100)
               .List<int>();
        }

        public IList<Processo> ObterTodosPorPeriodoDeColeta(DateTime? dataInicio, DateTime? dataFim)
        {
            return this.Session.QueryOver<Processo>()
                .Fetch(x => x.TipoDeProcesso).Eager
                .Fetch(x => x.Lote).Eager
                .Fetch(x => x.Lote.Pacote).Eager
                .Fetch(x => x.Lote.Pacote.Coleta).Eager
                .JoinQueryOver(x => x.Lote)
                .JoinQueryOver(x => x.Pacote)
                .JoinQueryOver(x => x.Coleta)
                .Where(x => x.Data >= dataInicio.GetValueOrDefault())
                .And(x => x.Data <= dataFim.GetValueOrDefault().AddDays(1))
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
        }

        public void AlterarStatusPorLote(int loteId, ProcessoStatus processoStatus)
        {
            this.Session
              .CreateQuery("update Processo set Status = :processoStatus where Lote.Id = :loteId")
              .SetParameter("loteId", loteId)
              .SetParameter("processoStatus", processoStatus)
              .ExecuteUpdate();
        }

        public void AlterarStatusPorLote(List<Lote> lotes, ProcessoStatus processoStatus)
        {
            this.Session
              .CreateQuery("update Processo set Status = :processoStatus where Lote.Id in (:lotes)")
              .SetParameter("processoStatus", processoStatus)
              .SetParameterList("lotes", lotes.Select(x => x.Id).ToArray())
              .ExecuteUpdate();
        }

        public IList<Processo> ObterPorPacoteComStatusFinalizado(int id)
        {
            return this.Session.QueryOver<Processo>()
                .Fetch(x => x.TipoDeProcesso).Eager
                .Where(x => x.Status != ProcessoStatus.Excluido)
                .JoinQueryOver(x => x.Lote)
                .JoinQueryOver(x => x.Pacote)
                .Where(x => x.Id == id)
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
        }

        public int ObterTotaldeLotesFinalizadosPorPacoteProcessado(int id)
        {
            return this.Session.QueryOver<Processo>()
                .JoinQueryOver(x => x.Lote)
                .Where(x => x.Status == LoteStatus.Finalizado || x.Status == LoteStatus.Erro) 
                .JoinQueryOver(x => x.PacoteProcessado)
                .Where(x => x.Id == id)
                .RowCount();
        }

        public int ObterTotaldeLotesFinalizadosEComErroPorPacoteProcessado(int id)
        {
            throw new NotImplementedException();
        }

        public bool EstaAguardandoAprovacao(int processoId)
        {
            return this.Session.QueryOver<Processo>()
                .Where(x => x.Id == processoId)
                .And(x => x.Status == ProcessoStatus.AguardandoAprovacao || 
                    x.Status == ProcessoStatus.AguardandoAprovacaoEspecial)
                .RowCount() > 0;
        }

        public void AtualizarHoraInicio(int processoId)
        {
            this.Session
              .CreateQuery("update Processo set HoraInicio = current_date() where Id = :id")
              .SetInt32("id", processoId)
              .ExecuteUpdate();
        }

        public void AlterarStatus(int id, ProcessoStatus status)
        {
            this.Session
              .CreateQuery("update Processo set Status = :status where Id = :id")
              .SetParameter("id", id)
              .SetParameter("status", status)
              .ExecuteUpdate();
        }

        public void AlterarDecisao(int id, ProcessoDecisao decisao)
        {
            this.Session
              .CreateQuery("update Processo set Decisao = :decisao where Id = :id")
              .SetParameter("id", id)
              .SetParameter("decisao", decisao)
              .ExecuteUpdate();
        }

        public int AlterarStatusAposRetornoPorAgenciaConta(string agencia, string conta, ProcessoStatus processoStatus)
        {
            return this.Session
              .CreateQuery("update Processo set Status = :status where Agencia = :agencia and Conta = :conta and (Status = :statusRetorno or Status = :statusErroTerc)")
              .SetParameter("agencia", agencia)
              .SetParameter("conta", conta)
              .SetParameter("status", processoStatus)
              .SetParameter("statusRetorno", ProcessoStatus.AguardandoRetorno)
              .SetParameter("statusErroTerc", ProcessoStatus.SetaErro)
              .ExecuteUpdate();
        }

        public Processo ObterProcessoFinalizadoOuErroTerceira(string agencia, string conta)
        {
            return this.Session.QueryOver<Processo>()
                .Where(x => x.Status == ProcessoStatus.Finalizado  || x.Status == ProcessoStatus.Erro)
                .Where(x => x.Agencia == agencia)
                .Where(x => x.Conta == conta)
                .Take(1)
                .SingleOrDefault();
        }

        public Processo ObterPorIdentificacao(string identificacao)
        {
            return this.Session.QueryOver<Processo>()
                .Fetch(x => x.Lote).Eager
                .Fetch(x => x.Lote.Pacote).Eager
                .Where(x => x.Identificacao == identificacao)
                .Take(1)
                .SingleOrDefault();
        }
        
        public void GravarFinalizado(int processoId, ProcessoStatus status)
        {
            this.Session
              .CreateQuery("update Processo set Status = :status where Id = :id")
              .SetParameter("id", processoId)
              .SetParameter("status", ProcessoStatus.Finalizado)
              .ExecuteUpdate();
        }

        public Processo ObterUltimoPorIdentificacao(string identificacao)
        {
            return this.Session.QueryOver<Processo>()
                .Inner.JoinQueryOver<Lote>(x => x.Lote)
                .Where(x => x.Identificacao == identificacao)
                .Fetch(x => x.Lote).Eager
                .Fetch(x => x.Documentos).Eager
                .Fetch(x => x.Documentos.First().TipoDocumento).Eager
                .Fetch(x => x.Documentos.First().Paginas).Eager
                .Fetch(x => x.Documentos.First().Indexacao).Eager
                .Fetch(x => x.Documentos.First().Indexacao.First().Campo).Eager
                .OrderBy(x => x.Id).Desc
                .Take(1)
                .SingleOrDefault();
        }

        public Processo ObterProcessoParaExportacao(int processoId)
        {
            return this.Session.QueryOver<Processo>()
                .Fetch(processo => processo.Lote).Eager
                .Fetch(processo => processo.UsuarioResponsavel).Eager
                .Fetch(processo => processo.Documentos).Eager
                .Fetch(processo => processo.Documentos.First().Paginas.First()).Eager
                .Fetch(processo => processo.Documentos.First().Indexacao).Eager
                .Fetch(processo => processo.Documentos.First().Indexacao.First().Campo).Eager
                .Fetch(processo => processo.RegrasVioladas).Eager
                .Fetch(processo => processo.RegrasVioladas.First().Regra).Eager
                .Where(x => x.Id == processoId)
                .TransformUsing(Transformers.DistinctRootEntity)
                .SingleOrDefault();
        }

        public IList<int> ObterIdsParaExportacao()
        {
            return this.Session.QueryOver<Processo>()
                .Where(x => x.Status == ProcessoStatus.AguardandoExportacao)
                .Select(x => x.Id)
                .OrderBy(x => x.Id).Asc
                .Take(100)
                .List<int>();
        }

        public void MarcarProcessoComoExportado(int processoId)
        {
            this.Session
             .CreateQuery("update Processo set Status = :status where Id = :id")
             .SetParameter("id", processoId)
             .SetParameter("status", ProcessoStatus.ExportacaoRealizada)
             .ExecuteUpdate();
        }

        public Processo ObterPorLoteId(int loteId)
        {
            return this.Session.QueryOver<Processo>()
                .Where(x => x.Lote.Id == loteId)
                .SingleOrDefault();
        }

        public IList<Processo> ObterProcessosParaClassifier()
        {
            return this.Session.QueryOver<Processo>()
                .Where(x => x.AcaoClassifier == 0)
                .Fetch(x => x.Lote).Eager
                .Fetch(x => x.Documentos).Eager
                .Fetch(x => x.Documentos.First().TipoDocumento).Eager
                .Fetch(x => x.Documentos.First().Paginas).Eager
                .Fetch(x => x.TipoDeProcesso).Eager
                .JoinQueryOver<Lote>(x => x.Lote)
                .Where(x => x.Status == LoteStatus.AguardandoClassifier)
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
        }

        public IList<Processo> ObterProcessosParaClassifierPorProcessoId(int processoId)
        {
            return this.Session.QueryOver<Processo>()
                .Where(x => x.Id == processoId)
                .Fetch(x => x.Lote).Eager
                .Fetch(x => x.Documentos).Eager
                .Fetch(x => x.Documentos.First().TipoDocumento).Eager
                .Fetch(x => x.Documentos.First().Paginas).Eager
                .Fetch(x => x.TipoDeProcesso).Eager
                .JoinQueryOver<Lote>(x => x.Lote)
                ////.Where(x => x.Status == LoteStatus.AguardandoClassifier)
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
        }

        public Processo ObterProcessoPorIdComPaginas(int processoId)
        {
            return this.Session.QueryOver<Processo>()
                .Where(x => x.Id == processoId)
                .Fetch(x => x.Lote).Eager
                .Fetch(x => x.Documentos).Eager
                .Fetch(x => x.Documentos.First().TipoDocumento).Eager
                .Fetch(x => x.Documentos.First().Paginas).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .OrderBy(x => x.Id).Asc
                .SingleOrDefault();
        }

        public void SalvarProcessamentoDoClassifier(int processoId, int acaoClassifier)
        {
            this.Session
             .CreateQuery("update Processo set AcaoClassifier = :acaoClassifier where Id = :id")
             .SetParameter("acaoClassifier", acaoClassifier)
             .SetParameter("id", processoId)
             .ExecuteUpdate();
        }
        
        public IList<Processo> Pesquisar(PesquisaDossieViewModel filtros)
        {
            Processo processo = null;
            Lote lote = null;
            Pacote pacote = null;
            PacoteProcessado pacoteProcessado = null;
            DossieEsperado dossieEsperado = null;
            Ocorrencia ultimaOcorrencia = null;

            var query = this.Session.QueryOver<Processo>(() => processo)
                .JoinAlias(() => processo.Lote, () => lote)
                .JoinAlias(() => lote.Pacote, () => pacote)
                .Left.JoinAlias(() => lote.PacoteProcessado, () => pacoteProcessado)
                .JoinAlias(() => lote.DossieEsperado, () => dossieEsperado)
                .Fetch(x => x.Lote).Eager
                .Fetch(x => x.Lote.Pacote).Eager
                .Fetch(x => x.Lote.PacoteProcessado).Eager
                .Fetch(x => x.Lote.DossieEsperado).Eager
                .Fetch(x => x.Lote.LoteCef).Eager
                .Left.JoinAlias(() => lote.DossieEsperado.UltimaOcorrencia, () => ultimaOcorrencia)
                .Fetch(x => x.Lote.DossieEsperado.UltimaOcorrencia).Eager;

            ////if (string.IsNullOrEmpty(filtros.DataInicio) == false)
            ////{
            ////    query.Where(() => lote.DataCriacao >= filtros.ObjetoDataInicio());
            ////}

            ////if (string.IsNullOrEmpty(filtros.DataFim) == false)
            ////{
            ////    query.Where(() => lote.DataCriacao < filtros.ObjetoDataFim().AddDays(1));
            ////}

            ////if (string.IsNullOrEmpty(filtros.DataInicioMovimento) == false)
            ////{
            ////    query.Where(() => pacoteProcessado.ArquivoRecebidoEm >= filtros.ObjetoDataInicioMovimento());
            ////}

            ////if (string.IsNullOrEmpty(filtros.DataFimMovimento) == false)
            ////{
            ////    query.Where(() => pacoteProcessado.ArquivoRecebidoEm < filtros.ObjetoDataFimMovimento().AddDays(1));
            ////}

            if (string.IsNullOrEmpty(filtros.Identificacao) == false)
            {
                query.WhereRestrictionOn(() => lote.Identificacao).IsLike(".%" + filtros.Identificacao + "%-", MatchMode.Anywhere);
            }

            if (string.IsNullOrEmpty(filtros.IdentificacaoCompleta) == false)
            {
                query.Where(() => processo.Identificacao == filtros.IdentificacaoCompleta);
            }

            if (filtros.LoteId > 0)
            {
                query.Where(() => processo.Lote.Id == filtros.LoteId);
            }

            if (filtros.ProcessoId > 0)
            {
                query.Where(() => processo.Id == filtros.ProcessoId);
            }

            if (string.IsNullOrEmpty(filtros.FolderCompleto) == false)
            {
                query.Where(() => dossieEsperado.CodigoDeBarras == filtros.FolderCompleto);
            }

            if (string.IsNullOrEmpty(filtros.Agente) == false)
            {
                query.WhereRestrictionOn(x => x.Identificacao).IsLike(filtros.Agente + "%.", MatchMode.Anywhere);
            }

            if (string.IsNullOrEmpty(filtros.Contrato) == false)
            {
                query.WhereRestrictionOn(() => processo.Identificacao).IsInsensitiveLike(filtros.Contrato.ToUpper(), MatchMode.Anywhere);
            }

            if (filtros.TipoProcessoId > 0)
            {
                query.Where(() => processo.TipoDeProcesso.Id == filtros.TipoProcessoId);
            }

            if (string.IsNullOrEmpty(filtros.Caixa) == false)
            {
                query.WhereRestrictionOn(() => pacote.Identificacao).IsLike("%" + filtros.Caixa.ToUpper() + "%", MatchMode.Anywhere);
            }

            if (string.IsNullOrEmpty(filtros.Folder) == false)
            {
                query.WhereRestrictionOn(() => dossieEsperado.CodigoDeBarras).IsLike("%" + filtros.Folder.ToUpper() + "%", MatchMode.Anywhere);
            }

            if (filtros.ColetaId > 0)
            {
                query.Where(() => pacote.Coleta.Id == filtros.ColetaId);
            }

            if (filtros.LotecefId > 0)
            {
                query.Where(() => lote.LoteCef.Id == filtros.LotecefId);
            }
            
            if (string.IsNullOrEmpty(filtros.Fase) == false)
            {
                query.Where(() => lote.Status == LoteStatus.ObterPorSigla(filtros.Fase));
            }

            ////if (filtros.UltimoLoteId > 0)
            ////{
            ////    if (filtros.TipoDeOrdenacao == "A")
            ////    {   
            ////        query.Where(() => lote.Id > filtros.UltimoLoteId);
            ////    }
            ////    else
            ////    {
            ////        query.Where(() => lote.Id < filtros.UltimoLoteId);
            ////    }
            ////}

            query.Where(() => lote.Status != LoteStatus.Excluido && lote.Status != LoteStatus.Erro);

            switch (filtros.ColunaDeOrdenacao)
            {
                case "tipo":
                    if (filtros.TipoDeOrdenacao == "A")
                    {
                        query.OrderBy(() => processo.TipoDeProcesso.Id).Asc();
                    }
                    else
                    {
                        query.OrderBy(() => processo.TipoDeProcesso.Id).Desc();
                    }

                    break;

                case "identificacao":
                    if (filtros.TipoDeOrdenacao == "A")
                    {
                        query.OrderBy(() => processo.Identificacao).Asc();
                    }
                    else
                    {
                        query.OrderBy(() => processo.Identificacao).Desc();
                    }

                    break;

                case "caixa":
                    if (filtros.TipoDeOrdenacao == "A")
                    {
                        query.OrderBy(() => pacote.Identificacao).Asc();
                    }
                    else
                    {
                        query.OrderBy(() => pacote.Identificacao).Desc();
                    }

                    break;

                case "data":
                    if (filtros.TipoDeOrdenacao == "A")
                    {
                        query.OrderBy(() => lote.DataCriacao).Asc();
                    }
                    else
                    {
                        query.OrderBy(() => lote.DataCriacao).Desc();
                    }

                    break;

                case "dataMovimento":
                    if (filtros.TipoDeOrdenacao == "A")
                    {
                        query.OrderBy(() => pacoteProcessado.ArquivoRecebidoEm).Asc();
                    }
                    else
                    {
                        query.OrderBy(() => pacoteProcessado.ArquivoRecebidoEm).Desc();
                    }

                    break;

                case "status":
                    if (filtros.TipoDeOrdenacao == "A")
                    {
                        query.OrderBy(() => lote.Status).Asc().ThenBy(() => processo.Status).Asc();
                    }
                    else
                    {
                        query.OrderBy(() => lote.Status).Desc().ThenBy(() => processo.Status).Desc();
                    }

                    break;

                default:
                    if (filtros.TipoDeOrdenacao == "A")
                    {
                        query.OrderBy(() => lote.Id).Asc();
                    }
                    else
                    {
                        query.OrderBy(() => lote.Id).Desc();
                    }

                    break;
            }

            return query
                .TransformUsing(Transformers.DistinctRootEntity)
                .Paginado(filtros.PaginaId, 30)
                ////.Take(30)
                .List();
        }

        public Processo ObterDetalheDossie(int processoId)
        {
            return this.Session.QueryOver<Processo>()
                .Where(x => x.Id == processoId)
                .Fetch(x => x.Lote).Eager
                .Fetch(x => x.Lote.Pacote).Eager
                .Fetch(x => x.Documentos).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .SingleOrDefault();
        }

        public Processo ObterProcessoParaProcessarNoWorkFlow(int processoId)
        {
            return this.Session.QueryOver<Processo>()
                .Where(x => x.Id == processoId)
                .Fetch(x => x.Lote).Eager
                .Fetch(x => x.Documentos).Eager
                .Fetch(x => x.Documentos.First().TipoDocumento).Eager
                .Fetch(x => x.Documentos.First().Paginas).Eager
                .Fetch(x => x.Documentos.First().Indexacao).Eager
                .Fetch(x => x.Documentos.First().Indexacao.First().Campo).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .SingleOrDefault();
        }

        public Lote ObterLotePorProcessoId(int processoId)
        {
            return this.Session.QueryOver<Processo>()
                .Where(x => x.Id == processoId)
                .Select(x => x.Lote)
                .Take(1)
                .SingleOrDefault<Lote>();
        }

        public Processo ObterPorAgenciaContaEPacote(string agencia, string numeroDaConta, int pacoteProcessadoId)
        {
            return this.Session.QueryOver<Processo>()
                .Where(x => x.Agencia == agencia)
                .Where(x => x.Conta == numeroDaConta)
                .Inner.JoinQueryOver<Lote>(x => x.Lote)
                .Where(x => x.PacoteProcessado.Id == pacoteProcessadoId)
                .Take(1)
                .SingleOrDefault();
        }

        public IList<Processo> ObterParaControleQualidade(int usuarioResponsavelId, ProcessoStatus status)
        {
            return this.Session.QueryOver<Processo>()
                .Where(x => x.Status == status)
                .Where(x => x.UsuarioResponsavel.Id == usuarioResponsavelId)
                .Fetch(x => x.Lote).Eager
                .Fetch(x => x.Lote.PacoteProcessado).Eager
                .Fetch(x => x.TipoDeProcesso).Eager
                .Fetch(x => x.Documentos.First()).Eager
                .Fetch(x => x.Documentos.First().TipoDocumento).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .List<Processo>();
        }

        public IList<Processo> ObterParaAjustes(int usuarioResponsavelId)
        {
            return this.Session.QueryOver<Processo>()
                .Where(x => x.UsuarioResponsavel.Id == usuarioResponsavelId)
                .Fetch(x => x.Lote).Eager
                .Fetch(x => x.TipoDeProcesso).Eager
                .JoinQueryOver(x => x.Lote)
                .Where(x => x.Status == LoteStatus.AguardandoAjustes)
                .TransformUsing(Transformers.DistinctRootEntity)
                .List<Processo>();
        }

        public Processo ObterComPacoteProcessado(int processoId)
        {
            return this.Session.QueryOver<Processo>()
                .Where(x => x.Id == processoId)
                .Fetch(x => x.Lote).Eager
                .Fetch(x => x.Lote.PacoteProcessado).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .List()
                .FirstOrDefault();
        }

        public Processo ObterComPacote(int processsoId)
        {
            return this.Session.QueryOver<Processo>()
                .Where(x => x.Id == processsoId)
                .Fetch(x => x.Documentos).Eager
                .Fetch(x => x.Documentos.First().TipoDocumento).Eager
                .Fetch(x => x.Lote).Eager
                .Fetch(x => x.Lote.Pacote).Eager
                .Fetch(x => x.Lote.PacoteProcessado).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .SingleOrDefault();
        }

        public void AtualizarQuantidadeDePaginas(int processoId, int count)
        {
            this.Session
             .CreateQuery("update Processo set QtdePaginas = :qtde where Id = :id")
             .SetParameter("qtde", count)
             .SetParameter("id", processoId)
             .ExecuteUpdate();
        }

        public Processo ObterPorIdComTipoDeProcesso(int processoId)
        {
            return this.Session.QueryOver<Processo>()
               .Where(x => x.Id == processoId)
               .Fetch(x => x.TipoDeProcesso).Eager
               .TransformUsing(Transformers.DistinctRootEntity)
               .SingleOrDefault();
        }

        public IList<Processo> ObterPendentesDeRecaptura(string caixa, string barcode)
        {
            Processo processo = null;
            Lote lote = null;
            Pacote pacote = null;

            var query = this.Session.QueryOver<Processo>(() => processo)
                .JoinAlias(() => processo.Lote, () => lote)
                .JoinAlias(() => lote.Pacote, () => pacote)
                .Fetch(x => x.Lote).Eager
                .Fetch(x => x.Lote.Pacote).Eager;

            ////Processo processo = null;
            ////Pacote pacote = null;
            ////Lote lote = null;

            ////var query = this.Session.QueryOver<Processo>(() => processo)
            ////    .JoinAlias(() => processo.Lote, () => lote)
            ////    .JoinAlias(() => lote.Pacote, () => pacote)
            ////    .Fetch(x => x.TipoDeProcesso).Eager
            ////    .Fetch(x => x.Lote).Eager
            ////    .Fetch(x => x.Lote.Pacote).Eager;

            if (string.IsNullOrEmpty(caixa) == false)
            {
                query.WhereRestrictionOn(() => pacote.Identificacao).IsLike("%" + caixa.ToUpper() + "%", MatchMode.Anywhere);
            }

            if (string.IsNullOrEmpty(barcode) == false)
            {
                query.WhereRestrictionOn(x => x.Barcode).IsLike("%" + barcode + "%", MatchMode.Anywhere);
            }

            return query
                .Where(() => lote.Status == LoteStatus.AguardandoRecaptura)
                .TransformUsing(Transformers.DistinctRootEntity)
                .OrderBy(x => x.Id).Asc
                .List();
            ////query.JoinQueryOver(x => x.Lote)
            ////    .Where(x => x.Status == LoteStatus.AguardandoRecaptura);

            ////return query
            ////   .TransformUsing(Transformers.DistinctRootEntity)
            ////   .OrderBy(x => x.Id).Asc
            ////   .List();
        }

        public void AlterarStatus(int processoId, ProcessoStatus novoStatus, ProcessoStatus statusAtual)
        {
            this.Session
             .CreateQuery("update Processo set Status = :novoStatus where Id = :id and Status = :statusAtual")
             .SetParameter("id", processoId)
             .SetParameter("novoStatus", novoStatus)
             .SetParameter("statusAtual", statusAtual)
             .ExecuteUpdate();
        }

        public IList<Processo> ObterPorLoteComDocumentos(int loteId)
        {
            return this.Session.QueryOver<Processo>()
               .Fetch(x => x.Documentos).Eager
               .JoinQueryOver(x => x.Lote)
               .Where(x => x.Id == loteId)
               .TransformUsing(Transformers.DistinctRootEntity)
               .List();
        }

        public void AlterarTipoDossie(int processoId, string tipoDossie)
        {
            this.Session
             .CreateQuery("update Processo set TipoDeProcesso.Id = :tipoDossie where Id = :id")
             .SetParameter("tipoDossie", tipoDossie)
             .SetParameter("id", processoId)
             .ExecuteUpdate();
        }

        public void AlterarIdentificacao(int processoId, string identificacao, string barcode)
        {
            this.Session
             .CreateQuery("update Processo set Identificacao = :identificacao, Barcode = :barcode where Id = :id")
             .SetParameter("identificacao", identificacao)
             .SetParameter("barcode", barcode)
             .SetParameter("id", processoId)
             .ExecuteUpdate();
        }

        public void AlterarDossie(int processoId, int tipoProcessoId, string identificacao)
        {
            this.Session
             .CreateQuery("update Processo set Identificacao = :identificacao, TipoDeProcesso.Id = :tipoProcessoId where Id = :id")
             .SetParameter("identificacao", identificacao)
             .SetParameter("tipoProcessoId", tipoProcessoId)
             .SetParameter("id", processoId)
             .ExecuteUpdate();
        }

        public void EnviarParaQualidadeCef(int loteId)
        {
            this.Session
             .CreateQuery("update Processo set Status = :status, HoraInicio = null where Lote.Id = :loteId")
             .SetParameter("status", ProcessoStatus.AguardandoControleQualidadeCef)
             .SetParameter("loteId", loteId)
             .ExecuteUpdate();
        }

        public IList<Processo> ObterPendentesDeCaptura(string caixa, string identificacao)
        {
            Processo processo = null;
            Lote lote = null;
            Pacote pacote = null;

            var query = this.Session.QueryOver<Processo>(() => processo)
                .JoinAlias(() => processo.Lote, () => lote)
                .JoinAlias(() => lote.Pacote, () => pacote)
                .Fetch(x => x.Lote).Eager
                .Fetch(x => x.Lote.Pacote).Eager;

            if (string.IsNullOrEmpty(caixa) == false)
            {
                query.WhereRestrictionOn(() => pacote.Identificacao).IsLike("%" + caixa.ToUpper() + "%", MatchMode.Anywhere);
            }

            if (string.IsNullOrEmpty(identificacao) == false)
            {
                query.WhereRestrictionOn(() => processo.Barcode).IsLike("%" + identificacao + "%", MatchMode.Anywhere);
            }
            
            return query    
                .Where(() => lote.Status == LoteStatus.EmCaptura)
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
        }

        public Processo ObterComLote(int processoId)
        {
            return this.Session.QueryOver<Processo>()
                .Where(x => x.Id == processoId)
               .Fetch(x => x.Lote).Eager
               .JoinQueryOver(x => x.Lote)
               .TransformUsing(Transformers.DistinctRootEntity)
               .SingleOrDefault();
        }

        public void SetarRetirarDaFila(int loteId, DateTime? data, int resultadoFila)
        {
            this.Session
              .CreateQuery("update Processo set HoraInicio = :data, UsuarioResponsavel = null, AcaoClassifier = :resultadoFila where Lote.Id = :loteId")
              .SetParameter("data", data)
              .SetParameter("resultadoFila", resultadoFila)
              .SetParameter("loteId", loteId)
              .ExecuteUpdate();
        }        
    }
}
