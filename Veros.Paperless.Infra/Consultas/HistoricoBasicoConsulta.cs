namespace Veros.Paperless.Infra.Consultas
{
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;
    using Model.Entidades;
    using Model.ViewModel;
    using NHibernate;

    public class HistoricoBasicoConsulta : DaoBase, IHistoricoBasicoConsulta
    {
        public IList<HistoricoBasicoViewModel> Obter(Lote lote)
        {
            var sql = @"SELECT t1.*, Nvl(u.usr_name,'sistema') Usuario  FROM 
(
    SELECT 'Caixa (Pack)'  NivelDeLog, pack_code Codigo, null CodigoDoUsuario, PACK_DTINIPREPARO Data, NULL AcaoLogDocumento, 'Inicio Preparo da Caixa ['|| pack_identificacao ||'] - Matricula dos envolvidos: '
    || u1.usr_matricula || ',' || u2.usr_matricula || ',' || u3.usr_matricula || ',' || u4.usr_matricula  LogDocumentoObservacao  
    FROM pack p 
    left JOIN usr u1 ON u1.usr_code = p.USR_PREPARO
    left JOIN usr u2 ON u2.usr_code = USR_PREPARO2                                                                                                                                                                         
    left JOIN usr u3 ON u3.usr_code = USR_PREPARO3
    left JOIN usr u4 ON u4.usr_code = USR_PREPARO4
    WHERE PACK_code  = (SELECT pack_code FROM batch WHERE batch_code = {0})
UNION
    SELECT 'Caixa (Pack)'  NivelDeLog, pack_code Codigo, null CodigoDoUsuario, PACK_DTPREPARO Data, NULL AcaoLogDocumento, 'Fim Preparo da Caixa ['|| pack_identificacao ||'] - Matricula dos envolvidos: '
    || u1.usr_matricula || ',' || u2.usr_matricula || ',' || u3.usr_matricula || ',' || u4.usr_matricula  LogDocumentoObservacao  
    FROM pack p 
    left JOIN usr u1 ON u1.usr_code = p.USR_PREPARO
    left JOIN usr u2 ON u2.usr_code = USR_PREPARO2
    left JOIN usr u3 ON u3.usr_code = USR_PREPARO3
    left JOIN usr u4 ON u4.usr_code = USR_PREPARO4
    WHERE PACK_code  = (SELECT pack_code FROM batch WHERE batch_code = {0})
UNION
    SELECT 'Pagina (doc)' NivelDeLog, doc_code Codigo, usr_code CodigoDoUsuario, LOGDOC_DATETIME Data, LOGDOC_ACTION AcaoLogDocumento, LOGDOC_OBS LogDocumentoObservacao FROM logdoc WHERE doc_code IN (SELECT doc_code FROM doc WHERE batch_code = {0})
UNION  
    SELECT 'Item Docum. (mdoc)' NivelDeLog, mdoc_code Codigo, usr_code CodigoDoUsuario, LOGMDOC_DATE Data,LOGMDOC_ACTION AcaoLogDocumento, LOGMDOC_OBS LogDocumentoObservacao FROM logmdoc WHERE mdoc_code IN (SELECT mdoc_code FROM mdoc WHERE batch_code = {0}) 
UNION 
    SELECT 'Dossie (batch)' NivelDeLog, batch_code Codigo, usr_code CodigoDoUsuario, LOGBATCH_DATE Data,LOGBATCH_ACTION AcaoLogDocumento, LOGBATCH_OBS LogDocumentoObservacao FROM logbatch WHERE batch_code = {0} 
union
    SELECT 'Dossie (proc)' NivelDeLog, PROC_CODE Codigo, usr_code CodigoDoUsuario, logproc_date Data, LOGPROC_ACTION AcaoLogDocumento, LOGPROC_OBS LogDocumentoObservacao FROM logproc WHERE proc_code IN (SELECT proc_code FROM proc WHERE batch_code = {0}) 
UNION  
    SELECT 'QualiM2sys ' NivelDeLog, mdoc_code Codigo, usr_code CodigoDoUsuario, regraproc_dtstart Data, NULL AcaoLogDocumento, regraproc_obs LogDocumentoObservacao FROM regraproc WHERE proc_code IN (SELECT proc_code FROM proc WHERE batch_code = {0}) AND regra_code = 1 
union 
    SELECT 'QualiCEF ' NivelDeLog, mdoc_code Codigo, usr_code CodigoDoUsuario, regraproc_dtstart Data, NULL AcaoLogDocumento, regraproc_obs LogDocumentoObservacao FROM regraproc WHERE proc_code IN (SELECT proc_code FROM proc WHERE batch_code = {0}) AND regra_code = 2
UNION
    SELECT 'Planilha CEF' NivelDeLog, dossieesperado_code Codigo, usr_code CodigoDoUsuario, LOGDOSSIEESPERADO_DATETIME Data, LOGDOSSIEESPERADO_ACTION AcaoLogDocumento, LOGDOSSIEESPERADO_DESC LogDocumentoObservacao FROM logdossieesperado WHERE DOSSIEESPERADO_CODE IN (SELECT DOSSIEESPERADO_CODE FROM batch WHERE batch_code = {0})
) t1 
left JOIN usr u ON u.usr_code = t1.CodigoDoUsuario 
ORDER BY t1.data";

        return this.Session.CreateSQLQuery(string.Format(sql, lote.Id))
            .AddScalar("NivelDeLog", NHibernateUtil.AnsiString)
            .AddScalar("Codigo", NHibernateUtil.Int32)
            .AddScalar("CodigoDoUsuario", NHibernateUtil.Int32)
            .AddScalar("Data", NHibernateUtil.DateTime)
            .AddScalar("AcaoLogDocumento", NHibernateUtil.AnsiString)
            .AddScalar("LogDocumentoObservacao", NHibernateUtil.AnsiString)
            .AddScalar("Usuario", NHibernateUtil.AnsiString)
            .SetResultTransformer(CustomResultTransformer<HistoricoBasicoViewModel>.Do())
            .List<HistoricoBasicoViewModel>();
        }
    }
}
