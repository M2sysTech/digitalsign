//-----------------------------------------------------------------------
// <copyright file="ValidateMaxLengthAttribute.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Data.Validation
{
    using System;
    using Castle.Components.Validator;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class ValidateMaxLengthAttribute : AbstractValidationAttribute
    {
        private readonly IValidator validator;

        public ValidateMaxLengthAttribute(int length, string errorMessage)
            : base(errorMessage)
        {
            this.validator = new MaxLengthValidator(length, errorMessage);
        }

        public override IValidator Build()
        {
            this.ConfigureValidatorMessage(this.validator);
            return this.validator;
        }
    }
}
