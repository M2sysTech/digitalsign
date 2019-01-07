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
                throw new RegraDeNegocioException("Não é possível avaliar a data. O texto informado não é uma data");
            }

            DateTime dateTime;

            var novaData = data
                .Replace(".", "/")
                .Replace("-", "/")
                .ToUpper();

            if (DateTime.TryParse(novaData, out dateTime) == false)
            {
                throw new RegraDeNegocioException("Não é possível avaliar a data. O texto informado não é uma data");
            }

            var campos = novaData.Split('/');

            int mes;

            return int.TryParse(campos[1], out mes);
        }
    }
}