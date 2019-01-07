namespace Veros.Framework.Service
{
    using Modelo;

    public interface ITrack
    {
        void Snapshot<T>(T entidade) where T : IEntidade;

        bool HasChanged<T>(T entidade) where T : IEntidade;
    }
}