namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Model.Entidades;

    public class EmailPendenteMap : ClassMap<EmailPendente>
    {
        public EmailPendenteMap()
        {
            this.Table("EMAILPENDENTE");
            this.Id(x => x.Id).Column("EMAILPENDENTE_CODE").GeneratedBy.Native("SEQ_EMAILPENDENTE");
            this.Map(x => x.De).Column("EMAILPENDENTE_DE");
            this.Map(x => x.Para).Column("EMAILPENDENTE_PARA");
            this.Map(x => x.TipoNotificacao).Column("EMAILPENDENTE_TIPONOTIFICACAO");
            this.Map(x => x.Status).Column("EMAILPENDENTE_STATUS");
            this.Map(x => x.Dt).Column("EMAILPENDENTE_DT");
            this.Map(x => x.EnviaEm).Column("EMAILPENDENTE_ENVIAEM");
            this.Map(x => x.Descricao).Column("EMAILPENDENTE_DESCRICAO");
            this.References(x => x.Lote).Column("BATCH_CODE");
            this.References(x => x.Processo).Column("PROC_CODE");
        }
    }
}