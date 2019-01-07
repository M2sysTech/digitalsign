namespace Veros.Paperless.Model.Repositorios
{
    using Entidades;
    using Framework.Modelo;

    public interface ILogBatimentoRepositorio : IRepositorio<LogBatimento>
    {
        LogBatimento ObterPorIndexacao(Indexacao indexacao);
    }
}