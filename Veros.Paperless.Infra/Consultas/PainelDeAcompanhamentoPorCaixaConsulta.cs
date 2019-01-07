namespace Veros.Paperless.Infra.Consultas
{
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;
    using NHibernate;

    public class PainelDeAcompanhamentoPorCaixaConsulta : DaoBase, IPainelDeAcompanhamentoPorCaixaConsulta
    {
        public IList<AcompanhamentoCaixa> Obter(int coletaId)
        {
            const string Sql = @"
SELECT  
  coleta_code as ColetaId,
  pack_code as CaixaId,
  pack_identificacao CaixaBarcode,
  (SELECT Count(1) FROM dossieesperado WHERE pacote_code =  p.pack_code) TotalPrevisto,
  (SELECT Count(1) FROM dossieesperado WHERE DOSSIEESPERADO_CODE not IN (SELECT DOSSIEESPERADO_CODE FROM batch WHERE p.pack_code = pack_code) AND pacote_code = p.pack_code) AS Preparo,
  (SELECT Count(1) FROM batch WHERE batch_status IN ('10','15','12') AND pack_code = p.pack_code) AS Captura,
  (SELECT Count(1) FROM batch WHERE batch_status IN ('30','35') AND pack_code = p.pack_code) AS Separacao,
  (SELECT Count(1) FROM batch WHERE batch_status IN ('50','55') AND pack_code = p.pack_code) AS Ocr,
  (SELECT Count(1) FROM batch WHERE batch_status IN ('45','4A') AND pack_code = p.pack_code) AS Capa,
  (SELECT Count(1) FROM batch WHERE batch_status IN ('81','82','83') AND pack_code = p.pack_code) AS Assinatura,
  (SELECT Count(1) FROM batch WHERE batch_status IN ('61','62') AND pack_code = p.pack_code) AS Classificacao,
  (SELECT Count(1) FROM batch WHERE batch_status IN ('M1','M5','MA') AND pack_code = p.pack_code) AS QualidadeM2,
  (SELECT Count(1) FROM batch WHERE batch_status IN ('Q1','Q5','QA') AND pack_code = p.pack_code) AS QualidadeCef,
  (SELECT Count(1) FROM batch WHERE batch_status IN ('J1','J5','J6','JA','JX') AND pack_code = p.pack_code) AS Ajustes,
  (SELECT Count(1) FROM batch WHERE batch_status IN ('65') AND pack_code = p.pack_code) AS Montagem,
  (SELECT Count(1) FROM batch WHERE batch_status IN ('XX') AND pack_code = p.pack_code) AS Devolucao,
  (SELECT Count(1) FROM batch WHERE batch_status IN ('G0', 'H0') AND pack_code = p.pack_code) AS Finalizado 
FROM pack p WHERE coleta_code = :coletaId
";

            return this.Session
                .CreateSQLQuery(Sql)
                .AddScalar("ColetaId", NHibernateUtil.Int32)
                .AddScalar("CaixaId", NHibernateUtil.Int32)
                .AddScalar("CaixaBarcode", NHibernateUtil.String)
                .AddScalar("TotalPrevisto", NHibernateUtil.Int32)
                .AddScalar("Preparo", NHibernateUtil.Int32)
                .AddScalar("Captura", NHibernateUtil.Int32)
                .AddScalar("Separacao", NHibernateUtil.Int32)
                .AddScalar("Ocr", NHibernateUtil.Int32)
                .AddScalar("Capa", NHibernateUtil.Int32)
                .AddScalar("Assinatura", NHibernateUtil.Int32)
                .AddScalar("Classificacao", NHibernateUtil.Int32)
                .AddScalar("QualidadeM2", NHibernateUtil.Int32)
                .AddScalar("QualidadeCef", NHibernateUtil.Int32)
                .AddScalar("Ajustes", NHibernateUtil.Int32)
                .AddScalar("Montagem", NHibernateUtil.Int32)
                .AddScalar("Devolucao", NHibernateUtil.Int32)
                .AddScalar("Finalizado", NHibernateUtil.Int32)
                .SetInt32("coletaId", coletaId)
                .SetResultTransformer(CustomResultTransformer<AcompanhamentoCaixa>.Do())
                .List<AcompanhamentoCaixa>();
        }
    }
}