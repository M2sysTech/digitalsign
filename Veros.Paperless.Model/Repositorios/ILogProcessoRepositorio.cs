namespace Veros.Paperless.Model.Repositorios
{
    using Entidades;
    using Framework.Modelo;

    public interface ILogProcessoRepositorio : IRepositorio<LogProcesso>
    {
        int ObterPorProcesso(int processoId);
    }
}
