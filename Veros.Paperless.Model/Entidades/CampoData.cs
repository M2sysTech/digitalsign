namespace Veros.Paperless.Model.Entidades
{
    using System;
    using System.Text.RegularExpressions;
    using Framework.Modelo;

    public class CampoData
    {
        public static bool PossuiMesNumerico(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                throw new RegraDeNegocioException("N�o � poss�vel avaliar a data. O texto informado n�o � uma data");
            }

            DateTime dateTime;

            var novaData = data
                .Replace(".", "/")
                .Replace("-", "/")
                .ToUpper();

            if (DateTime.TryParse(novaData, out dateTime) == false)
            {
                throw new RegraDeNegocioException("N�o � poss�vel avaliar a data. O texto informado n�o � uma data");
            }

            var campos = novaData.Split('/');

            int mes;

            return int.TryParse(campos[1], out mes);
        }
    }
}