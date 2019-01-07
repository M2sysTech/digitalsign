namespace Veros.Data
{
    using System.Linq;
    using Framework;
    using Seeds;

    public class DatabaseSeed : IDatabaseSeed
    {
        private readonly ISeedExecutor seedExecutor;

        public DatabaseSeed(ISeedExecutor seedExecutor)
        {
            this.seedExecutor = seedExecutor;
        }

        public void Executar()
        {
            if (this.seedExecutor.IsEnabled() == false)
            {
                Log.Application.Fatal("A chave Seed.Execute no arquivo settings.config n�o existe ou est� com valor false. Altere para true");
                Log.Application.Fatal("N�o executou seed");
                return;
            }

            var seeds = this.seedExecutor.GetSeeds();

            if (seeds.Any() == false)
            {
                Log.Application.Fatal("N�o foi encontrado Seed");
                return;
            }

            foreach (var seed in seeds)
            {
                this.seedExecutor.Executar(seed);    
            }
        }
    }
}