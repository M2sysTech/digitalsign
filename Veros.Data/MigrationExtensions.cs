//-----------------------------------------------------------------------
// <copyright file="MigrationExtensions.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Data
{
    using System.Data;
    using Migrator.Framework;

    public static class MigrationExtensions
    {
        public static Column WithId(this Migration migration)
        {
            return migration.WithId("id");
        }

        public static Column WithId(this Migration migration, string columnName)
        {
            return new Column(columnName, DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity);
        }

        public static Column WithReference(this Migration migration, string column)
        {
            return new Column(column, DbType.Int32);
        }

        public static Column WithAuditBy(this Migration migration)
        {
            return migration.WithReference("created_by");
        }

        public static Column WithAuditAt(this Migration migration)
        {
            return new Column("created_at", DbType.DateTime);
        }

        public static IDbConnection GetConnection(this ITransformationProvider provider)
        {
            return provider.GetCommand().Connection;
        }

        public static void SetColumnAsChar4000(
            this ITransformationProvider transformationProvider,
            string tableName,
            string columnName)
        {
            string sql = string.Format(
                @"ALTER TABLE {0} MODIFY ({1} VARCHAR2(4000))",
                tableName,
                columnName);

            transformationProvider.ExecuteNonQuery(sql);
        }
    }
}
