//-----------------------------------------------------------------------
// <copyright file="ILdapAuthenticator.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Security
{
    public interface ILdapAuthenticator
    {
        bool Authenticate(string domain, string user, string password);
    }
}