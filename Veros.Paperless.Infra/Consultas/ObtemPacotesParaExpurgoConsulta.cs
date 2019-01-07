namespace Veros.Paperless.Infra.Consultas
{
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;
    using NHibernate;

    public class ObtemPacotesParaExpurgoConsulta : DaoBase, IObtemPacotesParaExpurgoConsulta
    {
        public IList<PacoteParaExpurgoView> ObterDaBk(int quantidadeDeDias)
        {
            return this.Obter(quantidadeDeDias, "_BK");
        }

        public IList<PacoteParaExpurgoView> ObterDaHist(int quantidadeDeDias)
        {
            return this.Obter(quantidadeDeDias, "_HIST");
        }

        private IList<PacoteParaExpurgoView> Obter(int quantidadeDeDias, string origem)
        {
            const string Hql = @"
SELECT P.PACOTEPROCESSADO_CODE PacoteId
FROM PACOTEPROCESSADO{0} P
WHERE P.PACOTEPROCESSADO_RECEBIDOEM < SYSDATE - :quantidadeDeDias 
";
            return this.Session.CreateSQLQuery(string.Format(Hql, origem))
                .AddScalar("PacoteId", NHibernateUtil.Int32)
                .SetParameter("quantidadeDeDias", quantidadeDeDias)
                .SetResultTransformer(CustomResultTransformer<PacoteParaExpurgoView>.Do())
                .List<PacoteParaExpurgoView>();
        }
    }
}