namespace Veros.Paperless.Model.Repositorios
{
    using Framework.Modelo;
    using Veros.Paperless.Model.Entidades;

    public interface ITratamentoRegraRepositorio : IRepositorio<TratamentoRegra>
    {
        TratamentoRegra ObterPorRegra(int regraId);
    }
}