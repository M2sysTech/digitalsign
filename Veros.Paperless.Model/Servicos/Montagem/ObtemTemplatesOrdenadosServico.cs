namespace Veros.Paperless.Model.Servicos.Montagem
{
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Framework;

    public class ObtemTemplatesOrdenadosServico : IObtemTemplatesOrdenadosServico
    {
        public string Obter(
            IList<ValorReconhecido> valoresReconhecidos,
            IEnumerable<Template> templatesDoDocumento)
        {
            if (templatesDoDocumento == null)
            {
                Log.Application.Info("Nenhum template encontrado para o documento");
                return string.Empty;
            }

            var totalDePalavrasEncontradasPorTemplate = new Dictionary<int, int>();

            Log.Application.Debug("Procurando palavras chave nos campos do documento");
            foreach (var template in templatesDoDocumento.OrderBy(x => x.Ordem))
            {
                totalDePalavrasEncontradasPorTemplate[template.Id] = this.TotalDeChavesEncontradas(
                    template,
                    valoresReconhecidos);
            }

            Log.Application.Debug("Ordenando templates pela quantidade de palavras encontradas no documento");
            return totalDePalavrasEncontradasPorTemplate.OrderByDescending(x => x.Value).Aggregate(string.Empty,
                (current,
                    item) => current + (item.Key + ";"));
        }
        
        private int TotalDeChavesEncontradas(Template template, IEnumerable<ValorReconhecido> valoresReconhecidos)
        {
            if (string.IsNullOrEmpty(template.Chaves) || valoresReconhecidos == null)
            {
                return 0;
            }

            return (from valorReconhecido in valoresReconhecidos 
                    from chave in template.Chaves.Split(',') 
                    where valorReconhecido.Value != null && valorReconhecido.Value.ToUpper().IndexOf(chave.ToUpper(), System.StringComparison.Ordinal) > 0 
                    select valorReconhecido).Count();
        }
    }
}