namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using System.Linq;
    using Data.Hibernate;
    using Model.Entidades;
    using Model.Repositorios;
    using NHibernate.Transform;

    public class RegraImportadaRepositorio : Repositorio<RegraImportada>, IRegraImportadaRepositorio
    {
        public IList<RegraImportada> ObterRegrasImportadasPorDocumento(int documentoId)
        {
            return this.Session.QueryOver<RegraImportada>()
                .Where(x => x.Documento.Id == documentoId)
                .List();
        }

        public IList<RegraImportada> ObterSomenteDesvinculadasPorProcesso(int processoId)
        {
            var vinculos = this.Session.QueryOver<Regra>()
                .Select(x => x.Vinculo)
                .Where(x => x.Vinculo != string.Empty)
                .TransformUsing(Transformers.DistinctRootEntity)
                .List<object[]>()
                .Select(x => (string) x[0])
                .ToList();

            return this.Session.QueryOver<RegraImportada>()
                .Where(x => x.Processo.Id == processoId)
                .WhereRestrictionOn(x => x.Vinculo).Not.IsIn(vinculos)
                .List();
        }

        public Documento ObterDocumentoPorVinculoEProcesso(string vinculo, int processoId)
        {
            return this.Session.QueryOver<RegraImportada>()
                .Where(x => x.Processo.Id == processoId)
                .Where(x => x.Vinculo == vinculo)
                .Select(x => x.Documento)
                .Take(1)
                .SingleOrDefault<Documento>();
        }
    }
}
