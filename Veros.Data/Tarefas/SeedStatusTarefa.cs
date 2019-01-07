namespace Veros.Data.Tarefas
{
    using System;
    using System.Linq;
    using Entities;
    using Seeds;
    using Veros.Framework;

    public class SeedStatusTarefa : ITarefaM2
    {
        private readonly IUnitOfWork unitOfWork;

        public SeedStatusTarefa(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public string TextoDeAjuda
        {
            get
            {
                return "Mostra informacoes do ultimo seed executado";
            }
        }

        public string Comando
        {
            get
            {
                return "seed status";
            }
        }

        public void Executar(params string[] args)
        {
            Console2.Normal("Mostrando status dos seeds:");
            Console2.Normal(" ");

            this.unitOfWork.Transacionar(() =>
            {
                var seedsInfo = this.unitOfWork.Current.CurrentSession.QueryOver<SeedInfo>().List();
                var seeds = IoC.Current.GetAllInstances<Seed>();

                foreach (var seed in seeds)
                {
                    var seedInfo = seedsInfo.FirstOrDefault(x => x.Name == seed.Nome);
                    
                    Console2.Normal(" ");
                    Console2.Alert("Seed: " + seed.Nome);

                    if (seedInfo == null)
                    {
                        Console2.Error("\tNunca foi executado");
                        continue;
                    }

                    var atualizado = seed.EstaAtualizado(seedInfo.Version);
                    
                    if (atualizado)
                    {
                        Console2.Success("\tAtualizado: Sim");
                    }
                    else
                    {
                        Console2.Error("\t***** Atualizado: NAO *****");
                    }

                    Console2.Normal("\tExecutado em: " + seedInfo.ExecutedDate.ToString("dd/MM/yyyy HH:mm:ss"));
                    Console2.Normal("\tVersao seed ultima execucao: " + seedInfo.Version);
                    Console2.Normal("\tVersao seed atual: " + seed.VersaoDoSeed());
                    Console2.Normal("\tVersao da aplicacao quando o seed foi executado: " + seedInfo.AppVersion);
                }
            });

            Console2.Normal(" ");
        }
    }
}
