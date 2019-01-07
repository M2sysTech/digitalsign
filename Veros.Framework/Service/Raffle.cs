//-----------------------------------------------------------------------
// <copyright file="Raffle.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Realiza sorteio entre objetos de uma lista
    /// </summary>
    public class Raffle : IRaffle
    {
        public T Execute<T>(IList<T> candidates)
        {
            if (candidates.Count == 0)
            {
                throw new InvalidOperationException("Lista para sorteio está vazia");
            }

            return this.Execute(candidates, 1)[0];
        }

        public IList<T> Execute<T>(IList<T> candidates, int length)
        {
            if (candidates.Count == 0)
            {
                return new List<T>();
            }

            if (length >= candidates.Count)
            {
                return candidates;
            }

            var random = new Random();
            var total = candidates.Count;
            var raffled = new T[length];

            while (total > 1)
            {
                total--;
                var next = random.Next(total + 1);
                var value = candidates[next];
                candidates[next] = candidates[total];
                candidates[total] = value;
            }

            Array.Copy(candidates.ToArray(), raffled, length);
            return raffled;
        }
    }
}