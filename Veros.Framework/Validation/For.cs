//-----------------------------------------------------------------------
// <copyright file="For.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Validation
{
    /// <summary>
    /// Enumeração de é valido para...
    /// </summary>
    public enum For
    {
        /// <summary>
        /// Qualquer operação
        /// </summary>
        Any = 0,

        /// <summary>
        /// Operação de inserção
        /// </summary>
        Insert = 1,

        /// <summary>
        /// Operação de alteração
        /// </summary>
        Update = 2,

        /// <summary>
        /// Operação de inserção ou alteração
        /// </summary>
        InsertOrUpdate = 3,

        /// <summary>
        /// Operação de exclusão
        /// </summary>
        Delete = 4
    }
}