namespace Veros.Data.Entities
{
    public class SeedLog : SeedMetadata
    {
        public SeedLog(SeedInfo seedInfo, string machine)
        {
            this.Name = seedInfo.Name;
            this.Version = seedInfo.Version;
            this.AppVersion = seedInfo.AppVersion;
            this.ExecutedDate = seedInfo.ExecutedDate;
            this.Machine = machine;
        }

        protected SeedLog()
        {
        }

        public virtual string Machine
        {
            get;
            set;
        }
    }
}