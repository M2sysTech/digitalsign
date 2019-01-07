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
    public class LicencaConsumidaPorTipoDocumentoConsulta : DaoBase, ILicencaConsumidaPorTipoDocumentoConsulta
    {
        public IList<LicencaConsumidaPorTipoDocumento> Obter(string dataInicio, string dataFim)
        {
            var sql = @"
select 
    typedoc.typedoc_desc TipoDocumento, 
    sum(licencaconsumida.licencaconsumida_qtde) QuantidadeLicencasConsumidas
from 
    mdoc mdoc, 
    doc doc,
    typedoc typedoc,
    licencaconsumida licencaconsumida,
    batch batch
where
    mdoc.mdoc_code = doc.mdoc_code
    and typedoc.typedoc_id = mdoc.typedoc_id
    and licencaconsumida.doc_code = doc.doc_code
    and typedoc.typedoc_reconhecivel = 1
    and mdoc.batch_code = batch.batch_code
    and batch.batch_dt between :dataInicio and :dataFim
group by
    typedoc.typedoc_desc
";

            return this.Session
                .CreateSQLQuery(sql)
                .AddScalar("TipoDocumento", NHibernateUtil.String)
                .AddScalar("QuantidadeLicencasConsumidas", NHibernateUtil.Int32)
                .SetDateTime("dataInicio", DateTime.Parse(dataInicio))
                .SetDateTime("dataFim", DateTime.Parse(dataFim))
                .SetResultTransformer(CustomResultTransformer<LicencaConsumidaPorTipoDocumento>.Do())
                .List<LicencaConsumidaPorTipoDocumento>();
        }
    }
}