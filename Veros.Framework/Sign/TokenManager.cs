//-----------------------------------------------------------------------
// <copyright file="TokenManager.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Sign
{
    /// <summary>
    /// Gerenciador de token
    /// </summary>
    public class TokenManager
    {
        /// <summary>
        /// Gets DLL's suportadas pelo assinador
        /// </summary>
        public static string[] GetSupportedDll
        {
            get 
            {
                return new string[] { "aetpkss1.dll" };
            }
        }
    }
}
