namespace Veros.Paperless.Model.Servicos.Comparacao
{
    using System.Collections.Generic;
    using System.Globalization;

    public class PalavrasAbreviadas
    {
        private readonly string abreviation;
        private readonly Dictionary<string, List<string>> abreviationWords;

        public PalavrasAbreviadas(string abreviation)
        {
            this.abreviation = abreviation;
            this.abreviationWords = new Dictionary<string, List<string>>();

            this.abreviationWords.Add("parque", new List<string> { "parque", "prq", "pq", "p." });
            this.abreviationWords.Add("vila", new List<string> { "vila", "vl.", "vl", "v." });
            this.abreviationWords.Add("santa", new List<string> { "santa", "sta.", "sta", "st", "st." });
            this.abreviationWords.Add("santo", new List<string> { "santo", "sto.", "sto", "st", "st." });
            this.abreviationWords.Add("praça", new List<string> { "praça", "praca", "prc", "prc.", "pr", "pr.", "prc" });
            this.abreviationWords.Add("praca", new List<string> { "praça", "praca", "prc", "prc.", "pr", "pr.", "prc" });
            this.abreviationWords.Add("sao", new List<string> { "sao", "são", "s." });
            this.abreviationWords.Add("jardim", new List<string> { "jardim", "jd.", "jd" });
            this.abreviationWords.Add("apartamento", new List<string> { "apartamento", "apto.", "apto", "ap.", "ap" });
            this.abreviationWords.Add("bloco", new List<string> { "bloco", "bl.", "bl" });
            this.abreviationWords.Add("fundos", new List<string> { "fundos", "fds.", "fds", "fd.", "fd" });
        }

        public bool Contains(string state)
        {
            var find = this.abreviationWords.ContainsKey(this.abreviation);

            if (find == false)
            {
                return false;
            }

            for (var i = 0; i < this.abreviationWords[this.abreviation].Count; i++)
            {
                if (state.ToLower().Contains(this.abreviationWords[this.abreviation][i].ToString(CultureInfo.InvariantCulture)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}