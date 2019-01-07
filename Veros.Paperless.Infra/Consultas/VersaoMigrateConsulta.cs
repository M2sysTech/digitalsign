namespace Veros.Paperless.Infra.Consultas
{
    using Data.Hibernate;
    using Model.Consultas;

    public class VersaoMigrateConsulta : DaoBase, IVersaoMigrateConsulta
    {
        public VersaoMigrate Obter()
        {
            var sql = @"
SELECT To_Char(Max(VERSION)) Versao FROM SCHEMAINFO WHERE ROWNUM = 1";
            return this.Session.CreateSQLQuery(sql)
                .SetResultTransformer(CustomResultTransformer<VersaoMigrate>.Do())
                .UniqueResult<VersaoMigrate>();                    
        }
    }
}
