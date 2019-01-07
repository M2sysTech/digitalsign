namespace Veros.Data.Boot
{
    using Framework;
    using Framework.Service;

    public class ConfiguracaoBoot : IConfiguracaoBoot
    {
        public void Execute()
        {
            Database.Config.AddMapping(this.GetAssembly());
            Database.Config.AddMigration(this.GetAssembly());
        }
    }
}