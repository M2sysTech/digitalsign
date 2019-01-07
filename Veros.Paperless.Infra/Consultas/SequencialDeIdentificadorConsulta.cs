namespace Veros.Paperless.Infra.Consultas
{
    using Data.Hibernate;
    using Model.Consultas;
    using NHibernate;

    public class SequencialDeIdentificadorConsulta : DaoBase, ISequencialDeIdentificadorConsulta
    {
        public int Obter(string identificacao)
        {
            const string Hql = @"
SELECT SUM(Quantidade) + 1 TOTAL FROM
(
SELECT Count(0) Quantidade FROM BATCH WHERE BATCH_IDENTIFICACAO = :identificacao
UNION ALL
SELECT Count(0) Quantidade FROM BATCH_BK WHERE BATCH_IDENTIFICACAO = :identificacao
)
";
            return this.Session.CreateSQLQuery(Hql)
                .AddScalar("TOTAL", NHibernateUtil.Int32)
                .SetParameter("identificacao", identificacao)
                .UniqueResult<int>();
                ////.SetResultTransformer(CustomResultTransformer<int>.Do())
                ////.UniqueResult<int>();
        }
    }
}
