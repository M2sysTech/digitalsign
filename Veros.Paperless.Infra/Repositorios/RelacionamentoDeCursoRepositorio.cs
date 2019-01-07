namespace Veros.Paperless.Infra.Repositorios
{
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class RelacionamentoDeCursoRepositorio : Repositorio<RelacionamentoDeCurso>, IRelacionamentoDeCursoRepositorio
    {
        public string ObterNomeDaInstituicaoDeEnsino(int relacionamentoDeCursoId)
        {
            var relacionamentoDeCurso = this.Session.QueryOver<RelacionamentoDeCurso>()
                .Where(x => x.Id == relacionamentoDeCursoId)
                .Fetch(x => x.EnderecoDeInstituicaoDeEnsino).Eager
                .Fetch(x => x.EnderecoDeInstituicaoDeEnsino.InstituicaoDeEnsino).Eager
                .Take(1)
                .SingleOrDefault<RelacionamentoDeCurso>();

            if (relacionamentoDeCurso != null)
            {
                return relacionamentoDeCurso.EnderecoDeInstituicaoDeEnsino.InstituicaoDeEnsino.Nome;
            }

            return string.Empty;
        }
    }
}
