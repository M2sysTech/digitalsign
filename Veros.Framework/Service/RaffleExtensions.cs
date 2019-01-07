//-----------------------------------------------------------------------
// <copyright file="RaffleExtensions.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Service
{
    using System.Collections.Generic;

    public static class RaffleExtensions
    {
        public static IList<T> Raffle<T>(this IList<T> list, int length)
        {
            var raffle = GetRaffleService();
            return raffle.Execute(list, length);
        }

        private static IRaffle GetRaffleService()
        {
            return IoC.Current.Resolve<IRaffle>();
        }
    }
}