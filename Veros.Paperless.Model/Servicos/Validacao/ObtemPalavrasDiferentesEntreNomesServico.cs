namespace Veros.Paperless.Model.Servicos.Validacao
{
    using System.Collections.Generic;
    using Comparacao;
    using Framework;

    public class ObtemPalavrasDiferentesEntreNomesServico
    {
        private readonly ComparadorDeParecidos comparadorDeParecidos;

        public ObtemPalavrasDiferentesEntreNomesServico(ComparadorDeParecidos comparadorDeParecidos)
        {
            this.comparadorDeParecidos = comparadorDeParecidos;
        }

        public IList<string> Obter(string nome1, string nome2)
        {
            if (string.IsNullOrEmpty(nome1) || string.IsNullOrEmpty(nome2))
            {
                return null;
            }

            ////if (this.comparadorDeParecidos.SaoIguais(nome1, nome2) == false)
            ////{
            ////    return null;
            ////}

            nome1 = nome1
                .RemoverDiacritico()
                .RemoveStopWords()
                .RemoverPalavrasComDoisCaracteres()
                .RemoveAcentuacao()
                .Replace("\n", " ").Trim();

            nome2 = nome2
                .RemoverDiacritico()
                .RemoveStopWords()
                .RemoverPalavrasComDoisCaracteres()
                .RemoveAcentuacao()
                .Replace("\n", " ").Trim();

            if (nome1 == nome2)
            {
                return null;
            }

            var palavras1 = nome1.Split(' ');
            var palavras2 = nome2.Split(' ');

            if (this.QuantidadeDePalavrasDiferentes(palavras1, palavras2) != 1)
            {
                return null;
            }

            for (var i = 0; i < palavras1.Length; i++)
            {
                if (palavras1[i] != palavras2[i])
                {
                    return new List<string>
                    {
                        palavras1[i],
                        palavras2[i]
                    };
                }
            }

            return null;
        }

        private int QuantidadeDePalavrasDiferentes(string[] palavras1, string[] palavras2)
        {
            if (palavras1.Length != palavras2.Length)
            {
                return 0;
            }

            var quantidade = 0;

            for (var i = 0; i < palavras1.Length; i++)
            {
                if (palavras1[i] != palavras2[i])
                {
                    quantidade++;
                }
            }

            return quantidade;
        }
    }
}
