namespace Veros.Paperless.Model.Servicos.Importacao
{
    using Veros.Paperless.Model.Entidades;
    using System.Collections.Generic;
    using System;

    public class ValorReconhecidoFabrica : IValorReconhecidoFabrica
    {
        public List<ValorReconhecido> Criar(Pagina pagina, List<Tuple<string, string>> campos)
        {
            var retornoLista = new List<ValorReconhecido>();
            foreach (var tupla in campos)
            {
                var valorAtual = new ValorReconhecido()
                {
                    Pagina = pagina,
                    CampoTemplate = tupla.Item1,
                    Value = tupla.Item2,
                    TemplateName = "ocr"
                };

                retornoLista.Add(valorAtual);
            }

            return retornoLista;
        }
    }
}
