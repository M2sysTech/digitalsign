//-----------------------------------------------------------------------
// <copyright file="FileNonEmptyValidator.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Data.Validation
{
    using Castle.Components.Validator;
    using System;
    using System.Web;

    public class FileNonEmptyValidator : AbstractValidator
    {
        public override bool SupportsBrowserValidation
        {
            get { return false; }
        }

        public override bool IsValid(object instance, object fieldValue)
        {
            if (instance is ViewModel == false)
            {
                throw new InvalidOperationException();
            }

            var file = (HttpPostedFileBase)this.Property.GetValue(instance, null);

            if (file == null || string.IsNullOrWhiteSpace(file.FileName))
            {
                return true;
            }

            return file.ContentLength > 0;
        }
    }
}
