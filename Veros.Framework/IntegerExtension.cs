//-----------------------------------------------------------------------
// <copyright file="IntegerExtension.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework
{
    using Security;

    public static class IntegerExtension
    {
        public static int PaginateSkip(this int pagina, int itemsPorPagina)
        {
            return (pagina - 1) * itemsPorPagina;
        }

        public static long MegaBytes(this int mega)
        {
            return mega * 1024 * 1024;
        }

        public static string Hash(this int value)
        {
            return new Hash().HashText(value.ToString());
        }
    }
}