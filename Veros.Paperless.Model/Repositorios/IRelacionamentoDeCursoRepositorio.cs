namespace Veros.Paperless.Model.Repositorios
{
    using Entidades;
    using Framework.Modelo;

    public interface IRelacionamentoDeCursoRepositorio : IRepositorio<RelacionamentoDeCurso>
    {
        string ObterNomeDaInstituicaoDeEnsino(int relacionamentoDeCursoId);
    }
}
