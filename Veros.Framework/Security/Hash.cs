//-----------------------------------------------------------------------
// <copyright file="Hash.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Security
{
    public class Hash : HashBase
    {
        public override string Format
        {
            get
            {
                return "x2";
            }
        }
    }
}
