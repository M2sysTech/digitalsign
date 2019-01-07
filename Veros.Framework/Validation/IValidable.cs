//-----------------------------------------------------------------------
// <copyright file="IValidable.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informa��o Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Validation
{
    using System.Runtime.Serialization;

    public interface IValidable
    {
        /// <summary>
        /// Gets resumo dos erros de valida��o
        /// </summary>
        [IgnoreDataMember]
        ValidationSummary ValidationSummary
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether � v�lido
        /// </summary>
        [IgnoreDataMember]
        bool IsValid
        {
            get;
        }

        /// <summary>
        /// Valida o estado da entidade
        /// </summary>
        /// <returns>True se est� v�lido</returns>
        bool Validate();
    }
}