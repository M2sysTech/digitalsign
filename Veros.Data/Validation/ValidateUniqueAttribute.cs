//-----------------------------------------------------------------------
// <copyright file="ValidateUniqueAttribute.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Data.Validation
{
    using Castle.Components.Validator;
    using System;

    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class ValidateUniqueAttribute : AbstractValidationAttribute
    {
        public ValidateUniqueAttribute(string message, Type entityType) : base(message)
        {
            this.EntityType = entityType;
        }

        public Type EntityType
        {
            get;
            private set;
        }

        public override IValidator Build()
        {
            IValidator validator = new UniqueValidator(this.EntityType);
            this.ConfigureValidatorMessage(validator);
            return validator;
        }
    }
}
