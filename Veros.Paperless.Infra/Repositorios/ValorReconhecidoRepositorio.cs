namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using System.Linq;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class ValorReconhecidoRepositorio : Repositorio<ValorReconhecido>, IValorReconhecidoRepositorio
    {
        public IList<ValorReconhecido> ObtemPorDocumento(int documentoId)
        {
            return this.Session.QueryOver<ValorReconhecido>()
                .JoinQueryOver(x => x.Pagina)
                .Where(x => x.Documento.Id == documentoId)
                .List();
        }

        public ValorReconhecido ObtemPrimeiroPorPagina(int paginaId)
        {
            return this.Session.QueryOver<ValorReconhecido>()
                .Where(x => x.Pagina.Id == paginaId)
                .List()
                .FirstOrDefault();
        }
    }
}
