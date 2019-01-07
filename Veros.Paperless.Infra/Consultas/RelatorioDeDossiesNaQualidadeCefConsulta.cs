namespace Veros.Paperless.Infra.Consultas
{
    using System;
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;
    using NHibernate;

    /// <summary>
    /// TODO: escrever teste
    /// </summary>
    public class RelatorioDeDossiesNaQualidadeCefConsulta : DaoBase, IRelatorioDeDossiesNaQualidadeCefConsulta
    {
        public IList<DossiesNaQualidadeCef> Obter(DateTime dataMovimento)
        {
            var sql = @"
SELECT P.PROC_IDENTIFICACAO Dossie, P.BATCH_CODE Lote, P.PROC_CODE Processo FROM PROC P 
INNER JOIN BATCH B ON P.BATCH_CODE = B.BATCH_CODE
INNER JOIN PACOTEPROCESSADO PP ON B.PACOTEPROCESSADO_CODE = PP.PACOTEPROCESSADO_CODE
WHERE PP.PACOTEPROCESSADO_RECEBIDOEM = :DataMovimento
AND B.BATCH_STATUS = 'Q5'
ORDER BY P.PROC_IDENTIFICACAO";

            return this.Session
                .CreateSQLQuery(sql)
                .AddScalar("Dossie", NHibernateUtil.String)
                .AddScalar("Lote", NHibernateUtil.Int32)
                .AddScalar("Processo", NHibernateUtil.Int32)
                .SetDateTime("DataMovimento", dataMovimento)
                .SetResultTransformer(CustomResultTransformer<DossiesNaQualidadeCef>.Do())
                .List<DossiesNaQualidadeCef>();
        }
    }
}