namespace Veros.Paperless.Infra.Consultas
{
    using Data.Hibernate;
    using Model.Consultas;

    public class DataInstalacaoConsulta : DaoBase, IDataInstalacaoConsulta
    {
        public DataInstalacao Obter()
        {
            var sql = @"
SELECT Max(INSTALADOR_DATA) Data FROM instalador WHERE ROWNUM = 1";
            return this.Session.CreateSQLQuery(sql)
                .SetResultTransformer(CustomResultTransformer<DataInstalacao>.Do())
                .UniqueResult<DataInstalacao>();                    
        }
    }
}
