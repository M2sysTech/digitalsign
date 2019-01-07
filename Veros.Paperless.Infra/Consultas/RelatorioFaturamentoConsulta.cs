namespace Veros.Paperless.Infra.Consultas
{
    using System;
    using Data.Hibernate;
    using Model.Consultas;
    using Model.Entidades;
    using NHibernate;

    public class RelatorioFaturamentoConsulta : DaoBase, IRelatorioFaturamentoConsulta
    {
        public RelatorioFaturamento Obter(string dataInicio, string dataFim)
        {
            ////Talvez temos que inserir o pacoteprocessado_status = ?, onde '?'  é o status do pacote pronto para faturar
////WHERE
////   b.batch_tfaturamento between :dataInicio and :dataFim
            
            var sql = @"
SELECT 
    to_char(b.batch_tfaturamento, 'DD/MM/YYYY') LoteProcessadoEm, 
    p.pack_identificacao CaixaOrigem, 
    ds.dossieesperado_ufarquivo UnidadeOrigem, 
    ds.dossieesperado_nomecontrato NumeroDossie,
    ds.dossieesperado_matriculaagente AgenteFinanceiro, 
    count(d.doc_code) QuantidadePaginas
FROM
  batch b
  inner join pack p ON p.pack_code = b.pack_code
  inner join dossieesperado ds on ds.dossieesperado_code = b.dossieesperado_code
  left join (SELECT * FROM doc WHERE doc_filetype = 'JPG') d ON d.batch_code = b.batch_code
GROUP BY
  To_Char(b.batch_tfaturamento, 'DD/MM/YYYY'), p.pack_identificacao, ds.dossieesperado_ufarquivo,  ds.dossieesperado_nomecontrato, ds.dossieesperado_matriculaagente
";

            var dossiesParaFaturar = this.Session
                .CreateSQLQuery(sql)
                .AddScalar("LoteProcessadoEm", NHibernateUtil.Date)
                .AddScalar("CaixaOrigem", NHibernateUtil.String)
                .AddScalar("UnidadeOrigem", NHibernateUtil.String)
                .AddScalar("NumeroDossie", NHibernateUtil.String)
                .AddScalar("AgenteFinanceiro", NHibernateUtil.String)
                .AddScalar("QuantidadePaginas", NHibernateUtil.Int32)
                ////.SetDateTime("dataInicio", DateTime.Parse(dataInicio))
                ////.SetDateTime("dataFim", DateTime.Parse(dataFim))
                .SetResultTransformer(CustomResultTransformer<DossieParaFaturar>.Do())
                .List<DossieParaFaturar>();

            var relatorio = new RelatorioFaturamento
            {
                DossiesParaFaturar = dossiesParaFaturar,
                Inicio = DateTime.Parse(dataInicio),
                Termino = DateTime.Parse(dataFim)
            };

            return relatorio;
        }
    }
}