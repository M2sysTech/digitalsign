namespace Veros.Paperless.Model.Servicos
{
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Framework;

    public class LocalizaPaginaPorNomeServico
    {
        public Pagina Executar(
            IList<Pagina> paginasValidas, 
            IList<Pagina> paginasNoDocumento27, 
            Pagina paginaNaoEhBranco)
        {
            Log.Application.InfoFormat(
                "Analisando ordem Pagina #{0} com nome [{1}]", 
                paginaNaoEhBranco.Id,
                paginaNaoEhBranco.NomeArquivoSemExtensao);

            paginasValidas.AddRange(paginasNoDocumento27);
            var todosOsJpegsJuntos = paginasValidas.OrderBy(x => x.NomeArquivoSemExtensao);

            foreach (var pagina in todosOsJpegsJuntos)
            {
                Log.Application.DebugFormat(
                "Jpegs Ordenados #{0} com nome [{1}], Branco: {2}, Separador {3}",
                pagina.Id,
                pagina.NomeArquivoSemExtensao,
                pagina.EmBranco,
                pagina.Separadora);
            }

            var paginas = new List<Pagina>();

            var paginasAgrupadasPorDocumento = new List<List<Pagina>>();

            foreach (var pagina in todosOsJpegsJuntos)
            {
                if (pagina.Separadora)
                {
                    paginasAgrupadasPorDocumento.Add(paginas);
                    paginas = new List<Pagina>();
                    continue;
                }

                paginas.Add(pagina);
            }

            if (paginas.Count > 0)
            {
                paginasAgrupadasPorDocumento.Add(paginas);
            }

            Log.Application.InfoFormat(
                "{0} grupos de documentos encontrados",
                paginasAgrupadasPorDocumento.Count());

            foreach (var paginasDoDocumento in paginasAgrupadasPorDocumento)
            {
                if (paginasDoDocumento.Any(x => x.NomeArquivoSemExtensao == paginaNaoEhBranco.NomeArquivoSemExtensao) == false)
                {
                    continue;
                }

                for (var i = 0; i < paginasDoDocumento.Count; i++)
                {
                    if (paginasDoDocumento.ElementAt(i).NomeArquivoSemExtensao != paginaNaoEhBranco.NomeArquivoSemExtensao)
                    {
                        continue;
                    }

                    return this.RetornarPaginaValida(paginasDoDocumento, paginasAgrupadasPorDocumento);
                }
            }

            return null;
        }

        private Pagina RetornarPaginaValida(
            List<Pagina> paginasDoDocumento, 
            List<List<Pagina>> paginasAgrupadasPorDocumento)
        {
            Log.Application.Debug("Inicio processamento pagina válida");

            var pagina = paginasDoDocumento.LastOrDefault(
                    x => x.EmBranco == false && 
                    x.Separadora == false && 
                    x.Documento.TipoDocumento.Id != TipoDocumento.CodigoDocumentoGeral);
            
            if (pagina != null)
            {
                Log.Application.ErrorFormat(
                    "encontrou pagina para {0} nome {1}", 
                    pagina.Id, pagina.NomeArquivoSemExtensao);

                return pagina;
            }

            Log.Application.Debug("Procurando no grupo de documentos...");

            var documentosAgrupados = paginasAgrupadasPorDocumento.ElementAt(0);

            Log.Application.DebugFormat("{0} documentos do grupo", documentosAgrupados.Count);

            foreach (var doc in documentosAgrupados)
            {
                Log.Application.DebugFormat(
                    "EmBranco::{0}; Separador::{1}; TipoDocId::{2}",
                    doc.EmBranco,
                    doc.Separadora,
                    doc.Documento.TipoDocumento.Id);
            }

            var ultimoDoPrimeiroGrupo = documentosAgrupados.LastOrDefault(
                    x => x.EmBranco == false && 
                    x.Separadora == false);

            Log.Application.ErrorFormat(
                "encontrou pagina para {0} nome:: {1}", 
                ultimoDoPrimeiroGrupo.Id, 
                ultimoDoPrimeiroGrupo.NomeArquivoSemExtensao);

            return ultimoDoPrimeiroGrupo;
        }
    }
}