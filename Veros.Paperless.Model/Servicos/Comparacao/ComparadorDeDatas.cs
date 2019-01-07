namespace Veros.Paperless.Model.Servicos.Comparacao
{
    using System;

    public class ComparadorDeDatas : IComparador
    {
        public bool SaoIguais(string primeiroValor, string segundoValor)
        {
            var data1 = this.CriarData(primeiroValor);
            var data2 = this.CriarData(segundoValor);

            if (data1 == null || data2 == null)
            {
                return false;
            }
            
            return data1.GetValueOrDefault().Equals(data2.GetValueOrDefault());
        }

        public bool MesmoMesAno(string primeiroValor, string segundoValor)
        {
            var data1 = this.CriarData(primeiroValor);
            var data2 = this.CriarData(segundoValor);

            if (data1 == null || data2 == null)
            {
                return false;
            }

            return data1.GetValueOrDefault().Year == data2.GetValueOrDefault().Year &&
                data1.GetValueOrDefault().Month == data2.GetValueOrDefault().Month;
        }

        public bool EhMenor(string primeiroValor, string segundoValor)
        {
            var data1 = this.CriarData(primeiroValor);
            var data2 = this.CriarData(segundoValor);

            if (data1 == null || data2 == null)
            {
                return false;
            }

            return data1.GetValueOrDefault() < data2.GetValueOrDefault();
        }

        public bool EhMaior(string primeiroValor, string segundoValor)
        {
            var data1 = this.CriarData(primeiroValor);
            var data2 = this.CriarData(segundoValor);

            if (data1 == null || data2 == null)
            {
                return false;
            }

            return data1.GetValueOrDefault() > data2.GetValueOrDefault();
        }

        public bool EhMenorOuIgual(string primeiroValor, string segundoValor)
        {
            var data1 = this.CriarData(primeiroValor);
            var data2 = this.CriarData(segundoValor);

            if (data1 == null || data2 == null)
            {
                return false;
            }

            return data1.GetValueOrDefault() <= data2.GetValueOrDefault();
        }

        public bool EhMaiorOuIgual(string primeiroValor, string segundoValor)
        {
            var data1 = this.CriarData(primeiroValor);
            var data2 = this.CriarData(segundoValor);

            if (data1 == null || data2 == null)
            {
                return false;
            }

            return data1.GetValueOrDefault() >= data2.GetValueOrDefault();
        }

        public bool Contem(string primeiroValor, string segundoValor)
        {
            throw new NotImplementedException();
        }

        public DateTime? CriarData(string valor)
        {
            if (string.IsNullOrEmpty(valor) || valor.Length < 4)
            {
                return null;
            }

            valor = valor.CorrigirMesDaData();

            int dataNumero;
            if (int.TryParse(valor, out dataNumero))
            {
                valor = valor.Insert(2, "/");
                valor = valor.Insert(5, "/");
            }

            DateTime data;
            if (DateTime.TryParse(valor, out data))
            {
                return data;
            }

            return null;
        }
    }
}
