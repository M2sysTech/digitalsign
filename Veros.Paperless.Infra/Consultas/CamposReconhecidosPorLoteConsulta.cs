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
    public class CamposReconhecidosPorLoteConsulta : DaoBase, ICamposReconhecidosPorLoteConsulta
    {
        public IList<CampoRecohecidoPorLote> Obter(string dataInicio, string dataFim)
        {
            var sql = @"
select
    distinct mdoc.batch_code LoteId, 
    camposPassiveisReconhecimento.camposParaReconhecimento CamposReconheciveis, 
    licencasconsumidas.totalconsumido QuantidadeLicencasConsumidas, 
    camposBatidos.batido CamposBatidos, 
    count(mdoc.mdoc_code) DocumentosDoLote
from
    mdoc mdoc, batch batch,
    (select 
        mdoc.batch_code,
        count(tdcampos.tdcampos_code) as camposParaReconhecimento 
    from 
        mdoc mdoc inner join typedoc typedoc on (typedoc.typedoc_id = mdoc.typedoc_id) 
        inner join tdcampos tdcampos on (tdcampos.typedoc_id = typedoc.typedoc_id)
        inner join mdocdados mdocdados on (mdocdados.mdoc_code = mdoc.mdoc_code and tdcampos.tdcampos_code = mdocdados.tdcampos_code)
    where
        tdcampos.tdcampos_reconhecivel = 1
        and trim(mdocdados.mdocdados_valor2) is not null
    group by
        mdoc.batch_code) camposPassiveisReconhecimento, 
    (select 
        doc.batch_code, 
        sum(licencaconsumida.licencaconsumida_qtde) totalconsumido
    from 
        licencaconsumida licencaconsumida 
        inner join doc doc on (doc.doc_code = licencaconsumida.doc_code)
    group by
        doc.batch_code) licencasconsumidas,
    (select 
        mdoc.batch_code, 
        count(tdcampos.tdcampos_code) batido
    from 
        mdocdados mdocdados 
    inner join tdcampos tdcampos on (tdcampos.tdcampos_code = mdocdados.tdcampos_code)
    inner join mdoc mdoc on (mdoc.mdoc_code = mdocdados.mdoc_code)
    where
        tdcampos.tdcampos_reconhecivel = 1
        and mdocdados.mdocdados_ocrcomplementou = 1
    group by
        mdoc.batch_code) camposBatidos
where 
    camposPassiveisReconhecimento.batch_code = mdoc.batch_code and 
    licencasconsumidas.batch_code (+) = mdoc.batch_code  and 
    camposBatidos.batch_code (+) = mdoc.batch_code and
    batch.batch_code = mdoc.batch_code and
    batch.batch_dt between :dataInicio and :dataFim
group by 
    mdoc.batch_code, 
    camposPassiveisReconhecimento.camposParaReconhecimento, 
    licencasconsumidas.totalconsumido, 
    camposBatidos.batido
";

            return this.Session
                .CreateSQLQuery(sql)
                .AddScalar("LoteId", NHibernateUtil.Int32)
                .AddScalar("CamposReconheciveis", NHibernateUtil.Int32)
                .AddScalar("QuantidadeLicencasConsumidas", NHibernateUtil.Int32)
                .AddScalar("CamposBatidos", NHibernateUtil.Int32)
                .AddScalar("DocumentosDoLote", NHibernateUtil.Int32)
                .SetDateTime("dataInicio", DateTime.Parse(dataInicio))
                .SetDateTime("dataFim", DateTime.Parse(dataFim))
                .SetResultTransformer(CustomResultTransformer<CampoRecohecidoPorLote>.Do())
                .List<CampoRecohecidoPorLote>();
        }
    }
}