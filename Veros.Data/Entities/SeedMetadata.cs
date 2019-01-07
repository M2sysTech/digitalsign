namespace Veros.Data.Entities
{
    using System;
    using Framework.Modelo;

    public abstract class SeedMetadata : Entidade
    {
        public virtual string Name
        {
            get;
            protected set;
        }

        public virtual string Version
        {
            get;
            set;
        }

        public virtual string AppVersion
        {
            get;
            set;
        }

        public virtual DateTime ExecutedDate
        {
            get;
            set;
        }
    }
}