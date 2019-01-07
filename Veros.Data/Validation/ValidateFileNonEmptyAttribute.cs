//-----------------------------------------------------------------------
// <copyright file="ValidateFileNonEmptyAttribute.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Data.Validation
{
    using System;
    using Castle.Components.Validator;

    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class ValidateFileNonEmptyAttribute : AbstractValidationAttribute
    {
        public ValidateFileNonEmptyAttribute(string errorMessage) : base(errorMessage)
        {
        }

        public override IValidator Build()
        {
            IValidator validator = new FileNonEmptyValidator();
            this.ConfigureValidatorMessage(validator);
            return validator;
        }
    }
}
