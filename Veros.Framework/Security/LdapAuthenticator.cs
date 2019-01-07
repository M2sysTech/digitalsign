//-----------------------------------------------------------------------
// <copyright file="LdapAuthenticator.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Security
{
    using System.DirectoryServices.AccountManagement;

    public class LdapAuthenticator : ILdapAuthenticator
    {
        public bool Authenticate(string domain, string user, string password)
        {
            using (var context = new PrincipalContext(ContextType.Domain, domain))
            {
                return context.ValidateCredentials(user, password);
            }
        }
    }
}