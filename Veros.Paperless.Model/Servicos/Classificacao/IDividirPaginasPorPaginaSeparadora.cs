namespace Veros.Paperless.Model.Servicos.Classificacao
{
    using System.Collections.Generic;
    using Entidades;

    public interface IDividirPaginasPorPaginaSeparadora
    {
        /// <summary>
        /// Divide paginas em um array multidimensional com base no atributo Pagina.Separadora == true
        /// </summary>
        /// <param name="itensProcessados">Pagina com informações de separadora e pagina</param>
        /// <returns>
        /// no primeiro array o represenatne de documentos
        /// no segundo array as paginas de cada documento</returns>
        List<List<Pagina>> Executar(IList<ItemParaSeparacao> itensProcessados);
    }
}