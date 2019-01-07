namespace Veros.Data.Seeds
{
    using System;
    using System.Collections.Generic;
    using Entities;
    using Veros.Data.Hibernate;
    using Veros.Framework;

    public class SeedExecutor : DaoBase, ISeedExecutor
    {
        public void Executar(Seed seed)
        {
            var seedInfo = this.GetSeedInfo(seed.Nome);

            if (seed.EstaAtualizado(seedInfo.Version))
            {
                Log.Application.InfoFormat(
                    "Seed {0} versão {1} já foi executado",
                    seed.Nome,
                    seed.VersaoDoSeed());

                return;
            }

            Log.Application.InfoFormat("Executando {0}", seed.Nome);

            seed.Executar();

            this.unitOfWork.Transacionar(() => this.UpdateSeedVersion(seed, seedInfo));
        }

        public bool IsEnabled()
        {
            return SettingsConfig.SeedExecute;
        }

        public IEnumerable<Seed> GetSeeds()
        {
            return IoC.Current.GetAllInstances<Seed>();
        }

        private void UpdateSeedVersion(Seed seed, SeedInfo seedInfo)
        {
            seedInfo.Version = seed.VersaoDoSeed();
            seedInfo.AppVersion = Aplicacao.Versao;
            seedInfo.ExecutedDate = DateTime.Now;

            this.Session.SaveOrUpdate(seedInfo);
            this.Session.Save(new SeedLog(seedInfo, Aplicacao.NomeDaMaquina));
        }

        private SeedInfo GetSeedInfo(string seedName)
        {
            return this.unitOfWork.Obter(() =>
            {
                var seedRegistry = this.Session
                    .QueryOver<SeedInfo>()
                    .Where(x => x.Name == seedName)
                    .Take(1)
                    .SingleOrDefault();

                if (seedRegistry == null)
                {
                    seedRegistry = new SeedInfo(seedName);
                }

                return seedRegistry;
            });
        }
    }
}