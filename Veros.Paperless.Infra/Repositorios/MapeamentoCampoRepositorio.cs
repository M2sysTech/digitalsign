namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class MapeamentoCampoRepositorio : Repositorio<MapeamentoCampo>, IMapeamentoCampoRepositorio
    {
        public IList<MapeamentoCampo> ObterTodosExcetoPac()
        {
            return this.Session.QueryOver<MapeamentoCampo>()
                .JoinQueryOver<Campo>(x => x.Campo)
                .JoinQueryOver<TipoDocumento>(x => x.TipoDocumento)
                .Where(x => x.IsPac == false)
                .OrderBy(x => x.TypeDocCode).Asc                
                .List<MapeamentoCampo>();
        }

        public IList<MapeamentoCampo> ObterTodosComTipoDocumentoECampo()
        {
            return this.Session.QueryOver<MapeamentoCampo>()
                .OrderBy(x => x.NomeTemplate).Asc
                .JoinQueryOver(x => x.Campo)
                .JoinQueryOver(y => y.TipoDocumento)
                .OrderBy(y => y.Description).Asc
                .OrderBy(x => x.Description).Asc
                .List<MapeamentoCampo>();   
        }

        public bool JaExiste(MapeamentoCampo mapeamentoCampo)
        {
            return this.Session.QueryOver<MapeamentoCampo>()
                .Where(x => x.Campo == mapeamentoCampo.Campo)
                .Where(x => x.NomeCampoNoTemplate == mapeamentoCampo.NomeCampoNoTemplate)
                .Where(x => x.NomeTemplate == mapeamentoCampo.NomeTemplate)
                .List().Count > 0;
        }

        public bool ExisteMapeamentoParaCampo(int campoId)
        {
            return this.Session.QueryOver<MapeamentoCampo>()
                .Where(x => x.Campo.Id == campoId)
                .List().Count > 0;
        }
    }
}