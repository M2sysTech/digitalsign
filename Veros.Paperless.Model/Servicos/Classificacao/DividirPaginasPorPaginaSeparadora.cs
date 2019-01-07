namespace Veros.Paperless.Model.Servicos.Classificacao
{
    using System.Collections.Generic;
    using Entidades;

    public class DividirPaginasPorPaginaSeparadora : IDividirPaginasPorPaginaSeparadora
    {
        /// <summary>
        /// Divide paginas em um array multidimensional com base no atributo Pagina.Separadora == true
        /// </summary>
        /// <param name="itensProcessados">Pagina com informações de separadora e pagina</param>
        /// <returns>
        /// no primeiro array o represenatne de documentos
        /// no segundo array as paginas de cada documento</returns>
        public List<List<Pagina>> Executar(IList<ItemParaSeparacao> itensProcessados)
        {
            var paginasDoGrupo = new List<Pagina>();
            var paginasAgrupadasPorDocumento = new List<List<Pagina>>();

            var i = 0;

            foreach (var item in itensProcessados)
            {
                var pagina = item.Pagina;

                if (pagina.Separadora || pagina.ContrapartidaDeSeparadora)
                {
                    i++;
                    pagina.Status = PaginaStatus.StatusExcluida;
                    paginasDoGrupo.Add(pagina);
                    
                    if (i == 2)
                    {
                        i = 0;
                        paginasAgrupadasPorDocumento.Add(paginasDoGrupo);
                        paginasDoGrupo = new List<Pagina>();
                    }

                    continue;
                }

                paginasDoGrupo.Add(pagina);
            }

            if (paginasDoGrupo.Count > 0)
            {
                paginasAgrupadasPorDocumento.Add(paginasDoGrupo);
            }

            return paginasAgrupadasPorDocumento;
        }
    }
}