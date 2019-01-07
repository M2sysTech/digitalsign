//-----------------------------------------------------------------------
// <copyright file="MaxLengthValidator.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Data.Validation
{
    using System;
    using Castle.Components.Validator;

    [Serializable]
    public class MaxLengthValidator : AbstractValidator
    {
        private readonly int length;
        private readonly string message;
        
        public MaxLengthValidator(int length, string message)
        {
            this.length = length;
            this.message = message;
        }

        public override bool SupportsBrowserValidation
        {
            get { return false; }
        }

        public override bool IsValid(object instance, object fieldValue)
        {
            if (fieldValue == null)
            {
                return true;
            }

            return fieldValue.ToString().Length < this.length;
        }

        protected override string BuildErrorMessage()
        {
            return string.Format(this.message);
        }
    }
}
