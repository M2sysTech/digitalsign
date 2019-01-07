namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework.Modelo;

    [Serializable]
    public class Dll : Entidade
    {
        public virtual string Version { get; set; }

        public virtual string Usage { get; set; }

        public virtual bool Overwrite { get; set; }

        public virtual string Name { get; set; }

        public virtual char Restart { get; set; }
    }
}
