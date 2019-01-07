namespace Veros.Data
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Reflection;
    using Migrator;
    using Migrator.Framework;
    using Veros.Framework;

    public class DatabaseSchema : IDatabaseSchema
    {
        private readonly IUnitOfWork unitOfWork;

        public DatabaseSchema(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void AtualizarParaUltimoMigration()
        {
            if (this.DontRunMigration())
            {
                Log.Application.Info("A configuração Database.DisableMigration no settings.config está marcado para não executar Migration");
                return;
            }

            this.ForceInitializeUnitOfWork();

            Log.Application.Info("Atualizando schema banco de dados");
                
            foreach (var migration in Database.Config.Migrations)
            {
                Log.Application.InfoFormat("Executando migration " + migration.GetName().Name);
                var migrator = this.GetMigrator(migration);
                migrator.MigrateToLastVersion();
            }

            Log.Application.Info("Schema do banco de dados atualizado");
        }

        public long ObterUltimaVersaoNaoAplicado()
        {
            return this.ObterTodosMigrations().OrderByDescending(x => x).FirstOrDefault();
        }

        public void SetarVersaoManualmente(long version)
        {
            var migrations = this.ObterTodosMigrations();
            var applied = this.ObterMigrationsAplicados();

            migrations.RemoveAll(applied.Contains);
            var toapply = migrations.Where(x => x <= version);

            foreach (var migration in toapply)
            {
                this.unitOfWork.Current
                    .CurrentSession
                    .CreateSQLQuery("insert into schemainfo (version) values (:version)")
                    .SetParameter("version", migration)
                    .ExecuteUpdate();
            }
        }

        public List<long> ObterTodosMigrations()
        {
            var avaliableMigrations = new List<long>();

            foreach (var migration in Database.Config.Migrations)
            {
                var types = this.GetMigrator(migration).MigrationsTypes;

                foreach (var type in types)
                {
                    var migrationAttribute = type.GetCustomAttribute<MigrationAttribute>();
                    avaliableMigrations.Add(migrationAttribute.Version);
                }
            }

            return avaliableMigrations.OrderBy(x => x).ToList();
        }

        public List<long> ObterMigrationsNaoAplicados()
        {
            var todos = this.ObterTodosMigrations();
            var aplicados = this.ObterMigrationsAplicados();

            return todos.Except(aplicados).ToList();
        }

        public List<long> ObterMigrationsAplicados()
        {
            var migrations = new List<long>();

            foreach (var migration in Database.Config.Migrations)
            {
                var versions = this.GetMigrator(migration).AppliedMigrations;
                migrations.AddRange(versions);
            }

            return migrations.Distinct().ToList();
        }

        private Migrator GetMigrator(Assembly migrations)
        {
            return new global::Migrator.Migrator(
                Database.ProviderName,
                Database.ConnectionString,
                migrations);
        }

        private void ForceInitializeUnitOfWork()
        {
            Log.Application.Info("Conectando com o banco de dados antes de atualizar schema do banco de dados");

            if (this.unitOfWork.Begun == false)
            {
                using (this.unitOfWork.Begin())
                {
                }
            }
        }

        private bool DontRunMigration()
        {
            return ConfigurationManager.AppSettings["Database.DisableMigration"].ToBoolean();
        }
    }
}