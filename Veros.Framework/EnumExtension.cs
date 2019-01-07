//-----------------------------------------------------------------------
// <copyright file="EnumExtension.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework
{
    using System;
    using System.Linq;

    /// <summary>
    /// Extensão para enum
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// Converte enum para long
        /// </summary>
        /// <param name="enum">Enum do contexto</param>
        /// <returns>Enum convertido para long</returns>
        public static long ToLong(this Enum @enum)
        {
            return Convert.ToInt64(@enum);
        }

        /// <summary>
        /// Retorna os valores da Enum
        /// </summary>
        /// <typeparam name="T">Tipo da Enum</typeparam>
        /// <param name="enum">Enum do contexto</param>
        /// <returns>Todos os valores da Enum</returns>
        public static T[] GetValues<T>(this Enum @enum)
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToArray();
        }
    }
}
