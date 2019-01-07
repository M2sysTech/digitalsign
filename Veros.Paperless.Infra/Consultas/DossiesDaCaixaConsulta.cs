namespace Veros.Paperless.Infra.Consultas
{
    using System;
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;
    using NHibernate;

    public class DossiesDaCaixaConsulta : DaoBase, IDossiesDaCaixaConsulta
    {
        public IList<DossiesDaCaixa> Obter(int pacoteId)
        {
            var sql = @"
SELECT pk.pack_code Id, pk.pack_identificacao Caixa, pp.pacoteprocessado_recebidoem Data, 
p.proc_identificacao Dossie, de.dossieesperado_code DossieId, de.dossieesperado_codigobarras BarcodeCef, p.proc_status Status
FROM pacoteprocessado pp 
INNER JOIN batch b ON pp.pacoteprocessado_code = b.pacoteprocessado_code
INNER JOIN pack pk ON b.pack_Code = pk.pack_code
INNER JOIN proc p ON p.batch_Code = b.batch_code 
INNER JOIN dossieesperado de ON de.dossieesperado_Code = b.dossieesperado_code 
WHERE pk.pack_code = :pacoteId";  

        return this.Session
        .CreateSQLQuery(sql)
        .AddScalar("Id", NHibernateUtil.Int32)
        .AddScalar("Caixa", NHibernateUtil.String)
        .AddScalar("Data", NHibernateUtil.Date)
        .AddScalar("Dossie", NHibernateUtil.String)
        .AddScalar("DossieId", NHibernateUtil.Int32)
        .AddScalar("BarcodeCef", NHibernateUtil.String)                
        .AddScalar("Status", NHibernateUtil.String)                
        .SetParameter("pacoteId", pacoteId)
        .SetResultTransformer(CustomResultTransformer<DossiesDaCaixa>.Do())
        .List<DossiesDaCaixa>();                                
        }
    }
}
