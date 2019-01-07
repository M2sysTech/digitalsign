namespace Veros.Paperless.Model.Entidades
{
    using System.Collections.Generic;
    using System.Globalization;

    public class EstadosBrasil
    {
        private readonly string uf;
        private readonly Dictionary<string, List<string>> stateWords;

        public EstadosBrasil(string uf)
        {
            this.uf = uf;
            this.stateWords = new Dictionary<string, List<string>>();

            this.stateWords.Add("sp", new List<string> { "sp", "sao paulo", "sao", "paulo" });
            this.stateWords.Add("rj", new List<string> { "rj", "rio de janeiro", "rio", "janeir" });
            this.stateWords.Add("mg", new List<string> { "mg", "minas gerais", "gerais", "minas" });
            this.stateWords.Add("df", new List<string> { "df", "distrito federal", "distrito", "federal" });
            this.stateWords.Add("go", new List<string> { "go", "goias" });
            this.stateWords.Add("ac", new List<string> { "ac", "acre" });
            this.stateWords.Add("am", new List<string> { "am", "amazonas" });
            this.stateWords.Add("rr", new List<string> { "rr", "roraima" });
            this.stateWords.Add("ro", new List<string> { "ro", "rondonia" });
            this.stateWords.Add("al", new List<string> { "al", "alagoas" });
            this.stateWords.Add("ap", new List<string> { "ap", "amapa" });
            this.stateWords.Add("ba", new List<string> { "ba", "bahia" });
            this.stateWords.Add("ce", new List<string> { "ce", "ceara" });
            this.stateWords.Add("pa", new List<string> { "pa", "para" });
            this.stateWords.Add("ma", new List<string> { "ma", "maranhao" });
            this.stateWords.Add("pb", new List<string> { "pb", "paraiba" });
            this.stateWords.Add("pr", new List<string> { "pr", "parana" });
            this.stateWords.Add("pe", new List<string> { "pe", "pernambuco" });
            this.stateWords.Add("pi", new List<string> { "pi", "piaui" });
            this.stateWords.Add("se", new List<string> { "se", "sergipe" });
            this.stateWords.Add("to", new List<string> { "to", "tocantins" });
            this.stateWords.Add("mt", new List<string> { "mt", "mato grosso", "mato", "grosso" });
            this.stateWords.Add("ms", new List<string> { "ms", "mato grosso do sul", "mato", "grosso", "do sul" });
            this.stateWords.Add("rn", new List<string> { "rn", "rio grande do norte", "grande", "norte" });
            this.stateWords.Add("rs", new List<string> { "rs", "rio grande do sul", "grande", "sul" });
            this.stateWords.Add("es", new List<string> { "es", "espirito santo", "espirito", "santo" });
        }

        public bool Contains(string state)
        {
            var find = this.stateWords.ContainsKey(this.uf);

            if (find == false)
            {
                return false;
            }

            for (var i = 0; i < this.stateWords[this.uf].Count; i++)
            {
                if (state.ToLower().Contains(this.stateWords[this.uf][i].ToString(CultureInfo.InvariantCulture)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}