namespace Veros.Paperless.Infra.Consultas
{
    using Data.Hibernate;
    using Model.Consultas;

    public class PeriodoFaturamentoConsulta : DaoBase, IPeriodoFaturamentoConsulta
    {
        public PeriodoFaturamento Obter()
        {
            var sql = @"
SELECT 
    MAX(PACOTEPROCESSADO_RECEBIDOEM) DataFinal, MIN(PACOTEPROCESSADO_RECEBIDOEM) DataInicial
FROM 
    PACOTEPROCESSADO
WHERE 
    FATURADO = 1 
AND
    PACOTEPROCESSADO_STATUS = 0
";

            return this.Session.CreateSQLQuery(sql)
               .SetResultTransformer(CustomResultTransformer<PeriodoFaturamento>.Do())
               .UniqueResult<PeriodoFaturamento>();
        }
    }
}
