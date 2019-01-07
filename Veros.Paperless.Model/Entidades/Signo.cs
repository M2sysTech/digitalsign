namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework.Modelo;
    using Veros.Paperless.Model.Servicos.Comparacao;

    [Serializable]
    public class Signo : Entidade
    {
        public virtual string ObterPorDataNascimento(string data)
        {
            var dataFormatada = data.ConverteParaData();

            var dia = dataFormatada.Day;
            var mes = dataFormatada.Month;

            if (mes == 01)
            {
                if (dia >= 21)
                {
                    return "AQUARIO";
                }

                return "CAPRICORNIO";
            }

            if (mes == 02)
            {
                if (dia >= 20)
                {
                    return "PEIXES";
                }

                return "AQUARIO";
            }

            if (mes == 03)
            {
                if (dia >= 21)
                {
                    return "ARIES";
                }

                return "PEIXES";
            }

            if (mes == 04)
            {
                if (dia >= 21)
                {
                    return "TOURO";
                }

                return "ARIES";
            }

            if (mes == 05)
            {
                if (dia >= 21)
                {
                    return "GEMEOS";
                }

                return "TOURO";
            }

            if (mes == 06)
            {
                if (dia >= 21)
                {
                    return "CANCER";
                }

                return "GEMEOS";
            }

            if (mes == 07)
            {
                if (dia >= 22)
                {
                    return "LEAO";
                }

                return "CANCER";
            }

            if (mes == 08)
            {
                if (dia >= 23)
                {
                    return "VIRGEM";
                }

                return "LEAO";
            }

            if (mes == 09)
            {
                if (dia >= 23)
                {
                    return "LIBRA";
                }

                return "VIRGEM";
            }

            if (mes == 10)
            {
                if (dia >= 23)
                {
                    return "ESCORPIAO";
                }

                return "LIBRA";
            }

            if (mes == 11)
            {
                if (dia >= 23)
                {
                    return "SAGITARIO";
                }

                return "ESCORPIAO";
            }

            if (mes == 12)
            {
                if (dia >= 22)
                {
                    return "CAPRICORNIO";
                }

                return "SAGITARIO";
            }

            return string.Empty;
        }
    }
}