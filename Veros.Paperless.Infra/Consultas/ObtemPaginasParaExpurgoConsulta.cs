namespace Veros.Paperless.Infra.Consultas
{
    using System;
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;

    public class ObtemPaginasParaExpurgoConsulta : DaoBase, IObtemPaginasParaExpurgoConsulta
    {
        public IList<PaginaExpurgoConsulta> Obter(DateTime? dataLimite)
        {
            var sql = @"
SELECT 
    DISTINCT(DOC_CODE) AS DocId, DOC_FILETYPE AS TipoArquivo  
FROM 
    DOC_BK D 
INNER JOIN BATCH_BK B ON B.BATCH_CODE=D.BATCH_CODE
INNER JOIN PACOTEPROCESSADO_BK PP ON PP.PACOTEPROCESSADO_CODE=B.PACOTEPROCESSADO_CODE
WHERE 
    PP.PACOTEPROCESSADO_STATUS=0 AND 
    PP.PACOTEPROCESSADO_RECEBIDOEM <= To_Date('{0} 23:59:59', 'dd/MM/yyyy HH24:MI:SS')";

            return this.Session
                .CreateSQLQuery(string.Format(sql, dataLimite.Value.Date.ToString("dd/MM/yyyy")))
                .SetResultTransformer(CustomResultTransformer<PaginaExpurgoConsulta>.Do())
                .List<PaginaExpurgoConsulta>();
        }
    }
}
