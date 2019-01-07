namespace Veros.Data
{
    using FluentNHibernate.Cfg.Db;
    using System.Collections.Generic;
    using System.Reflection;

    public class DatabaseConfiguration
    {
        private readonly List<Assembly> mappings = new List<Assembly>();
        private readonly List<Assembly> migrations = new List<Assembly>();
        private readonly IDictionary<string, string> hibernateSetting = new Dictionary<string, string>();

        public IEnumerable<Assembly> Migrations
        {
            get { return this.migrations; }
        }

        public IEnumerable<Assembly> Mappings
        {
            get { return this.mappings; }
        }

        public IDictionary<string, string> HibernateSettings
        {
            get { return this.hibernateSetting; }
        }

        public void AddHibernateSetting(string key, string value)
        {
            this.hibernateSetting.Add(key, value);
        }

        public void AddMapping(Assembly assembly)
        {
            this.mappings.Add(assembly);
        }

        public void AddMigration(Assembly assembly)
        {
            this.migrations.Add(assembly);
        }

        public CacheSettingsBuilder CacheConfiguration(CacheSettingsBuilder obj)
        {
            return obj;
        }
    }
}