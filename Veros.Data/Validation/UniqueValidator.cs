//-----------------------------------------------------------------------
// <copyright file="UniqueValidator.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Data.Validation
{
    using System;
    using Castle.Components.Validator;
    using NHibernate.Criterion;
    using Veros.Data.Hibernate;
    using Veros.Framework;

    public class UniqueValidator : AbstractValidator
    {
        private readonly Type entityType;
        private readonly ISessionStore sessionStore;

        public UniqueValidator(Type entityType) : this(
            IoC.Current.Resolve<ISessionStore>(),
            entityType)
        {
        }

        public UniqueValidator(ISessionStore sessionStore, Type entityType)
        {
            this.sessionStore = sessionStore;
            this.entityType = entityType;
        }

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

            var viewModel = (ViewModel)instance;
            var propertyName = this.Property.Name;

            var id = this.sessionStore.Current.CreateCriteria(this.entityType)
                .SetProjection(Projections.Property("Id"))
                .Add(Restrictions.Eq(propertyName, fieldValue))
                .UniqueResult<int>();

            return id == 0 || id.Equals(viewModel.Id);
        }
    }
}
