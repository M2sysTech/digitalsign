namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class RankingReclassificacaoRepositorio : Repositorio<RankingReclassificacao>, IRankingReclassificacaoRepositorio
    {
        public RankingReclassificacao ObterPorTipoDeDocumento(int tipoDeDocumentoId)
        {
          return this.Session.QueryOver<RankingReclassificacao>()
                .JoinQueryOver(x => x.TipoDocumento)
                .Where(x => x.TypeDocCode == tipoDeDocumentoId)
                .SingleOrDefault();
        }
    }
}
