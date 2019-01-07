namespace Veros.Paperless.Infra.Consultas
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data.Hibernate;
    using Model.Entidades;
    using NHibernate;

    public class AcompanhamentoPorHoraConsulta : DaoBase, IAcompanhamentoPorHoraConsulta
    {
        public IList<Rajada> Obter(int pacoteProcessadoId)
        {
            var acompanhamentos = this.ObterAcompanhamentoPorHora(pacoteProcessadoId);

            IList<Rajada> rajadas = new List<Rajada>();

            foreach (var acompanhamento in acompanhamentos)
            {
                if (rajadas.Any(x => x.Arquivo == acompanhamento.Arquivo && x.DataRecebimento == acompanhamento.DataRecebimento) == false)
                {
                    rajadas.Add(new Rajada
                    {
                        Arquivo = acompanhamento.Arquivo,
                        DataRecebimento = acompanhamento.DataRecebimento,
                        TotalDeContas = acompanhamento.TotalDeContas,
                        Intervalos = this.ObterIntervalos(acompanhamento.Arquivo, acompanhamentos)
                    });    
                }
            }

            return rajadas;
        }

        private IList<IntervaloDeRajada> ObterIntervalos(string arquivo, IEnumerable<AcompanhamentoPorHora> acompanhamentos)
        {
            var intervalos = new List<IntervaloDeRajada>();

            var quantidadeAcumulada = 0;
            foreach (var acompanhamento in acompanhamentos.Where(x => x.Arquivo == arquivo).OrderBy(x => x.QuantidadeDeHoras))
            {
                quantidadeAcumulada += acompanhamento.FinalizadasNaHora;

                intervalos.Add(new IntervaloDeRajada
                {
                    Hora = acompanhamento.DataRecebimento.AddHours(acompanhamento.QuantidadeDeHoras),
                    Quantidade = quantidadeAcumulada,
                    QuantidadeDeHoras = acompanhamento.QuantidadeDeHoras
                });
            }

            return intervalos;
        }

        private IEnumerable<AcompanhamentoPorHora> ObterAcompanhamentoPorHora(int pacoteProcessadoId)
        {
            const string Hql = @"
SELECT PP.PACOTEPROCESSADO_ARQUIVO Arquivo, PP.PACOTEPROCESSADO_RECEBIDOEM DataRecebimento, PP.PACOTEPROCESSADO_TOTALCONTAS TotalDeContas, 
    Ceil((B.BATCH_TFIM - PP.PACOTEPROCESSADO_RECEBIDOEM) * 24) QuantidadeDeHoras, 
    Count(B.BATCH_CODE) FinalizadasNaHora
FROM BATCH B 
  INNER JOIN PACOTEPROCESSADO PP ON B.PACOTEPROCESSADO_CODE = PP.PACOTEPROCESSADO_CODE
WHERE PP.PACOTEPROCESSADO_RECEBIDOEM IS NOT NULL AND B.BATCH_TFIM IS NOT NULL
    AND B.BATCH_STATUS = :status
    AND PP.PACOTEPROCESSADO_CODE = :pacoteProcessadoCode
GROUP BY PP.PACOTEPROCESSADO_ARQUIVO, PP.PACOTEPROCESSADO_RECEBIDOEM, PP.PACOTEPROCESSADO_TOTALCONTAS, Ceil((B.BATCH_TFIM - PP.PACOTEPROCESSADO_RECEBIDOEM) * 24)
ORDER BY Arquivo, QuantidadeDeHoras
";
            return this.Session.CreateSQLQuery(Hql)
                .AddScalar("Arquivo", NHibernateUtil.String)
                .AddScalar("DataRecebimento", NHibernateUtil.DateTime)
                .AddScalar("TotalDeContas", NHibernateUtil.Int32)
                .AddScalar("QuantidadeDeHoras", NHibernateUtil.Int32)
                .AddScalar("FinalizadasNaHora", NHibernateUtil.Int32)
                .SetParameter("status", LoteStatus.Finalizado.Value)
                .SetParameter("pacoteProcessadoCode", pacoteProcessadoId)
               .SetResultTransformer(CustomResultTransformer<AcompanhamentoPorHora>.Do())
               .List<AcompanhamentoPorHora>();
        }

        public class AcompanhamentoPorHora
        {
            public string Arquivo { get; set; }

            public DateTime DataRecebimento { get; set; }

            public int TotalDeContas { get; set; }

            public int QuantidadeDeHoras { get; set; }

            public int FinalizadasNaHora { get; set; }
        }
    }
}
