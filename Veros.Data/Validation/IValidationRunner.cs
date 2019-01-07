//-----------------------------------------------------------------------
// <copyright file="IValidationRunner.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Data.Validation
{
    using Castle.Components.Validator;
    using Framework.Validation;

    /// <summary>
    /// Contrato para execução de validação
    /// </summary>
    public interface IValidationRunner
    {
        /// <summary>
        /// Checa se objeto é válido
        /// </summary>
        /// <param name="validable">Objeto a ser validado</param>
        /// <returns>True se é válido</returns>
        bool IsValid(Validable validable);

        /// <summary>
        /// Retorna resumo dos erros
        /// </summary>
        /// <param name="validable">Objeto a ser validado</param>
        /// <returns>Resumo dos erros</returns>
        ValidationSummary GetSummary(Validable validable);

        bool IsValid(Validable validable, RunWhen runWhen);
    }
}
