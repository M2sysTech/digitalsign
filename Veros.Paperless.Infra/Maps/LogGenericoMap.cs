namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class LogGenericoMap : ClassMap<LogGenerico>
    {
        public LogGenericoMap()
        {
            this.Table("LOG");
            this.Id(x => x.Id).Column("LOG_CODE").GeneratedBy.Native("SEQ_LOG");
            this.Map(x => x.Acao).Column("LOG_ACTION");
            this.Map(x => x.Observacao).Column("LOG_DESC");
            this.Map(x => x.DataRegistro).Column("LOG_DATETIME");
            this.Map(x => x.UsuarioNome).Column("USR_NAME");
            this.Map(x => x.Modulo).Column("LOG_MODULE");
            this.Map(x => x.Registro).Column("LOG_REGISTER");
            this.Map(x => x.EstacaoDeTrabalho).Column("LOG_STATION");
            this.Map(x => x.Checado).Column("LOG_CHECKED");
        }
    }
}
