namespace Veros.Paperless.Model.Servicos.Validacao
{
    using System.Linq;
    using Batimento;
    using Entidades;

    public class VerificaSeNomeReconhecidoServico : IVerificaSeNomeReconhecidoServico
    {
        private readonly BatimentoFullText batimentoFullText;

        public VerificaSeNomeReconhecidoServico(BatimentoFullText batimentoFullText)
        {
            this.batimentoFullText = batimentoFullText;
        }

        public bool Verificar(string palavra, ImagemReconhecida imagemReconhecida)
        {
            var valoresReconhecidos = imagemReconhecida.ValoresReconhecidos;

            palavra = palavra.ToUpper() + " ";
            foreach (var valorReconhecido in valoresReconhecidos.Where(x => string.IsNullOrEmpty(x.Value) == false))
            {
                var textoReconhecido = valorReconhecido.Value.ToUpper() + " ";
                if (textoReconhecido.IndexOf(palavra, System.StringComparison.Ordinal) > 0)
                {
                    return true;
                }
            }

            return this.batimentoFullText.Batido(palavra, imagemReconhecida.Palavras);
        }
    }
}
