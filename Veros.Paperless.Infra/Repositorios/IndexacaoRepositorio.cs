namespace Veros.Paperless.Infra.Repositorios
{
    using System;
    using System.Collections.Generic;
    using NHibernate.Criterion;
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class IndexacaoRepositorio : Repositorio<Indexacao>, IIndexacaoRepositorio
    {
        public Indexacao ObterPorCampoDeUmDocumento(int campoId, Documento documento)
        {
            return this.Session.QueryOver<Indexacao>()
                .Where(x => x.Campo.Id == campoId)
                .And(x => x.Documento == documento)
                .SingleOrDefault<Indexacao>();
        }

        public IList<Indexacao> ObterTodosPorDocumentoComOsCampos(Documento documento)
        {
            return this.Session.QueryOver<Indexacao>()
                .Where(x => x.Documento == documento)
                .Fetch(x => x.Campo).Eager
                .List();
        }

        public void AlterarPrimeiroValor(int indexacaoId, string valor)
        {
            this.Session
              .CreateQuery(@"
update Indexacao set 
    UsuarioPrimeiroValor    = :usuario, 
    DataPrimeiraDigitacao   = :data, 
    PrimeiroValor           = :primeiroValor, 
    OcrComplementou         = :ocrComplementou
where Id = :id")
            .SetInt32("usuario", Usuario.CodigoDoUsuarioSistema)
            .SetDateTime("data", DateTime.Now)
            .SetInt32("id", indexacaoId)
            .SetString("primeiroValor", valor)
            .SetBoolean("ocrComplementou", true)
            .ExecuteUpdate();
        }

        public IList<Indexacao> ObterValidadosPorDataDeProducao(DateTime dataProducao)
        {
            Indexacao indexacao = null;
            Campo campo = null;
            Documento documento = null;
            Lote lote = null;

            return this.Session.QueryOver(() => indexacao)
                .JoinAlias(() => indexacao.Campo, () => campo)
                .JoinAlias(() => indexacao.Documento, () => documento)
                .JoinAlias(() => documento.Lote, () => lote)
                .Where(() => indexacao.ValorFinal != null)
                .Where(() => campo.ParaValidacao)
                .Where(() => lote.DataCriacao >= dataProducao)
                .And(() => lote.DataCriacao < dataProducao.AddDays(1))
                .List();
        }

        public IList<Indexacao> ObterPendentesDeDigitacaoPorDocumento(Documento documento)
        {
            //// TODO: refatorar: simplificar or http://www.methodicmadness.com/2012/04/extending-queryover-with-or.html
            return this.Session.QueryOver<Indexacao>()
                .Where(x => x.Documento == documento)
                .Where(Restrictions.On<Indexacao>(c => c.PrimeiroValor).IsNull)
                .JoinQueryOver(c => c.Campo)
                .Where(x => x.Digitavel)
                .List();
        }

        public bool ExisteValorPreenchidoParaCampoPorProcesso(int campoId, int processoId)
        {
            return this.Session.QueryOver<Indexacao>()
                .Where(x => x.Campo.Id == campoId)
                .And(x => x.PrimeiroValor == null)
                .JoinQueryOver(x => x.Documento)
                .JoinQueryOver(x => x.Processo)
                .Where(x => x.Id == processoId)
                .RowCount() > 0;
        }

        public bool ExistePorCampoEProcesso(int tipoCampoId, int processoId)
        {
            return this.Session.QueryOver<Indexacao>()
                .Where(x => x.Campo.Id == tipoCampoId)
                .JoinQueryOver(x => x.Documento)
                .JoinQueryOver(x => x.Processo)
                .Where(x => x.Id == processoId)
                .RowCount() > 0;
        }

        public IList<Indexacao> ObterPorCampoEProcesso(int tipoCampoId, int processoId)
        {
            return this.Session.QueryOver<Indexacao>()
                .Where(x => x.Campo.Id == tipoCampoId)
                .JoinQueryOver(x => x.Documento)
                .JoinQueryOver(x => x.Processo)
                .Where(x => x.Id == processoId)
                .Fetch(x => x.Campo).Eager
                .List();
        }

        public void AlterarCampo(int indexacaoId, int campoId)
        {
            this.Session
              .CreateQuery(@"
update Indexacao set 
    Campo.Id    = :campoId
where Id = :id")
            .SetParameter("campoId", campoId)
            .SetParameter("id", indexacaoId)
            .ExecuteUpdate();
        }

        public void AlterarValorFinal(int indexacaoId, string valorNovo)
        {
            this.Session.CreateQuery(@"
update Indexacao set
    ValorFinal = :valorFinal 
where Id = :id")
            .SetParameter("id", indexacaoId)
            .SetParameter("valorFinal", valorNovo)
            .ExecuteUpdate();
        }

        public IList<Indexacao> ObterTodosPorPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            return this.Session.QueryOver<Indexacao>()
                .JoinQueryOver(x => x.Documento)
                .JoinQueryOver(x => x.Lote)
                .Where(x => x.DataFimIcr > dataInicial)
                .And(x => x.DataFimIcr < dataFinal)
                .List();
        }

        public IList<Indexacao> ObterTodosValidadosPorPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            return this.Session.QueryOver<Indexacao>()
                .Where(x => x.EstaBatido())
                .JoinQueryOver(x => x.Documento)
                .JoinQueryOver(x => x.Lote)
                .Where(x => x.DataFimIcr > dataInicial)
                .And(x => x.DataFimIcr < dataFinal)
                .List();
        }

        public IList<Indexacao> ObterPorCampoPorPeriodo(Campo campo, DateTime dataInicial, DateTime dataFinal)
        {
            return this.Session.QueryOver<Indexacao>()
                .Where(x => x.Campo.Id == campo.Id)
                .JoinQueryOver(x => x.Documento)
                .JoinQueryOver(x => x.Lote)
                .Where(x => x.DataFimIcr > dataInicial)
                .And(x => x.DataFimIcr < dataFinal)
                .List();
        }

        public IList<Indexacao> ObterPorTipoCampoDeUmDocumento(int documentoId, TipoCampo tipoCampo)
        {
            return this.Session.QueryOver<Indexacao>()
                .Where(x => x.Documento.Id == documentoId)
                .JoinQueryOver(x => x.Campo)
                .Where(x => x.TipoCampo == tipoCampo)
                .List();
        }

        public IList<Indexacao> ObterPorReferenciaDeArquivo(int documentoId, string referenciaDeArquivo)
        {
            return this.Session.QueryOver<Indexacao>()
                .Where(x => x.Documento.Id == documentoId)
                .JoinQueryOver(x => x.Campo)
                .Where(x => x.ReferenciaArquivo == referenciaDeArquivo)
                .List();
        }

        public IList<Indexacao> ObterComMapeamentoPorDocumento(Documento documento)
        {
            return this.Session.QueryOver<Indexacao>()
                .OrderBy(x => x.Campo).Asc
                .Where(x => x.Documento.Id == documento.Id)
                .And(x => x.PrimeiroValor == null)
                .JoinQueryOver(x => x.Campo)
                .JoinQueryOver(x => x.MappedFields)
                .List();
        }

        public void SalvarNaCaptura(Indexacao indexacao)
        {
            var linhasAfetadas = this.Session
              .CreateQuery(@"
update Indexacao set 
    SegundoValor = :valor2
where Documento.Id = :documentoId
    and Campo.Id = :campoId")
            .SetParameter("valor2", indexacao.SegundoValor)
            .SetParameter("documentoId", indexacao.Documento.Id)
            .SetParameter("campoId", indexacao.Campo.Id)
            .ExecuteUpdate();

            if (linhasAfetadas < 1)
            {
                this.Salvar(indexacao);
            }
        }

        public IList<Indexacao> ObterPorProcesso(int processoId)
        {
            return this.Session.QueryOver<Indexacao>()
                .Fetch(x => x.Campo).Eager
                .Fetch(x => x.Documento).Eager
                .JoinQueryOver(x => x.Documento)
                .Where(x => x.Processo.Id == processoId)
                .List();
        }

        public void AlterarCampoPorDocumentoECampo(int documentoId, int campoIdOriginal, int campoIdAlterarPara)
        {
            this.Session
              .CreateQuery(@"
update Indexacao set 
    Campo.Id    = :campoIdAlterarPara
where Documento.Id = :documentoId
    and Campo.Id = :campoIdOriginal")
            .SetParameter("campoIdAlterarPara", campoIdAlterarPara)
            .SetParameter("documentoId", documentoId)
            .SetParameter("campoIdOriginal", campoIdOriginal)
            .ExecuteUpdate();
        }

        public void LimparValor1E2(int indexacaoId)
        {
            this.Session.CreateQuery(@"
update Indexacao set
    PrimeiroValor = null,
    SegundoValor = null
where Id = :id")
            .SetParameter("id", indexacaoId)
            .ExecuteUpdate();
        }

        public IList<Indexacao> ObterPorReferenciaArquivoDeUmDocumento(int loteId, string campoRefArquivo)
        {
            string hql = @"
select 
    index
from 
    Indexacao index 
inner join index.Campo campo
inner join index.Documento documento
inner join documento.Lote lote
where 
    campo.ReferenciaArquivo = :campoRefArquivo and
    lote.Id = :loteId";

            return this.Session.CreateQuery(hql)
                .SetParameter("campoRefArquivo", campoRefArquivo)
                .SetParameter("loteId", loteId)
                .List<Indexacao>();

            ////return this.Session.QueryOver<Indexacao>()
            ////.Fetch(x => x.Campo).Eager
            ////.Where(x => x.Campo.ReferenciaArquivo == campoRefArquivo)
            ////.JoinQueryOver(x => x.Documento)
            ////.Where(x => x.Lote.Id == loteId)
            ////.List();
        }

        public void SalvarValorFinalBatidoComOcr(int indexacaoId)
        {
            this.Session.CreateQuery(@"
update Indexacao set
    ValorFinalBateComOcr = true
where Id = :id")
            .SetParameter("id", indexacaoId)
            .ExecuteUpdate();
        }
    }
}