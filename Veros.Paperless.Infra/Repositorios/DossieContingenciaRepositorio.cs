namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class DossieContingenciaRepositorio : Repositorio<DossieContingencia>, IDossieContingenciaRepositorio
    {
        public IList<DossieContingencia> Pesquisar(string contrato, string matricula, string barcode)
        {
            var query = this.Session.QueryOver<DossieContingencia>();

            if (string.IsNullOrEmpty(contrato) == false)
            {
                query.Where(x => x.NumeroContrato == contrato);
            }

            if (string.IsNullOrEmpty(matricula) == false)
            {
                query.Where(x => x.MatriculaAgente == matricula);
            }

            if (string.IsNullOrEmpty(barcode) == false)
            {
                query.Where(x => x.CodigoDeBarras == barcode);
            }

            return query.List<DossieContingencia>();
        }
    }
}