namespace Veros.Paperless.Infra.Consultas
{
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Entidades;
    using NHibernate;

    public class EstoqueFilasProducaoConsulta : DaoBase, IEstoqueFilasProducaoConsulta
    {
        public EstoqueFilasProducao Obter()
        {
            var estoqueCapturaRecaptura = this.ObterEstoqueCapturaRecaptura();
            var estoqueSeparacaoAutomatica = this.ObterEstoqueSeparacaoAutomatica();
            var estoqueTriagemOcr = this.ObterEstoqueTriagemOcr();
            var estoqueClassificacao = this.ObterEstoqueIdentificacaoManual();
            var estoqueQualidadeM2 = this.ObterEstoqueQualidadeM2();
            var estoqueAjuste = this.ObterEstoqueAjuste();
            var estoqueAjusteQCef = this.ObterEstoqueAjusteQCef();

            return EstoqueFilasProducao.Criar(
                estoqueCapturaRecaptura,
                estoqueSeparacaoAutomatica,
                estoqueTriagemOcr, 
                estoqueClassificacao, 
                estoqueQualidadeM2, 
                estoqueAjuste,
                estoqueAjusteQCef);
        }

        private IList<EstoqueFilasProducao.FaseCapturaRecaptura> ObterEstoqueCapturaRecaptura()
        {
            var sql = @"
SELECT 0 Alocado, capturaRecapturaEmAndamento.Total RestaTerminar FROM 
(
SELECT count(1) Total
FROM PROC P
INNER JOIN BATCH B ON P.BATCH_CODE = B.BATCH_CODE
INNER JOIN PACOTEPROCESSADO PP ON B.PACOTEPROCESSADO_CODE = PP.PACOTEPROCESSADO_CODE AND PP.PACOTEPROCESSADO_ATIVADO = 'S'
WHERE B.BATCH_STATUS IN ('10', 'R5')) capturaRecapturaEmAndamento
";
            var valor = this.Session.CreateSQLQuery(sql)
                .AddScalar("Alocado", NHibernateUtil.Int32)
                .AddScalar("RestaTerminar", NHibernateUtil.Int32)
                .SetResultTransformer(CustomResultTransformer<EstoqueFilasProducao.FaseCapturaRecaptura>.Do())
                .List<EstoqueFilasProducao.FaseCapturaRecaptura>();

            return valor;
        }

        private IList<EstoqueFilasProducao.FaseSeparacaoAutomatica> ObterEstoqueSeparacaoAutomatica()
        {
            var sql = @"
SELECT 0 Alocado, separacaoAutomaticaEmAndamento.Total RestaTerminar FROM 
(
SELECT count(1) Total
FROM PROC P
INNER JOIN BATCH B ON P.BATCH_CODE = B.BATCH_CODE
INNER JOIN PACOTEPROCESSADO PP ON B.PACOTEPROCESSADO_CODE = PP.PACOTEPROCESSADO_CODE AND PP.PACOTEPROCESSADO_ATIVADO = 'S'
WHERE B.BATCH_STATUS = '35') separacaoAutomaticaEmAndamento
";

            var valor = this.Session.CreateSQLQuery(sql)
                .AddScalar("Alocado", NHibernateUtil.Int32)
                .AddScalar("RestaTerminar", NHibernateUtil.Int32)
                .SetResultTransformer(CustomResultTransformer<EstoqueFilasProducao.FaseSeparacaoAutomatica>.Do())
                .List<EstoqueFilasProducao.FaseSeparacaoAutomatica>();

            return valor;
        }

        private IList<EstoqueFilasProducao.FilaTriagemOcr> ObterEstoqueTriagemOcr()
        {
            var sql = @"
SELECT triagemPendente.Total RestaTerminar, triagemEmAndamento.Total Alocado FROM 
(
SELECT count(1) Total
FROM PROC P
INNER JOIN BATCH B ON P.BATCH_CODE = B.BATCH_CODE
INNER JOIN PACOTEPROCESSADO PP ON B.PACOTEPROCESSADO_CODE = PP.PACOTEPROCESSADO_CODE AND PP.PACOTEPROCESSADO_ATIVADO = 'S'
WHERE P.PROC_STATUS= 'T5'   
    AND (P.PROC_STARTTIME IS NULL OR P.PROC_STARTTIME < (SYSDATE - 5 / 24 / 60))) triagemPendente,

(SELECT count(1) Total
FROM PROC P
INNER JOIN BATCH B ON P.BATCH_CODE = B.BATCH_CODE
INNER JOIN PACOTEPROCESSADO PP ON B.PACOTEPROCESSADO_CODE = PP.PACOTEPROCESSADO_CODE AND PP.PACOTEPROCESSADO_ATIVADO = 'S'
WHERE P.PROC_STATUS= 'T5'   
    AND (P.PROC_STARTTIME IS NOT NULL AND P.PROC_STARTTIME  BETWEEN (SYSDATE - 5 / 24 / 60) AND (SYSDATE + 1 / 24 / 60))) triagemEmAndamento
";

            var valor = this.Session.CreateSQLQuery(sql)
                .AddScalar("Alocado", NHibernateUtil.Int32)
                .AddScalar("RestaTerminar", NHibernateUtil.Int32)
                .SetResultTransformer(CustomResultTransformer<EstoqueFilasProducao.FilaTriagemOcr>.Do())
                .List<EstoqueFilasProducao.FilaTriagemOcr>();

            return valor;
        }

        private IList<EstoqueFilasProducao.FaseIdentificacaoManual> ObterEstoqueIdentificacaoManual()
        {
            var sql = @"
SELECT classificacaoEmAndamento.Total Alocado, classificacaoPendente.Total RestaTerminar FROM
(SELECT 
    count(0) Total
FROM BATCH B
    INNER JOIN MDOC M ON B.BATCH_CODE = M.BATCH_CODE
    INNER JOIN PACOTEPROCESSADO PP ON B.PACOTEPROCESSADO_CODE = PP.PACOTEPROCESSADO_CODE AND PP.PACOTEPROCESSADO_ATIVADO = 'S'
WHERE B.BATCH_STATUS = 'I5'
    AND M.TYPEDOC_ID = 990
    AND M.MDOC_STATUS != '*'
    AND M.MDOC_VIRTUAL = 0
    AND (M.MDOC_STARTTIME IS NULL OR M.MDOC_STARTTIME < (SYSDATE - 5 / 24 / 60))) classificacaoPendente,

(SELECT 
    count(0) Total
FROM BATCH B
    INNER JOIN MDOC M ON B.BATCH_CODE = M.BATCH_CODE
    INNER JOIN PACOTEPROCESSADO PP ON B.PACOTEPROCESSADO_CODE = PP.PACOTEPROCESSADO_CODE AND PP.PACOTEPROCESSADO_ATIVADO = 'S'
WHERE B.BATCH_STATUS = 'I5'
    AND M.TYPEDOC_ID = 990
    AND M.MDOC_STATUS != '*'
    AND M.MDOC_VIRTUAL = 0
    AND (M.MDOC_STARTTIME IS NOT NULL AND M.MDOC_STARTTIME  BETWEEN (SYSDATE - 5 / 24 / 60) AND SYSDATE)) classificacaoEmAndamento
";

            var valor = this.Session.CreateSQLQuery(sql)
                .AddScalar("Alocado", NHibernateUtil.Int32)
                .AddScalar("RestaTerminar", NHibernateUtil.Int32)
                .SetResultTransformer(CustomResultTransformer<EstoqueFilasProducao.FaseIdentificacaoManual>.Do())
                .List<EstoqueFilasProducao.FaseIdentificacaoManual>();

            return valor;
        }

        private IList<EstoqueFilasProducao.FaseQualidadeM2Sys> ObterEstoqueQualidadeM2()
        {
            var sql = @"
SELECT qualidadeM2EmAndamento.Total Alocado, qualidadeM2Pendente.Total RestaTerminar FROM 
(SELECT 
    Count(0) Total
FROM 
    PROC P 
    INNER JOIN BATCH B ON B.BATCH_CODE = P.BATCH_CODE
    INNER JOIN PACOTEPROCESSADO PP ON B.PACOTEPROCESSADO_CODE = PP.PACOTEPROCESSADO_CODE AND PP.PACOTEPROCESSADO_ATIVADO = 'S'
WHERE 
    P.PROC_STATUS = 'M5'  
    AND B.BATCH_STATUS = 'M5'
    AND (P.PROC_STARTTIME IS NULL OR P.PROC_STARTTIME < (SYSDATE - 10 / 24 / 60))) qualidadeM2Pendente,

(SELECT 
    Count(0) Total
FROM 
    PROC P 
    INNER JOIN BATCH B ON B.BATCH_CODE = P.BATCH_CODE
    INNER JOIN PACOTEPROCESSADO PP ON B.PACOTEPROCESSADO_CODE = PP.PACOTEPROCESSADO_CODE AND PP.PACOTEPROCESSADO_ATIVADO = 'S'
WHERE 
    P.PROC_STATUS = 'M5'  
    AND B.BATCH_STATUS = 'M5'
    AND (P.PROC_STARTTIME IS NOT NULL AND P.PROC_STARTTIME BETWEEN (SYSDATE - 10 / 24 / 60) AND SYSDATE)) qualidadeM2EmAndamento
";

            var valor = this.Session.CreateSQLQuery(sql)
                .AddScalar("Alocado", NHibernateUtil.Int32)
                .AddScalar("RestaTerminar", NHibernateUtil.Int32)
                .SetResultTransformer(CustomResultTransformer<EstoqueFilasProducao.FaseQualidadeM2Sys>.Do())
                .List<EstoqueFilasProducao.FaseQualidadeM2Sys>();

            return valor;
        }

        private IList<EstoqueFilasProducao.FaseAjustes> ObterEstoqueAjuste()
        {
            var sql = @"
SELECT ajustePendente.Total RestaTerminar, ajusteEmAndamento.Total Alocado FROM 
(SELECT 
    Count(DISTINCT p.proc_code) Total
FROM 
    PROC P
    INNER JOIN BATCH B ON P.BATCH_CODE = B.BATCH_CODE    
    INNER JOIN PACOTEPROCESSADO PP ON B.PACOTEPROCESSADO_CODE = PP.PACOTEPROCESSADO_CODE AND PP.PACOTEPROCESSADO_ATIVADO = 'S'
WHERE 
    B.BATCH_STATUS = 'J5'   
    AND NVL(B.BATCH_RESULTQCEF, '*') != 'M'
    AND (P.PROC_STARTTIME IS NULL OR P.PROC_STARTTIME < (SYSDATE - 5 / 24 / 60))) ajustePendente,

(SELECT 
    Count(DISTINCT p.proc_code) Total
FROM 
    PROC P
    INNER JOIN BATCH B ON P.BATCH_CODE = B.BATCH_CODE
    INNER JOIN PACOTEPROCESSADO PP ON B.PACOTEPROCESSADO_CODE = PP.PACOTEPROCESSADO_CODE AND PP.PACOTEPROCESSADO_ATIVADO = 'S'
WHERE 
    B.BATCH_STATUS = 'J5'   
    AND NVL(B.BATCH_RESULTQCEF, '*') != 'M'
    AND (P.PROC_STARTTIME IS NOT NULL AND P.PROC_STARTTIME BETWEEN (SYSDATE - 5 / 24 / 60) AND (SYSDATE + 1 / 24 / 60))) ajusteEmAndamento
";

            var valor = this.Session.CreateSQLQuery(sql)
                .AddScalar("Alocado", NHibernateUtil.Int32)
                .AddScalar("RestaTerminar", NHibernateUtil.Int32)
                .SetResultTransformer(CustomResultTransformer<EstoqueFilasProducao.FaseAjustes>.Do())
                .List<EstoqueFilasProducao.FaseAjustes>();

            return valor;
        }

        private IList<EstoqueFilasProducao.FaseAjustesQCef> ObterEstoqueAjusteQCef()
        {
            var sql = @"
SELECT ajustePendente.Total RestaTerminar, ajusteEmAndamento.Total Alocado FROM 
(SELECT 
    Count(DISTINCT p.proc_code) Total
FROM 
    PROC P
    INNER JOIN BATCH B ON P.BATCH_CODE = B.BATCH_CODE    
    INNER JOIN PACOTEPROCESSADO PP ON B.PACOTEPROCESSADO_CODE = PP.PACOTEPROCESSADO_CODE AND PP.PACOTEPROCESSADO_ATIVADO = 'S'
WHERE 
    B.BATCH_STATUS = 'J5'    
    AND B.BATCH_RESULTQCEF = 'M'
    AND (P.PROC_STARTTIME IS NULL OR P.PROC_STARTTIME < (SYSDATE - 5 / 24 / 60))) ajustePendente,

(SELECT 
    Count(DISTINCT p.proc_code) Total
FROM 
    PROC P
    INNER JOIN BATCH B ON P.BATCH_CODE = B.BATCH_CODE
    INNER JOIN PACOTEPROCESSADO PP ON B.PACOTEPROCESSADO_CODE = PP.PACOTEPROCESSADO_CODE AND PP.PACOTEPROCESSADO_ATIVADO = 'S'
WHERE 
    B.BATCH_STATUS = 'J5'   
    AND B.BATCH_RESULTQCEF = 'M'
    AND (P.PROC_STARTTIME IS NOT NULL AND P.PROC_STARTTIME BETWEEN (SYSDATE - 5 / 24 / 60) AND (SYSDATE + 1 / 24 / 60))) ajusteEmAndamento
";

            var valor = this.Session.CreateSQLQuery(sql)
                .AddScalar("Alocado", NHibernateUtil.Int32)
                .AddScalar("RestaTerminar", NHibernateUtil.Int32)
                .SetResultTransformer(CustomResultTransformer<EstoqueFilasProducao.FaseAjustesQCef>.Do())
                .List<EstoqueFilasProducao.FaseAjustesQCef>();

            return valor;
        }
    }
}