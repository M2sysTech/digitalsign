namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework.Modelo;
    using Iesi.Collections.Generic;
    using System.Collections.Generic;

    [Serializable]
    public class Perfil : Entidade
    {
        public static int IdGestorM2Sys = 1;
        public static int IdGestorLojista = 64;

        public Perfil()
        {
            this.Acessos = new List<Acesso>();
        }
    
        public virtual List<Acesso> Acessos
        {
            get;
            set;
        }

        public virtual string Descricao
        {
            get;
            set;
        }

        public virtual string Sigla
        {
            get;
            set;
        }
    }
}
