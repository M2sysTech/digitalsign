namespace Veros.Paperless.Model.Servicos.Comparacao
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ComparadorDeDatasComMesEAno : IComparador
    {
        public bool SaoIguais(string valorCadastro, string valorReconhecido)
        {
            if (string.IsNullOrEmpty(valorReconhecido) ||
                 string.IsNullOrEmpty(valorCadastro))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(valorReconhecido) ||
                string.IsNullOrWhiteSpace(valorCadastro))
            {
                return false;
            }

            return this.ComparaValores(valorReconhecido, valorCadastro);
        }

        public bool EhMenor(string primeiroValor, string segundoValor)
        {
            throw new NotImplementedException();
        }

        public bool EhMaior(string primeiroValor, string segundoValor)
        {
            throw new NotImplementedException();
        }

        public bool EhMenorOuIgual(string primeiroValor, string segundoValor)
        {
            throw new NotImplementedException();
        }

        public bool EhMaiorOuIgual(string primeiroValor, string segundoValor)
        {
            throw new NotImplementedException();
        }

        public bool Contem(string primeiroValor, string segundoValor)
        {
            throw new NotImplementedException();
        }

        private bool ComparaValores(string valorReconhecido, string valorCadastro)
        {
            valorReconhecido = valorReconhecido
                .Replace("/", string.Empty)
                .Replace("-", string.Empty)
                .Replace(".", string.Empty)
                .Replace("_", string.Empty)
                .Replace(" ", string.Empty);

            if (valorReconhecido.Length != valorCadastro.Length)
            {
                return this.ComparacaoEspecifica(valorReconhecido, valorCadastro);
            }

            return valorReconhecido == valorCadastro;
        }

        private bool ComparacaoEspecifica(string valorReconhecido, string valorCadastro)
        {
            valorReconhecido = this.ConverterMesDaData(valorReconhecido);

            var databatida = valorReconhecido.IndexOf(valorCadastro, System.StringComparison.Ordinal);
            
            return databatida > 0;
        }

        private string ConverterMesDaData(string data)
        {
            var meses = new Dictionary<string, string>
            {
                { "janeiro", "01" }, { "janeir0", "01" }, { "jan", "01" },
                { "fevereiro", "02" }, { "fevereir0", "02" }, { "fev", "02" },
                { "março", "03" }, { "març0", "03" }, { "marco", "03" }, { "marc0", "03" }, { "mar", "03" },
                { "abril", "04" }, { "abri1", "04" }, { "abr", "04" },
                { "maio", "05" }, { "mai0", "05" }, { "mai", "05" },
                { "junho", "06" }, { "junh0", "06" }, { "jun", "06" },
                { "julho", "07" }, { "julh0", "07" }, { "ju1h0", "07" }, { "jul", "07" }, { "ju1", "07" },
                { "agosto", "08" }, { "ag0st0", "08" }, { "agO", "08" }, { "ag0", "08" },
                { "setembro", "09" }, { "setembr0", "09" }, { "set", "09" },
                { "outubro", "10" }, { "0utubr0", "10" }, { "out", "10" }, { "0ut", "10" },
                { "novembro", "11" }, { "n0vembr0", "11" },  { "nov", "11" }, { "n0v", "11" }, 
                { "dezembro", "12" }, { "dezembr0", "12" }, { "dez", "12" }
            };

            foreach (var mes in meses.Where(mes => data.ToLower().Contains(mes.Key)).OrderByDescending(x => x.Key.Length))
            {
                return data.ToLower().Replace(mes.Key, mes.Value);
            }

            return data;
        }
    }
}