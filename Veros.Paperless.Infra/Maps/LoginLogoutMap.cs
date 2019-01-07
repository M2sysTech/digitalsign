namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class LoginLogoutMap : ClassMap<LoginLogout>
    {
        public LoginLogoutMap()
        {
            this.Table("LOGINLOGOUT");
            this.Id(x => x.Id).Column("LOGINLOGOUT_CODE").GeneratedBy.Native("SEQ_LOGINLOGOUT");
            this.Map(x => x.DataLogin).Column("LOGINLOGOUT_DTIN");
            this.Map(x => x.DataLogout).Column("LOGINLOGOUT_DTOUT");
            this.Map(x => x.Maquina).Column("LOGINLOGOUT_MAQUINA");
            this.References(x => x.Usuario).Column("USR_CODE");
        }
    }
}
