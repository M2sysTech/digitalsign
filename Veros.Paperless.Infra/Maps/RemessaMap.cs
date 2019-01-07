namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class RemessaMap : ClassMap<Remessa>
    {
        public RemessaMap()
        {
            this.Table("REMESSA");
            this.Id(x => x.Id).Column("REMESSA_CODE").GeneratedBy.Native("SEQ_REMESSA");
            this.Map(x => x.Arquivo).Column("REMESSA_NAMEARQUIVO");
            this.Map(x => x.DataHoraGeracao).Column("REMESSA_DTGERACAO");
            this.Map(x => x.DataHoraRecibo).Column("REMESSA_DTRECIBO");
            this.Map(x => x.DataHoraRemessa).Column("REMESSA_DATA");
            this.Map(x => x.Status).Column("REMESSA_STATUS");
            this.Map(x => x.Extensao).Column("REMESSA_EXTENSAO");
            this.Map(x => x.Sequencial).Column("REMESSA_SEQUENCIAL");
            this.References(x => x.Processo).Column("PROC_CODE");
        }
    }
}
