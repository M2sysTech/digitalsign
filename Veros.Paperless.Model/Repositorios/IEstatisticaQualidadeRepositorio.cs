namespace Veros.Paperless.Model.Repositorios
{
    using Entidades;
    using Framework.Modelo;

    public interface IEstatisticaQualidadeRepositorio : IRepositorio<EstatisticaQualidade>
    {
        EstatisticaQualidade ObterDeHojePorUsuario(int usuarioId);

        void IncrementarOkParaHoje(int usuarioId);

        void IncrementarNaoOkParaHoje(int usuarioId);
    }
}
