namespace Veros.Data.Hibernate
{
    using NHibernate.SqlTypes;
    using NHibernate.Type;

    public class SimNaoType : CharBooleanType
    {
        public SimNaoType() : base(new AnsiStringFixedLengthSqlType(1))
        {
        }

        public override string Name
        {
            get { return "SimNaoType"; }
        }

        protected override string TrueString
        {
            get { return "S"; }
        }

        protected override string FalseString
        {
            get { return "N"; }
        }
    }
}
