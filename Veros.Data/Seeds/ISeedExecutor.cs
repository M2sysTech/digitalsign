namespace Veros.Data.Seeds
{
    using System.Collections.Generic;

    public interface ISeedExecutor
    {
        void Executar(Seed seed);

        bool IsEnabled();

        IEnumerable<Seed> GetSeeds();
    }
}