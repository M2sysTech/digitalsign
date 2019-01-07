namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework.Modelo;

    public class LogOperador : Entidade
    {
        public virtual string UsuarioNome { get; set; }

        public virtual string UsuarioMatricula { get; set; }
        
        public virtual int UsuarioGrupo { get; set; }

        public virtual DateTime? HoraLogin { get; set; }

        public virtual DateTime? HoraLogoff { get; set; }

        public virtual string Maquina { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual DateTime? HoraUltimoAcesso { get; set; }
    }
}