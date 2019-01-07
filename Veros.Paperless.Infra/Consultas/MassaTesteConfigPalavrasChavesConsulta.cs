namespace Veros.Paperless.Infra.Consultas
{
    using System;
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;
    using NHibernate;

    public class MassaTesteConfigPalavrasChavesConsulta : DaoBase, IMassaTesteConfigPalavrasChavesConsulta
    {
        public IList<DocumentoParaPalavraChave> Obter(string sqlNativo)
        {
            //// exemplo de query esperada (listar sempre os PDFs):
            //// SELECT m.mdoc_code DocumentoId, m.mdoc_pai DocumentoPaiId, m.typedoc_id TipoDocumentoId FROM mdoc m JOIN batch b ON b.batch_code = m.batch_code where (...)
            return this.Session.CreateSQLQuery(sqlNativo)
                .AddScalar("DocumentoId", NHibernateUtil.Int32)
                .AddScalar("DocumentoPaiId", NHibernateUtil.Int32)
                .AddScalar("TipoDocumentoId", NHibernateUtil.Int32)
                .SetResultTransformer(CustomResultTransformer<DocumentoParaPalavraChave>.Do())
                .List<DocumentoParaPalavraChave>();
        }
    }
}