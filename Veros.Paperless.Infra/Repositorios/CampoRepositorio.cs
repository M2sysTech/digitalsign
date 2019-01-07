namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using System.Linq;
    using NHibernate.Transform;
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class CampoRepositorio : Repositorio<Campo>, ICampoRepositorio
    {
        public Campo ObterPorNome(string name)
        {
            return this.Session.QueryOver<Campo>()
                .Where(x => x.Description == name)
                .SingleOrDefault<Campo>();
        }

        public IList<Campo> ObterPorTipoDocumento(TipoDocumento tipoDocumento)
        {
            return this.Session.QueryOver<Campo>()
                .Where(x => x.TipoDocumento == tipoDocumento)
                .List<Campo>();
        }

        public Campo[] ObterMarcadosParaValidacaoPorCodigoTipoDocumento(params int[] ids)
        {
            throw new System.NotImplementedException();
        }

        public IList<Campo> ObterMarcadosParaValidacaoPorCodigoTipoDocumento(int codigoTipoDocumento)
        {
            return this.Session.QueryOver<Campo>()
                .Where(x => x.TipoDocumento.Id == codigoTipoDocumento)
                .Where(x => x.ParaValidacao)
                .List();
        }

        public Campo ObterPorReferenciaDeArquivo(TipoDocumento tipoDocumento, string referenciaArquivo)
        {
            return this.Session.QueryOver<Campo>()
                .Where(x => x.ReferenciaArquivo == referenciaArquivo)
                .Where(x => x.TipoDocumento.Id == tipoDocumento.Id)
                .Take(1)
                .SingleOrDefault<Campo>();
        }

        public IList<Campo> ObterCamposComGrupo()
        {
            return this.Session.QueryOver<Campo>()
                .Where(x => x.Grupo != null)
                .List();
        }

        public IList<Campo> ObterPorCodigoTipoDocumento(int codigo)
        {
            return this.Session.QueryOver<Campo>()
               .Where(x => x.TipoDocumento.Id == codigo)
               .List();
        }

        public IList<Campo> ObterPorTipoDocumentoComMapeamentoOcr(int tipoDocumentoId)
        {
            return this.Session.QueryOver<Campo>()
                .Where(x => x.TipoDocumento.Id == tipoDocumentoId)
                .OrderBy(x => x.Id).Asc
                .Fetch(x => x.MappedFields).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
        }

        public IList<Campo> ObterTodosReconheciveis()
        {
            return this.Session.QueryOver<Campo>()
                .Where(x => x.Reconhecivel)
                .List();
        }
    }
}
