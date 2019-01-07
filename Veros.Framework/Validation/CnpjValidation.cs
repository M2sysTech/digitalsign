//-----------------------------------------------------------------------
// <copyright file="CnpjValidation.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Validation
{
    using System;

    /// <summary>
    /// Valida cnpj
    /// </summary>
    public class CnpjValidation
    {
        public bool IsValid(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            return this.IsValidInternal(value);
        }

        /// <summary>
        /// Checa se um cnpj é válido
        /// </summary>
        /// <param name="cnpj">Cnpj a ser validado</param>
        /// <returns>True se o cnpj é válido</returns>
        private bool IsValidInternal(string cnpj)
        {
            var multiplicador1 = new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            cnpj = cnpj.Trim();
            cnpj = cnpj
                .Replace(".", string.Empty)
                .Replace("-", string.Empty)
                .Replace("/", string.Empty);

            if (cnpj.Length != 14)
            {
                return false;
            }

            var tempCnpj = cnpj.Substring(0, 12);

            var soma = 0;

            for (var i = 0; i < 12; i++)
            {
                var cnpjPiece = tempCnpj[i].ToString();

                if (cnpjPiece.IsInt() == false)
                {
                    return false;
                }

                soma += int.Parse(cnpjPiece) * multiplicador1[i];
            }

            var resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            var digito = resto.ToString();

            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (var i = 0; i < 13; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            digito = digito + resto;

            return cnpj.EndsWith(digito);
        }
    }
}
