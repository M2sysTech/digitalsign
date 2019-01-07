//-----------------------------------------------------------------------
// <copyright file="CpfValidation.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Validation
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Validação de cpf
    /// </summary>
    public class CpfValidation
    {
        /// <summary>
        /// Checa se cpf é válido
        /// </summary>
        /// <param name="cpf">Cpf a ser validado</param>
        /// <returns>True se é válido</returns>
        public bool IsValid(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
            {
                return false;
            }

            if (Regex.IsMatch(cpf, @"(^(\d{3}.\d{3}.\d{3}-\d{2})|(\d{11})$)"))
            {
                return this.IsValidInternal(cpf);
            }

            return false;
        }

        /// <summary>
        /// Checa se cpf é válido
        /// </summary>
        /// <param name="cpf">Cpf a ser validado</param>
        /// <returns>True se é válido</returns>
        private bool IsValidInternal(string cpf)
        {
            var multiplicador1 = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", string.Empty).Replace("-", string.Empty);

            if (cpf.Length != 11)
            {
                return false;
            }

            var tempCpf = cpf.Substring(0, 9);

            // calcula primeiro digíto
            var soma = 0;

            for (var i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }

            var resto = soma % 11;

            resto = resto < 2 ? 0 : 11 - resto;

            var digito = resto.ToString();

            tempCpf = tempCpf + digito;

            // calcula segundo digíto
            soma = 0;
            for (var i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            digito = digito + resto;

            return cpf.EndsWith(digito);
        }
    }
}
