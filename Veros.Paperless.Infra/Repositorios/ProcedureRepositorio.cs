namespace Veros.Paperless.Infra.Repositorios
{
    using Data.Hibernate;
    using Model.Entidades;
    using Model.Repositorios;

    public class ProcedureRepositorio : Repositorio<Documento>, IProcedureRepositorio
    {
        public void RegerarPdfPorDocumento(int documentoId)
        {
            string query = @"call REGERARPDFPORMDOC(:documentoId)";

            this.Session.CreateSQLQuery(query)
                .SetParameter("documentoId", documentoId)
                .ExecuteUpdate();
        }

        public void RetornarLoteParaSeparacao(int loteId)
        {
            string query = @"call RETORNAPARASEPARACAO(:loteId)";

            this.Session.CreateSQLQuery(query)
                .SetParameter("loteId", loteId)
                .ExecuteUpdate();
        }

        public void RecontarPaginasCertificado(int lotecefId)
        {
            string query = @"call RECONTARPAGINASLOTECEF(:lotecefId)";

            this.Session.CreateSQLQuery(query)
                .SetParameter("lotecefId", lotecefId)
                .ExecuteUpdate();
        }
    }
}
