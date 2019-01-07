//-----------------------------------------------------------------------
// <copyright file="ObjectExtensions.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Extensão para objetos
    /// </summary>
    [DebuggerStepThrough]
    public static class ObjectExtensions
    {
        public static T SerializedClone<T>(this T source)
        {
            return ObjectComparer.Clone(source);
        }

        public static PropertyInfo[] GetProperties<T>(this T obj1)
        {
            return (
                from type in obj1.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                where (type.PropertyType.IsValueType || type.PropertyType.Equals(typeof(string)))
                select type).ToArray();
        }

        /// <summary>
        /// Converte um objeto em valor de enum
        /// </summary>
        /// <typeparam name="T">tipo do enum</typeparam>
        /// <param name="value">valor para converter</param>
        /// <returns>valor no tipo enum</returns>
        public static T ToEnum<T>(this object value)
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException(typeof(T) + " não é um Enum");
            }

            if (value is string)
            {
                return (T)Enum.Parse(typeof(T), value.ToString(), true);
            }

            return (T)Enum.ToObject(typeof(T), Convert.ToInt32(value));
        }

        /// <summary>
        /// Convert string para long. Caso passe nulo ou alfa, retorna 0
        /// </summary>
        /// <param name="value">String a ser convertida</param>
        /// <returns>Int 64. Caso passe nulo ou alfa, retorna 0</returns>
        public static long ToLong(this object value)
        {
            long ret = 0;

            if (value == null)
            {
                return ret;
            }
            
            long.TryParse(value.ToString(), out ret);
            return ret;
        }

        /// <summary>
        /// Convert string para long. Caso passe nulo ou alfa, retorna 0
        /// </summary>
        /// <param name="value">String a ser convertida</param>
        /// <returns>Int 32. Caso passe nulo ou alfa, retorna 0</returns>
        public static int ToInt(this object value)
        {
            int ret = 0;

            if (value == null)
            {
                return ret;
            }

            if (value.GetType().IsEnum)
            {
                return (int) value;
            }

            int.TryParse(value.ToString(), out ret);
            return ret;
        }

        public static decimal ToDecimal(this object value)
        {
            decimal ret = 0;

            if (value == null)
            {
                return ret;
            }

            decimal.TryParse(value.ToString(), out ret);
            return ret;
        }

        /// <summary>
        /// Converte para string mesmo se objeto sendo null
        /// </summary>
        /// <param name="value">Objeto a ser convertido para string</param>
        /// <returns>String. Se objeto for null, retorna string.Empty</returns>
        public static string ToStringSafe(this object value)
        {
            if (value != null)
            {
                return value.ToString();
            }
            
            return string.Empty;
        }

        /// <summary>
        /// Checa se um valor é do tipo bool
        /// </summary>
        /// <param name="value">Valor a ser checado</param>
        /// <returns>True se valor for boolean</returns>
        public static bool IsBoolean(this object value)
        {
            if (value == null)
            {
                return false;
            }

            bool boolean;
            return bool.TryParse(value.ToString(), out boolean);
        }

        public static bool ToBoolean(this object value)
        {
            if (value == null)
            {
                return false;
            }

            bool boolean;
            bool.TryParse(value.ToString(), out boolean);
            return boolean;
        }

        /// <summary>
        /// Checa se um valor é do tipo datetime
        /// </summary>
        /// <param name="value">Valor a ser checado</param>
        /// <returns>True se valor for datetime</returns>
        public static bool IsDateTime(this object value)
        {
            if (value == null)
            {
                return false;
            }

            DateTime date;
            return DateTime.TryParse(value.ToString(), out date);
        }

        /// <summary>
        /// Checa se um valor é do tipo float
        /// </summary>
        /// <param name="value">Valor a ser checado</param>
        /// <returns>True se valor for float</returns>
        public static bool IsFloat(this object value)
        {
            if (value == null)
            {
                return false;
            }

            float @float;
            return float.TryParse(value.ToString(), out @float);
        }

        /// <summary>
        /// Checa se um valor é do tipo integer
        /// </summary>
        /// <param name="value">Valor a ser checado</param>
        /// <returns>True se valor for float</returns>
        public static bool IsInt(this object value)
        {
            if (value == null)
            {
                return false;
            }

            int integer;
            return int.TryParse(value.ToString(), out integer);
        }

        /// <summary>
        /// Checa se um valor é de um enum T
        /// </summary>
        /// <param name="value">Valor a ser checado</param>
        /// <returns>True se valor for de um enum T</returns>
        public static bool IsValueOf<T>(this object value)
        {
            try
            {
                return Enum.IsDefined(typeof(T), Convert.ToInt32(value));
            }
            catch
            {
                return false;
            }
        }

        public static T Parse<T>(this string value)
        {
            var result = default(T);

            if (string.IsNullOrEmpty(value) == false)
            {
                var tc = TypeDescriptor.GetConverter(typeof(T));
                result = (T)tc.ConvertFrom(value);
            } 
            
            return result;
        }

        /// <summary>
        /// Cria uma classe a partir do nome
        /// </summary>
        /// <typeparam name="T">Tipo abstrato da classe</typeparam>
        /// <param name="className">Nome da classe</param>
        /// <returns>Uma classe do tipo T</returns>
        public static T New<T>(string className)
        {
            return (T)Activator.CreateInstance(typeof(T).Assembly.GetType(className, true, true));
        }

        /// <summary>
        /// Checa se um valor existe em uma lista de possibilidades
        /// </summary>
        /// <typeparam name="T">Tipo do valor</typeparam>
        /// <param name="value">Valor a ser checado</param>
        /// <param name="options">Lista de possibilidades</param>
        /// <returns>True se valor existe na lista de possibilidades</returns>
        public static bool IsIn<T>(this T value, params T[] options)
        {
            foreach (var option in options)
            {
                if (value.Equals(option))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Verifica se um objeto tem conteudo. Se for string, desconsidera espaços em branco
        /// </summary>
        /// <param name="object"></param>
        /// <returns></returns>
        public static bool NaoTemConteudo(this object @object)
        {
            if (@object == null)
            {
                return true;
            }

            if (@object is string)
            {
                var @string = @object.ToString();
                return string.IsNullOrEmpty(@string.Trim());
            }

            return false;
        }

        public static Assembly GetAssembly<T>(this T value)
        {
            return typeof(T).Assembly;
        }

        //// TODO: mover pro framework
        public static bool ContemSomenteLetras(this string value)
        {
            return value.ToCharArray().All(char.IsLetter);
        }
    }
}
