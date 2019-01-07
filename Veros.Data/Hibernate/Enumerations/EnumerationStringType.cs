namespace Veros.Data.Hibernate.Enumerations
{
    using System;
    using System.Data;
    using Veros.Framework;
    using NHibernate.Dialect;
    using NHibernate.SqlTypes;
    using NHibernate.Type;

    public class EnumerationStringType<T> : PrimitiveType where T : EnumerationString<T>
    {
        public EnumerationStringType() : base(new SqlType(DbType.AnsiString))
        {
        }

        public override Type ReturnedClass
        {
            get { return typeof(T); }
        }

        public override string Name
        {
            get { return "EnumerationString"; }
        }

        public override Type PrimitiveClass
        {
            get { return typeof(string); }
        }

        public override object DefaultValue
        {
            get { return string.Empty; }
        }

        public override object Get(IDataReader rs, int index)
        {
            var o = rs[index];
            var value = o.ToString();
            return EnumerationString<T>.FromString(value);
        }

        public override object Get(IDataReader rs, string name)
        {
            var ordinal = rs.GetOrdinal(name);
            return this.Get(rs, ordinal);
        }

        public override object FromStringValue(string xml)
        {
            return xml;
        }

        public override void Set(IDbCommand cmd, object value, int index)
        {
            var parameter = (IDataParameter)cmd.Parameters[index];
            var val = (EnumerationString<T>)value;
            parameter.Value = val.Value;
        }

        public override string ObjectToSQLString(object value, Dialect dialect)
        {
            return value.ToString();
        }
    }
}
