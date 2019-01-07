//-----------------------------------------------------------------------
// <copyright file="SimpleCryptography.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Security
{
    using System;
    using System.Text;

    public class SimpleCryptography : ICryptography
    {
        private const int Key = 309304;

        public string Encode(string value)
        {
            throw new NotImplementedException();
        }

        public string Decode(string value)
        {
            var s = value;
            var n = s.Length;
            var ss = new StringBuilder(n);
            var sn = new long[n + 1];
            const int K1 = 11 + (Key % 233);
            const int K2 = 7 + (Key % 239);
            const int K3 = 5 + (Key % 241);
            const int K4 = 3 + (Key % 251);

            for (var i = 1; i <= n; i++)
            {
                sn[i] = Convert.ToChar(s.Substring(i - 1, 1));
            }

            for (var i = 1; i <= n - 2; i++)
            {
                sn[i] = sn[i] ^ sn[i + 2] ^ (K4 * sn[i + 1]) % 256;
            }

            for (var i = n; i >= 3; i += -1)
            {
                sn[i] = sn[i] ^ sn[i - 2] ^ (K3 * sn[i - 1]) % 256;
            }

            for (var i = 1; i <= n - 1; i++)
            {
                sn[i] = sn[i] ^ sn[i + 1] ^ (K2 * sn[i + 1]) % 256;
            }

            for (var i = n; i >= 2; i += -1)
            {
                sn[i] = sn[i] ^ sn[i - 1] ^ (K1 * sn[i - 1]) % 256;
            }

            for (var i = 1; i <= n; i++)
            {
               ss.Append((char)sn[i]);
            }

            return ss.ToString();
        }
    }
}
