namespace Veros.Data.Hibernate.Enumerations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentNHibernate.Conventions;
    using FluentNHibernate.Conventions.AcceptanceCriteria;
    using FluentNHibernate.Conventions.Inspections;
    using FluentNHibernate.Conventions.Instances;
    using Veros.Framework;

    public class EnumerationIntTypeConvention : IPropertyConvention, IPropertyConventionAcceptance
    {
        private static readonly Type openType = typeof(EnumerationIntType<>);

        public void Apply(IPropertyInstance instance)
        {
            var closedType = openType.MakeGenericType(instance.Property.PropertyType);
            instance.CustomType(closedType);
        }

        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => this.IsEnumerationType(x.Property.PropertyType));
        }

        private bool IsEnumerationType(Type type)
        {
            return this.GetTypeHierarchy(type)
                .Where(t => t.IsGenericType)
                .Select(t => t.GetGenericTypeDefinition())
                .Any(t => t == typeof(EnumerationInt<>));
        }

        private IEnumerable<Type> GetTypeHierarchy(Type type)
        {
            while (type != null)
            {
                yield return type;
                type = type.BaseType;
            }
        }
    }
}
