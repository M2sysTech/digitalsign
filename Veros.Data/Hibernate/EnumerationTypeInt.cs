namespace Veros.Data.Hibernate
{
    using System;
    using System.Data;
    using Framework;
    using NHibernate.Dialect;
    using NHibernate.SqlTypes;
    using NHibernate.Type;

    public class EnumerationTypeInt<T> : PrimitiveType where T : Enumeration<T, int>
    {
        public EnumerationTypeInt() : base(new SqlType(DbType.Int32))
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
            get { return string.Empty; }
        }

        public override object Get(IDataReader rs, int index)
        {
            var o = rs[index];
            var value = o.ToString();
            return "0";///Enumeration<T>.FromString(value);
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
            var val = "0";//// (Enumeration<T>)value;
            parameter.Value = "0"; //// val.Value;
        }

        public override string ObjectToSQLString(object value, Dialect dialect)
        {
            return value.ToString();
        }
    }
}
