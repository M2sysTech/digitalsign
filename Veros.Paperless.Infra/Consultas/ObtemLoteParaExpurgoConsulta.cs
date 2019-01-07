namespace Veros.Paperless.Infra.Consultas
{
    using System;
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;
    using NHibernate;

    public class ObtemLoteParaExpurgoConsulta : DaoBase, IObtemLoteParaExpurgoConsulta
    {
        public IList<LoteParaExpurgoView> ObterDaBk(int pacoteId)
        {
            return this.Obter("_BK", pacoteId);
        }

        public IList<LoteParaExpurgoView> ObterDaHist(int pacoteId)
        {
            return this.Obter("_HIST", pacoteId);
        }

        private IList<LoteParaExpurgoView> Obter(string ambiente, int pacoteId)
        {
            const string Hql = @"
SELECT B.BATCH_CODE LoteId
FROM BATCH{0} B
WHERE B.PACOTEPROCESSADO_CODE = :pacoteId";

            return this.Session.CreateSQLQuery(string.Format(Hql, ambiente))
                .AddScalar("LoteId", NHibernateUtil.Int32)
                .SetParameter("pacoteId", pacoteId)
                .SetResultTransformer(CustomResultTransformer<LoteParaExpurgoView>.Do())
                .List<LoteParaExpurgoView>();
        }
    }
}