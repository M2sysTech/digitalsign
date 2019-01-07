namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using NHibernate.Util;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class RegraVioladaRepositorio : Repositorio<RegraViolada>, IRegraVioladaRepositorio
    {
        public IList<RegraViolada> ObterRegrasVioladasParaAprovacao(int processoId)
        {
            return this.Session.QueryOver<RegraViolada>()
                .Where(x => x.Processo.Id == processoId)
                .Inner.JoinQueryOver(x => x.Regra)
                .Where(x => x.Fase == Regra.FaseAprovacao || x.Fase == Regra.FaseFormalistica || x.Fase == null)
                .Where(x => x.Classificacao == Regra.ClassificacaoErro || x.Classificacao == null)
                .Fetch(x => x.Regra.Tratamentos.First()).Eager
                .Fetch(x => x.Documento).Eager
                .List();
        }

        public IList<RegraViolada> ObterRegrasVioladasParaDetalhe(int processoId)
        {
            return this.Session.QueryOver<RegraViolada>()
                 .Where(x => x.Processo.Id == processoId)
                 .Fetch(x => x.Regra).Eager
                 .Where(x => x.Status == RegraVioladaStatus.Marcada || x.Status == RegraVioladaStatus.Pendente)
                 .Fetch(x => x.Regra).Eager
                 .Fetch(x => x.Regra.Tratamentos.First()).Eager
                 .Fetch(x => x.Documento).Eager
                 .List(); 
        }

        public IList<RegraViolada> ObterPorProcesso(int processoId, int regraId)
        {
            return this.Session.QueryOver<RegraViolada>()
                 .Where(x => x.Processo.Id == processoId)
                 .Where(x => x.Regra.Id == regraId)
                 .Fetch(x => x.Regra).Eager
                 .List(); 
        }

        public IList<RegraViolada> ObterRegrasDeQualidade(int processoId)
        {
            return this.Session.QueryOver<RegraViolada>()
                 .Where(x => x.Processo.Id == processoId)
                 .Where(x => x.Regra.Id == Regra.CodigoRegraQualidadeM2 
                     || x.Regra.Id == Regra.CodigoRegraQualidadeCef 
                     || x.Regra.Id == Regra.CodigoRegraDocumentoComProblemaNaClassificacao)
                 .Where(x => x.Status != RegraVioladaStatus.Aprovada)
                 .Fetch(x => x.Regra).Eager
                 .List();
        }

        public void AlterarStatus(Processo processo, int regraId, RegraVioladaStatus statusAtual, RegraVioladaStatus statusNovo)
        {
            this.Session
              .CreateQuery(@"
update RegraViolada set Status = :statusNovo 
where Processo.Id = :processoId
and Status = :statusAtual
and Regra.Id = :regraId")
              .SetParameter("statusNovo", statusNovo)
              .SetParameter("processoId", processo.Id)
              .SetParameter("statusAtual", statusAtual)
              .SetParameter("regraId", regraId)
              .ExecuteUpdate();
        }

        public void AprovarRegrasMarcadas(int processoId)
        {
            this.Session.CreateQuery(@"
update RegraViolada set Status = :status where Processo.Id = :processoId")
              .SetParameter("status", RegraVioladaStatus.Aprovada)
              .SetParameter("processoId", processoId)
              .ExecuteUpdate();
        }

        public void ExcluirRegraPorLoteId(int loteId)
        {
            var processoAlvo = this.Session.QueryOver<Processo>()
                 .Where(x => x.Lote.Id == loteId)
                 .SingleOrDefault(); 

            if (processoAlvo == null)
            {
                return;
            }

            this.Session
              .CreateQuery("delete from RegraViolada where Processo.Id = :processoAlvo ")
              .SetInt32("processoAlvo", processoAlvo.Id)
              .ExecuteUpdate();
        }

        public IList<RegraViolada> ObterRegrasDeCapaOuTermo(int processoId)
        {
            RegraViolada regraviolada = null;
            Documento documento = null;

            return this.Session.QueryOver<RegraViolada>(() => regraviolada)
                 .Where(x => x.Processo.Id == processoId)
                 .Where(x => x.Status != RegraVioladaStatus.Aprovada)
                 .Fetch(x => x.Regra).Eager
                 .JoinQueryOver(() => regraviolada.Documento, () => documento)
                 .Where(x => x.TipoDocumento.Id == TipoDocumento.CodigoFolhaDeRosto || x.TipoDocumento.Id == TipoDocumento.CodigoTermoAutuacaoDossie)
                 .List();
        }

        public IList<RegraViolada> ObterRegraEmAbertoPorProcesso(int processoId, int regraId, int documentoId)
        {
            return this.Session.QueryOver<RegraViolada>()
                .Where(x => x.Processo.Id == processoId)
                .Where(x => x.Status == RegraVioladaStatus.Pendente || x.Status == RegraVioladaStatus.Marcada)
                .Where(x => x.Regra.Id == regraId)
                .Where(x => x.Documento.Id == documentoId)
                .List();
        }

        public void AlterarStatus(int id, RegraVioladaStatus status)
        {
            this.Session
              .CreateQuery("update RegraViolada set Status = :status where Id = :id")
              .SetInt32("id", id)
              .SetParameter("status", status)
              .ExecuteUpdate();
        }

        public IList<RegraViolada> ObterRegrasVioladasParaValidacaoPorProcesso(int processoId)
        {
            return this.Session.QueryOver<RegraViolada>()
                .Where(x => x.Processo.Id == processoId)
                .Where(x => x.Status == RegraVioladaStatus.Pendente)
                .Inner.JoinQueryOver(x => x.Regra)
                .Where(x => x.Fase == Regra.FaseValidacao)
                .List();            
        }

        public IList<RegraViolada> ObterRegrasVioladasParaValidacaoPorDocumento(int documentoId)
        {
            return this.Session.QueryOver<RegraViolada>()
                .Where(x => x.Documento.Id == documentoId)
                .Where(x => x.Status == RegraVioladaStatus.Pendente)
                .Inner.JoinQueryOver(x => x.Regra)
                .Where(x => x.Fase == Regra.FaseValidacao)
                .List();
        }

        public IList<RegraViolada> ObterRegrasVioladasParaExportacaoPorDocumento(int documentoId)
        {
            //// TODO: verificar se esse OU || funciona
            return this.Session.QueryOver<RegraViolada>()
                .Where(x => x.Documento.Id == documentoId)
                .Where(x => x.Status == RegraVioladaStatus.Marcada || x.Status == RegraVioladaStatus.Aprovada)
                .Inner.JoinQueryOver(x => x.Regra)
                .List();
        }

        public bool ExistePorProcessoERegraEStatus(int processoId, int regraId, string statusDaRegraViolada)
        {
            throw new System.NotImplementedException();
        }

        public int ObterTotalPorProcessoEStatusRegra(int regraId, int processoId, string statusRegra)
        {
            throw new System.NotImplementedException();
        }

        public IList<RegraViolada> ObterTodasPorProcesso(int processoId)
        {
            return this.Session.QueryOver<RegraViolada>()
                .JoinQueryOver(x => x.Processo)
                .Where(x => x.Id == processoId)
                .List();
        }

        public bool ExisteRegraDeFraude(int processoId)
        {
            return this.Session.QueryOver<RegraViolada>()
                .Where(x => x.Processo.Id == processoId)
                .And(x => x.Status == RegraVioladaStatus.Pendente)
                .Inner.JoinQueryOver(x => x.Regra)
                .Where(x => x.RegraDeFraude)
                .List().Count > 0;
        }

        public bool ExisteRegraDeDivergenciaNoProcesso(int processoId)
        {
            ////TODO: Verificar uma forma de detectar regras de divergência que não seja pelo Id
            return this.Session.QueryOver<RegraViolada>()
                .Where(x => x.Processo.Id == processoId)
                .Where(x => x.Status == RegraVioladaStatus.Marcada)
                .Where(x => x.Regra.Id == 204 || 
                    x.Regra.Id == 205 || 
                    x.Regra.Id == 2755 || 
                    x.Regra.Id == 2756 || 
                    x.Regra.Id == 2757 || 
                    x.Regra.Id == 2758)
                .RowCount() > 0;
        }

        public IList<RegraViolada> ObterRegrasDeFraude(int processoId)
        {
            return this.Session.QueryOver<RegraViolada>()
                .Where(x => x.Processo.Id == processoId)
                .And(x => x.Status == RegraVioladaStatus.Pendente)
                .Inner.JoinQueryOver(x => x.Regra)
                .Where(x => x.RegraDeFraude)
                .List();
        }

        public IList<RegraViolada> ObterRegrasVioladasParaExportacaoPorProcesso(int processoId)
        {
            //// TODO: verificar se esse OU || funciona
            return this.Session.QueryOver<RegraViolada>()
                .Where(x => x.Processo.Id == processoId)
                .Where(x => x.Status == RegraVioladaStatus.Marcada || x.Status == RegraVioladaStatus.Aprovada)
                .Inner.JoinQueryOver(x => x.Regra)
                .List();
        }

        public int ObterQuantidadeRegrasVioladasParaProvaZeroPorProcesso(int processoId)
        {
            return this.Session.QueryOver<RegraViolada>()
                .Where(x => x.Processo.Id == processoId)
                .Where(x => x.Status == RegraVioladaStatus.Pendente)
                .Inner.JoinQueryOver(x => x.Regra)
                .Where(x => x.Fase == Regra.FaseProvaZero)
                .RowCount();
        }

        public bool ExistePorProcessoERegraEStatus(int processoId, int regraId, RegraVioladaStatus status)
        {
            return this.Session.QueryOver<RegraViolada>()
                .Where(x => x.Processo.Id == processoId)
                .Where(x => x.Status == status)
                .Where(x => x.Regra.Id == regraId)
                .RowCount() > 0;
        }

        public int ObterTotalPorProcessoEStatusRegra(int regraId, int processoId, RegraVioladaStatus status)
        {
            return this.Session.QueryOver<RegraViolada>()
                .Where(x => x.Status == status)
                .And(x => x.Id == regraId)
                .JoinQueryOver(x => x.Processo)
                .Where(x => x.Id == processoId)
                .RowCount();
        }

        public bool ExisteRegraVioladaPorProcesso(int processoId)
        {
            return this.Session.QueryOver<RegraViolada>()
                .Where(x => x.Status == RegraVioladaStatus.Marcada || x.Status == RegraVioladaStatus.Aprovada)
                .And(x => x.Id == processoId)
                .RowCount() > 0;
        }

        public bool ExisteRegraVioladaSemTratamento(int processoId, string fase)
        {
            return this.Session.QueryOver<RegraViolada>()
                .Where(x => x.Status == RegraVioladaStatus.Pendente || x.Status == RegraVioladaStatus.EmTratamento)
                .And(x => x.Processo.Id == processoId)
                .JoinQueryOver(x => x.Regra)
                .And(x => x.Fase == fase)
                .RowCount() > 0;
        }

        public Regra ObterRegraPorId(int regraVioladaId)
        {
            return this.Session.QueryOver<RegraViolada>()
                .Where(x => x.Id == regraVioladaId)
                .Select(x => x.Regra) 
                .Take(1)
                .SingleOrDefault<Regra>();
        }

        public RegraViolada ObterRegraViloladaPorId(int regraVioladaId)
        {
            return this.Session.QueryOver<RegraViolada>()
                .Where(x => x.Id == regraVioladaId)
                .Take(1)
                .SingleOrDefault();
        }

        public void ExcluirRegraDoProcesso(int processoId, int regraId)
        {
            this.Session
              .CreateQuery("delete from RegraViolada where Processo.Id = :processoId and Regra.Id = :regraId")
              .SetInt32("processoId", processoId)
              .SetParameter("regraId", regraId)
              .ExecuteUpdate();
        }

        public bool ExisteRegraDeErroVioladaSemTratamento(int processoId, string faseDeRegra)
        {
            return this.Session.QueryOver<RegraViolada>()
                .Where(x => x.Status == RegraVioladaStatus.Pendente || x.Status == RegraVioladaStatus.EmTratamento)
                .And(x => x.Processo.Id == processoId)
                .JoinQueryOver(x => x.Regra)
                .And(x => x.Fase == faseDeRegra)
                .And(x => x.Classificacao == Regra.ClassificacaoErro)
                .RowCount() > 0;
        }

        public IList<RegraViolada> ObterVioladasParaExportacao(int processoId)
        {
            return this.Session.QueryOver<RegraViolada>()
                .Where(x => x.Status == RegraVioladaStatus.Pendente || x.Status == RegraVioladaStatus.Marcada)
                .And(x => x.Processo.Id == processoId)
                .Fetch(x => x.Regra).Eager
                .Inner.JoinQueryOver(x => x.Regra)
                .And(x => x.Fase == Regra.FaseAprovacao)
                .List();
        }

        public bool ExisteRegraPendenteDeRevisao(int processoId)
        {
            return this.Session.QueryOver<RegraViolada>()
                .Where(x => x.Revisada == false)
                .And(x => x.Processo.Id == processoId)
                .And(x => x.Status == RegraVioladaStatus.Marcada)
                .JoinQueryOver(x => x.Regra)
                .And(x => x.Fase == Regra.FaseFormalistica)
                .And(x => x.RegraDeRevisao == true)
                .RowCount() > 0;
        }

        public bool ExisteRegraFinalizacaoNeurotech(int processoId)
        {
            return this.Session.QueryOver<RegraViolada>()
                .Where(x => x.Processo.Id == processoId)
                .JoinQueryOver(x => x.Regra)
                .And(x => x.Identificador == "V001" || x.Identificador == "V002")
                .RowCount() > 0;
        }

        public bool ExisteRegraVioladaPorRegra(int processoId, int regraId)
        {
            return this.Session.QueryOver<RegraViolada>()
                .Where(x => x.Processo.Id == processoId)
                .And(x => x.Regra.Id == regraId)
                .RowCount() > 0;
        }
    }
}
