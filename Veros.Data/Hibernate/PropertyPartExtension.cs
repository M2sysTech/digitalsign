namespace Veros.Data.Hibernate
{
    using FluentNHibernate.Mapping;

    public static class PropertyPartExtension
    {
        public static PropertyPart BooleanoSimNao(this PropertyPart propertyPart)
        {
            return propertyPart.CustomType<SimNaoType>();
        }
    }
}
