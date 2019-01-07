namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using Data.Hibernate;
    using NHibernate;
    using NHibernate.Criterion;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class TipoDocumentoRepositorio : Repositorio<TipoDocumento>, ITipoDocumentoRepositorio
    {
        public TipoDocumento ObterPorCodigo(int codigo)
        {
            return this.Session.QueryOver<TipoDocumento>()
                .Where(x => x.Id == codigo)
                .SingleOrDefault<TipoDocumento>();
        }

        public IList<TipoDocumento> ObterDocumentosNaoMestres()
        {
            //// TODO: criar TipoDocumento.NaoMestres e colocar essa lista de Tipos lá
            //// TODO: trocar vários wherenot por WhereNot.In(TipoDocumento.NaoMestres)
            return this.Session.QueryOver<TipoDocumento>()
                .WhereNot(x => x.Id == TipoDocumento.CodigoNaoIdentificado)
                .List();
        }

        public IList<TipoDocumento> ObterDocumentosMestres()
        {
            //// TODO: criar TipoDocumento.Mestres e colocar essa lista de Tipos lá
            //// TODO: trocar vários wherenot por WhereNot.In(TipoDocumento.Mestres)
            return this.Session.QueryOver<TipoDocumento>()
                .WhereNot(x => x.Id == TipoDocumento.CodigoCie)
                .WhereNot(x => x.Id == TipoDocumento.CodigoCnh)
                .WhereNot(x => x.Id == TipoDocumento.CodigoDocumentoGeral)
                .WhereNot(x => x.Id == TipoDocumento.CodigoNaoIdentificado)
                .WhereNot(x => x.Id == TipoDocumento.CodigoPassaporte)
                .WhereNot(x => x.Id == TipoDocumento.CodigoRg)
                .List();
        }

        public IList<TipoDocumento> ObterParaIdentificacao()
        {
            return this.Session.QueryOver<TipoDocumento>()
                .Where(x => x.Id != TipoDocumento.CodigoFichaDeCadastro &&
                    x.Id != TipoDocumento.CodigoFichaDeCadastro &&
                    x.Id != TipoDocumento.CodigoNaoIdentificado)
                .OrderBy(x => x.Description).Asc
                .List();
        }

        public TipoDocumento ObterPorTypeDoc(int tipoDocumento)
        {
            return this.Session.QueryOver<TipoDocumento>()
                .Where(x => x.TypeDocCode == tipoDocumento)
                .SingleOrDefault();
        }

        public int ObterUltimoTypedocCode()
        {
            const string Hql = @"
select 
    max(TypeDocCode)   
from 
    TipoDocumento";
            return this.Session.CreateQuery(Hql)
                .UniqueResult<int>();
        }

        public void ExcluirPorId(int id)
        {
            this.Session
              .CreateQuery(@"
delete from TipoDocumento where Id = :id")
              .SetInt32("id", id)
              .ExecuteUpdate();
        }

        public IList<TipoDocumento> ObterPorDescricao(string removeAcentuacao)
        {
            return this.Session.QueryOver<TipoDocumento>()
            .Where(Restrictions.Eq(
                Projections.SqlFunction("lower", NHibernateUtil.String,
                    Projections.Property<TipoDocumento>(x => x.Description)),
                removeAcentuacao))
            .List();
        }

        public IList<TipoDocumento> ObterGruposDosItensDocumentais()
        {
            return this.Session.QueryOver<TipoDocumento>()
                .Where(x => x.GrupoId == 0 || x.Id == x.GrupoId)
                .OrderBy(x => x.Description).Asc
                .List();
        }

        public IList<TipoDocumento> ObterPorRangeTypedoc(int inicial, int final)
        {
            return this.Session.QueryOver<TipoDocumento>()
                .Where(x => x.Id >= inicial && x.Id <= final)
                .OrderBy(x => x.Id).Asc
                .List();
        }
    }
}
