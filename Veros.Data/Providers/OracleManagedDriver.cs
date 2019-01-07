namespace Veros.Data.Providers
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Reflection;
    using NHibernate.AdoNet;
    using NHibernate.Driver;
    using NHibernate.Engine.Query;
    using NHibernate.SqlTypes;

    public class OracleManagedDriver : ReflectionBasedDriver, IEmbeddedBatcherFactoryProvider
    {
        private const string DriverAssemblyName = "Oracle.ManagedDataAccess";
        private const string OracleParameterTypeName = "Oracle.ManagedDataAccess.Client.OracleParameter";
        private const string OracleDbTypeEnumName = "Oracle.ManagedDataAccess.Client.OracleDbType";
        private const string ConnectionTypeName = "Oracle.ManagedDataAccess.Client.OracleConnection";
        private const string CommandTypeName = "Oracle.ManagedDataAccess.Client.OracleCommand";
        private static readonly SqlType guidSqlType = new SqlType(DbType.Binary, 16);
        private readonly PropertyInfo oracleCommandBindByName;
        private readonly PropertyInfo oracleDbType;
        private readonly object oracleDbTypeRefCursor;

        public OracleManagedDriver()
            : base("Oracle.ManagedDataAccess.Client", DriverAssemblyName, ConnectionTypeName, CommandTypeName)
        {
            var oracleCommandType = this.CreateCommand().GetType();
            var parameterType = oracleCommandType.Assembly.GetType(OracleParameterTypeName);
            var oracleDbTypeEnum = oracleCommandType.Assembly.GetType(OracleDbTypeEnumName);
            this.oracleCommandBindByName = oracleCommandType.GetProperty("BindByName");
            this.oracleDbType = parameterType.GetProperty("OracleDbType");
            this.oracleDbTypeRefCursor = Enum.Parse(oracleDbTypeEnum, "RefCursor");
        }

        public static bool Versao10
        {
            get
            {
                var versaoOracle = ConfigurationManager.AppSettings["Database.Oracle.Version"];
                return string.IsNullOrEmpty(versaoOracle) == false && versaoOracle.Equals("10");
            }
        }

        Type IEmbeddedBatcherFactoryProvider.BatcherFactoryClass
        {
            get { return typeof(OracleDataClientBatchingBatcherFactory); }
        }

        public override bool UseNamedPrefixInSql
        {
            get { return true; }
        }

        public override bool UseNamedPrefixInParameter
        {
            get { return true; }
        }

        public override string NamedPrefix
        {
            get { return ":"; }
        }

        protected override void InitializeParameter(IDbDataParameter dbparam, string name, SqlType sqlType)
        {
            switch (sqlType.DbType)
            {
                case DbType.Boolean:
                    base.InitializeParameter(dbparam, name, SqlTypeFactory.Int16);
                    break;
                case DbType.Guid:
                    base.InitializeParameter(dbparam, name, guidSqlType);
                    break;
                default:
                    base.InitializeParameter(dbparam, name, sqlType);
                    break;
            }
        }

        protected override void OnBeforePrepare(IDbCommand command)
        {
            base.OnBeforePrepare(command);

            //// http://tgaw.wordpress.com/2006/03/03/ora-01722-with-odp-and-command-parameters/
            this.SetCommandPropertyValue(this.oracleCommandBindByName, command, true);

            var detail = CallableParser.Parse(command.CommandText);

            if (!detail.IsCallable)
            {
                return;
            }

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = detail.FunctionName;
            this.SetCommandPropertyValue(this.oracleCommandBindByName, command, false);

            var outCursor = command.CreateParameter();
            this.oracleDbType.SetValue(outCursor, this.oracleDbTypeRefCursor, null);
            outCursor.Direction = detail.HasReturn ? ParameterDirection.ReturnValue : ParameterDirection.Output;
            command.Parameters.Insert(0, outCursor);
        }

        protected virtual void SetCommandPropertyValue(PropertyInfo propertyInfo, IDbCommand command, object value)
        {
            propertyInfo.SetValue(command, value, null);
        }
    }
}
