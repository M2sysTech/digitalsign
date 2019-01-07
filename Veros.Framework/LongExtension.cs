//-----------------------------------------------------------------------
// <copyright file="LongExtension.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework
{
    using System;
    using Security;

    /// <summary>
    /// Extensão da classe Int64
    /// </summary>
    public static class LongExtension
    {
        public static byte[] ToBytes(this long value)
        {
            return BitConverter.GetBytes(value);
        }

        public static string Hash(this long value)
        {
            return new Hash().HashText(value.ToString());
        }

        public static string ToDiskSize(this long bytes)
        {
            const int Scale = 1024;
            var orders = new[] { "GB", "MB", "KB", "Bytes" };
            var max = (long) Math.Pow(Scale, orders.Length - 1);

            foreach (var order in orders)
            {
                if (bytes >= max)
                {
                    return string.Format("{0:##.##} {1}", decimal.Divide(bytes, max), order);
                }

                max /= Scale;
            }

            return "0 Bytes";            
        }
    }
}