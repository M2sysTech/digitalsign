namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework.Modelo;

    public class LoginLogout : Entidade
    {
        public virtual DateTime? DataLogin { get; set; }

        public virtual DateTime? DataLogout { get; set; }

        public virtual string Maquina { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}