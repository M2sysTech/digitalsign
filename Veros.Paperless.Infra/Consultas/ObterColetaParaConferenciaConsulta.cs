namespace Veros.Paperless.Infra.Consultas
{
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;
    using Model.ViewModel;
    using NHibernate;

    public class ObterColetaParaConferenciaConsulta : DaoBase, IObterColetaParaConferenciaConsulta
    {
        public IList<ObterColetaParaConferencia> Pesquisar(PesquisaPacoteViewModel filtros)
        {
            var statusPacote = filtros.PacoteStatus;
            
            var sql = @"
SELECT P.PACK_CODE PacoteId, P.PACK_IDENTIFICACAO Identificacao, C.COLETA_CODE ColetaId, 
COUNT(DE.DOSSIEESPERADO_CODE) TotalDossies, C.COLETA_DESCRICAO Descricao, C.COLETA_ENDERECO Endereco,
C.COLETA_DATA DataColeta, C.COLETA_DTRECEPCAO DataRecepcao
FROM PACK P INNER JOIN COLETA C ON P.COLETA_CODE = C.COLETA_CODE
LEFT JOIN DOSSIEESPERADO DE ON P.PACK_CODE = DE.PACOTE_CODE
WHERE P.PACK_STATUS = :statusPacote    
GROUP BY P.PACK_CODE, P.PACK_IDENTIFICACAO, C.COLETA_CODE, C.COLETA_DESCRICAO, C.COLETA_ENDERECO,
C.COLETA_DATA, C.COLETA_DTRECEPCAO";

            switch (filtros.ColunaDeOrdenacao)
            {
                case "caixa":
                    sql = filtros.TipoDeOrdenacao == "A" ? sql + " ORDER BY P.PACK_IDENTIFICACAO ASC" : sql + " ORDER BY P.PACK_IDENTIFICACAO DESC";
                    break;

                case "coleta":
                    sql = filtros.TipoDeOrdenacao == "A" ? sql + " ORDER BY C.COLETA_CODE ASC" : sql + " ORDER BY C.COLETA_CODE DESC";
                    break;

                case "descricao":
                    sql = filtros.TipoDeOrdenacao == "A" ? sql + " ORDER BY C.COLETA_DESCRICAO ASC" : sql + " ORDER BY C.COLETA_DESCRICAO DESC";
                    break;

                case "endereco":
                    sql = filtros.TipoDeOrdenacao == "A" ? sql + " ORDER BY C.COLETA_ENDERECO ASC" : sql + " ORDER BY C.COLETA_ENDERECO DESC";
                    break;

                case "dataColeta":
                    sql = filtros.TipoDeOrdenacao == "A" ? sql + " ORDER BY C.COLETA_DATA ASC" : sql + " ORDER BY C.COLETA_DATA DESC";
                    break;

                case "dataRecepcao":
                    sql = filtros.TipoDeOrdenacao == "A" ? sql + " ORDER BY C.COLETA_DTRECEPCAO ASC" : sql + " ORDER BY C.COLETA_DTRECEPCAO DESC";
                    break;

                default:
                    sql = sql + " ORDER BY P.PACK_IDENTIFICACAO";
                    break;
            }

            return this.Session
                .CreateSQLQuery(sql)
                .AddScalar("PacoteId", NHibernateUtil.Int32)
                .AddScalar("Identificacao", NHibernateUtil.String)
                .AddScalar("ColetaId", NHibernateUtil.Int32)
                .AddScalar("TotalDossies", NHibernateUtil.Int32)
                .AddScalar("Descricao", NHibernateUtil.String)
                .AddScalar("Endereco", NHibernateUtil.String)
                .AddScalar("DataColeta", NHibernateUtil.DateTime)
                .AddScalar("DataRecepcao", NHibernateUtil.DateTime)
                .SetParameter("statusPacote", statusPacote)
                .SetResultTransformer(CustomResultTransformer<ObterColetaParaConferencia>.Do())
                .List<ObterColetaParaConferencia>();
        }
    }
}
