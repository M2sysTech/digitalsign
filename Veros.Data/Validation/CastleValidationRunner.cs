//-----------------------------------------------------------------------
// <copyright file="CastleValidationRunner.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Data.Validation
{
    using Castle.Components.Validator;
    using Framework.Validation;

    /// <summary>
    /// Executor de validação utilizando Castle
    /// </summary>
    public class CastleValidationRunner : IValidationRunner
    {
        /// <summary>
        /// Executor de validação
        /// </summary>
        private readonly ValidatorRunner runner = new ValidatorRunner(new CachedValidationRegistry());

        /// <summary>
        /// Checa se objeto é válido
        /// </summary>
        /// <param name="validable">Objeto a ser validado</param>
        /// <returns>True se é válido</returns>
        public bool IsValid(Validable validable)
        {
            return this.runner.IsValid(validable, RunWhen.Everytime);
        }

        public bool IsValid(Validable validable, RunWhen runWhen)
        {
            return this.runner.IsValid(validable, runWhen);
        }

        /// <summary>
        /// Retorna resumo dos erros
        /// </summary>
        /// <param name="validable">Objeto a ser validado</param>
        /// <returns>Resumo dos erros</returns>
        public ValidationSummary GetSummary(Validable validable)
        {
            var summary = new ValidationSummary();
            var castleSummary = this.runner.GetErrorSummary(validable);

            foreach (var property in castleSummary.InvalidProperties)
            {
                foreach (var error in castleSummary.GetErrorsForProperty(property))
                {
                    summary.Add(property, error);
                }
            }

            return summary;
        }
    }
}