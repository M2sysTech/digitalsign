//-----------------------------------------------------------------------
// <copyright file="IRaffle.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Service
{
    using System.Collections.Generic;

    /// <summary>
    /// Contrato para sorteio
    /// </summary>
    public interface IRaffle
    {
        /// <summary>
        /// Executa um sorteio entre os candidatos
        /// </summary>
        /// <typeparam name="T">Tipo dos candidatos</typeparam>
        /// <param name="candidates">Lista de candidatos</param>
        /// <returns>Candidato sorteado</returns>
        T Execute<T>(IList<T> candidates);

        IList<T> Execute<T>(IList<T> candidates, int length);
    }
}