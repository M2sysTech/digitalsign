//-----------------------------------------------------------------------
// <copyright file="IValidable.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Validation
{
    using System.Runtime.Serialization;

    public interface IValidable
    {
        /// <summary>
        /// Gets resumo dos erros de validação
        /// </summary>
        [IgnoreDataMember]
        ValidationSummary ValidationSummary
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether é válido
        /// </summary>
        [IgnoreDataMember]
        bool IsValid
        {
            get;
        }

        /// <summary>
        /// Valida o estado da entidade
        /// </summary>
        /// <returns>True se está válido</returns>
        bool Validate();
    }
}