namespace Veros.Paperless.Infra.Boot
{
    using Veros.Data;
    using Veros.Framework;
    using Veros.Framework.Service;

    public class DatabaseBoot : IConfiguracaoBoot
    {
        public void Execute()
        {
            Database.Config.AddMapping(this.GetAssembly());
            Database.Config.AddMigration(this.GetAssembly());
        }
    }
}
