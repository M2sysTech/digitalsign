namespace Veros.Paperless.Model.Servicos.Documentos
{
    using System.Collections.Generic;
    using System.Linq;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class ObtemGruposDeTiposDeDocumentosServico : IObtemGruposDeTiposDeDocumentosServico
    {        
        private readonly ITipoDocumentoRepositorio tipoDocumentoRepositorio;

        public ObtemGruposDeTiposDeDocumentosServico(
            ITipoDocumentoRepositorio tipoDocumentoRepositorio)
        {
            this.tipoDocumentoRepositorio = tipoDocumentoRepositorio;            
        }

        public IList<TipoDocumento> Obter()
        {
            var tipos = this.tipoDocumentoRepositorio.ObterTodos()
               .Where(x => TipoDocumento.CodigosBloqueadosNaClassificacao.Contains(x.Id) == false)
               .ToList();

            foreach (var tipo in tipos.Where(x => x.GrupoId > 0))
            {
                var grupo = tipos.FirstOrDefault(x => x.Id == tipo.GrupoId);

                if (grupo != null)
                {
                    tipo.GrupoDescricao = grupo.Description;
                }
            }

            return tipos.OrderBy(x => x.Description).ToList();
        }        
    }
}
