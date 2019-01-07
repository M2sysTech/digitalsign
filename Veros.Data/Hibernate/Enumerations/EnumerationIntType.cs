namespace Veros.Data.Hibernate.Enumerations
{
    using System;
    using System.Data;
    using Veros.Framework;
    using NHibernate.Dialect;
    using NHibernate.SqlTypes;
    using NHibernate.Type;

    public class EnumerationIntType<T> : PrimitiveType where T : EnumerationInt<T>
    {
        public EnumerationIntType() : base(new SqlType(DbType.Int32))
        {
        }

        public override Type ReturnedClass
        {
            get { return typeof(T); }
        }

        public override string Name
        {
            get { return "EnumerationInt"; }
        }

        public override Type PrimitiveClass
        {
            get { return typeof(int); }
        }

        public override object DefaultValue
        {
            get { return 0; }
        }

        public override object Get(IDataReader rs, int index)
        {
            var o = rs[index];
            var value = o.ToInt();
            return EnumerationInt<T>.FromInt(value);
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
            var val = (EnumerationInt<T>)value;
            parameter.Value = val.Value;
        }

        public override string ObjectToSQLString(object value, Dialect dialect)
        {
            return value.ToString();
        }
    }
}
