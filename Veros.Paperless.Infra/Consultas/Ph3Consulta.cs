namespace Veros.Paperless.Infra.Consultas
{
    using Data.Hibernate;
    using Model.Consultas;
    using Model.Ph3;

    public class Ph3Consulta : DaoBase, IPh3Consulta
    {
        public ConsultaPf Obter(string cpf)
        {
            var sql = @"
select 
    *
from
    ph3a
where
    cpf = :cpf";

            var resultado = this.Session.CreateSQLQuery(sql)
                .SetParameter("cpf", cpf)
                .UniqueResult();

            if (resultado == null)
            {
                return new ConsultaPf();
            }

            var lista = resultado as object[];
            var consultaPf = ConsultaPfFabrica.Criar(lista);

            return consultaPf;
        }
    }
}
