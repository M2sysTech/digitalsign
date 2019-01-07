namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class UsuarioMap : ClassMap<Usuario>
    {
        public UsuarioMap()
        {
            this.Table("USR");
            this.Id(x => x.Id).Column("USR_CODE").GeneratedBy.Native("SEQ_USR");
            this.Map(x => x.Login).Column("USR_MATRICULA");
            this.Map(x => x.Nome).Column("USR_NAME");
            this.Map(x => x.Senha).Column("USR_PWD").Not.Update();
            this.Map(x => x.Status).Column("USR_STATUSPWD").Not.Update();
            this.Map(x => x.Tipo).Column("USR_TYPE").Not.Update();
            this.Map(x => x.ForcarAlteracaoSenha).Column("USR_NETLOGIN");
            this.Map(x => x.NomeArquivoFoto).Column("USR_ARQUIVOFOTO");
            this.Map(x => x.Token).Column("USR_TOKEN");
            this.Map(x => x.DataUltimoLogin).Column("USR_LASTLOGIN");
            this.Map(x => x.UltimoAcesso).Column("USR_UPDATEDATE");
            this.Map(x => x.EstaLogado).Column("USR_LOGGED");

            this.References(x => x.Loja).Column("PREST_CODE");
            this.References(x => x.Perfil).Column("PERFIL_CODE");
            this.Where(" USR_TYPE IN ('W','U')");
        }
    }
}
