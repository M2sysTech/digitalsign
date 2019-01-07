namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Entidades;
    using Model.Repositorios;
                
    public class PalavraReconhecidaRepositorio : Repositorio<PalavraReconhecida>, IPalavraReconhecidaRepositorio
    {
        public IList<PalavraReconhecida> ObterPorDocumentoId(int documentoId)
        {
            return this.Session.QueryOver<PalavraReconhecida>()
               .OrderBy(x => x.Id).Asc
               .JoinQueryOver(x => x.Pagina)
               .Where(x => x.Documento.Id == documentoId)
               .List<PalavraReconhecida>();
        }

        public IList<PalavraReconhecida> ObterPorDocumentoIdParaMontarPdf(int documentoId)
        {
            return this.Session.QueryOver<PalavraReconhecida>()
               .OrderBy(x => x.Id).Asc
               .JoinQueryOver(x => x.Pagina)
               .Where(x => x.Documento.Id == documentoId)
               .Fetch(x => x.Pagina).Eager
               .OrderBy(x => x.Ordem).Asc
               .List<PalavraReconhecida>();
        }
    }
}