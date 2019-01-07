namespace Veros.Paperless.Infra.Consultas
{
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;
    using Model.ViewModel;
    using NHibernate;

    public class DossiePorLoteCefConsulta : DaoBase, IDossiePorLoteCefConsulta
    {
        public IList<DossieStatusViewModel> Obter(int lotecefId)
        {
            string sql;

            sql = @"
Select * from 
(
SELECT  rownum linha,
    b.batch_code LoteId, 
    p.proc_code ProcessoId,
    p.typeproc_code ProcessoTipo, 
    proc_identificacao ProcessoNumero,
    i.dossieesperado_codigobarras FolderDossie,
    k.pack_identificacao CodCaixa,
    batch_status LoteStatus, 
    proc_status ProcStatus, 
    k.pack_status CaixaStatus,                                                                                               
    k.coleta_code ColetaCode,

    f.LOTECEF_CODE LoteCefCode,                                                                                                                            
    f.LOTECEF_STATUS LoteCefStatus,                                                                                                                            
    f.LOTECEF_DTCRIACAO LoteCefDataCriacao,
    f.LOTECEF_DTFIM LoteCefDataFim,

    k.PACK_DTMOV CaixaData,
    K.PACK_DTCONFERENCIA  CaixaDataConferencia,
    k.PACK_DTPREPARO CaixaDataPreparo,
    b.BATCH_TTX LoteDataTransmissao,
    b.BATCH_TICR LoteDataOcr,
    b.BATCH_TENVIO LoteDataEnvio,
    B.BATCH_REAVALIADOEM LoteDataReavaliado,
    B.BATCH_TFATURAMENTO LoteDataFaturamento,
    P.PROC_STARTTIME ProcDataStart,
    i.dossieesperado_code EsperadoCode,
    i.DOSSIEESPERADO_MATRICULAAGENTE EsperadoMatricula,
    i.DOSSIEESPERADO_HIPOTECA EsperadorHipoteca,
    i.DOSSIEESPERADO_NOMECONTRATO EsperadoContrato,
    k.PACK_DTDEVOLUCAO CaixaDataDevolucao,
    f.lotecef_dtaprov LoteCefDataAprovacao, 
    pa.qtdePags QtdePaginas
FROM pack k 
    JOIN batch b ON b.pack_code = k.pack_code  
    JOIN proc p ON p.batch_code = b.batch_code  
    left JOIN dossieesperado i ON i.pacote_code = k.pack_code AND b.dossieesperado_code = i.dossieesperado_code  
    left JOIN lotecef f ON f.lotecef_code = b.lotecef_code
    left JOIN 
    (
      SELECT Sum(m.mdoc_qtdepag) qtdePags, b.batch_code FROM BATCH B JOIN mdoc m ON m.batch_code = b.batch_code 
      WHERE m.mdoc_virtual = '1' AND m.mdoc_status <> '*' AND m.typedoc_Id NOT IN (27,990,75,121)
        AND b.batch_status <> '*'
      GROUP BY b.batch_code  
    ) pa ON pa.batch_code = b.batch_code 
WHERE k.pack_status <> '*'
    AND (b.batch_code IS NOT NULL OR i.dossieesperado_code IS NOT NULL)
    AND (b.batch_code IS NULL OR b.batch_status <> '*')    
    And B.lotecef_code = :loteCefId
ORDER BY k.pack_code, i.dossieesperado_code)";

            return this.Session
                .CreateSQLQuery(sql)
                .AddScalar("LoteId", NHibernateUtil.Int32)
                .AddScalar("ProcessoId", NHibernateUtil.Int32)
                .AddScalar("ProcessoTipo", NHibernateUtil.Int32)
                .AddScalar("ProcessoNumero", NHibernateUtil.String)
                .AddScalar("FolderDossie", NHibernateUtil.String)
                .AddScalar("CodCaixa", NHibernateUtil.String)
                .AddScalar("LoteStatus", NHibernateUtil.String)
                .AddScalar("ProcStatus", NHibernateUtil.String)
                .AddScalar("CaixaStatus", NHibernateUtil.String)
                .AddScalar("ColetaCode", NHibernateUtil.Int32)
                .AddScalar("LoteCefCode", NHibernateUtil.Int32)
                .AddScalar("LoteCefStatus", NHibernateUtil.String)
                .AddScalar("LoteCefDataCriacao", NHibernateUtil.DateTime)
                .AddScalar("LoteCefDataFim", NHibernateUtil.DateTime)
                .AddScalar("CaixaData", NHibernateUtil.DateTime)
                .AddScalar("CaixaDataConferencia", NHibernateUtil.DateTime)
                .AddScalar("CaixaDataPreparo", NHibernateUtil.DateTime)
                .AddScalar("LoteDataTransmissao", NHibernateUtil.DateTime)
                .AddScalar("LoteDataOcr", NHibernateUtil.DateTime)
                .AddScalar("LoteDataEnvio", NHibernateUtil.DateTime)
                .AddScalar("LoteDataReavaliado", NHibernateUtil.DateTime)
                .AddScalar("LoteDataFaturamento", NHibernateUtil.DateTime)
                .AddScalar("ProcDataStart", NHibernateUtil.DateTime)
                .AddScalar("EsperadoCode", NHibernateUtil.Int32)
                .AddScalar("EsperadoMatricula", NHibernateUtil.String)
                .AddScalar("EsperadorHipoteca", NHibernateUtil.String)
                .AddScalar("EsperadoContrato", NHibernateUtil.String)
                .AddScalar("CaixaDataDevolucao", NHibernateUtil.DateTime)
                .AddScalar("LoteCefDataAprovacao", NHibernateUtil.DateTime)
                .AddScalar("QtdePaginas", NHibernateUtil.Int32)            
                .SetParameter("loteCefId", lotecefId)
                .SetResultTransformer(CustomResultTransformer<DossieStatusViewModel>.Do())
                .List<DossieStatusViewModel>();
        }
    }
}
