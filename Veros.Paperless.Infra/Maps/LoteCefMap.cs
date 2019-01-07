namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class LoteCefMap : ClassMap<LoteCef>
    {
        public LoteCefMap()
        {
            this.Table("LOTECEF");
            this.Id(x => x.Id).Column("LOTECEF_CODE").GeneratedBy.Native("SEQ_LOTECEF");
            this.Map(x => x.DataCriacao).Column("LOTECEF_DTCRIACAO");
            this.Map(x => x.DataFim).Column("LOTECEF_DTFIM");
            this.Map(x => x.Status).Column("LOTECEF_STATUS");
            this.Map(x => x.Visivel).Column("LOTECEF_VISIVEL");
            this.Map(x => x.QuantidadeDeLotes).Column("LOTECEF_QTBATCH");
            this.Map(x => x.DataAprovacao).Column("LOTECEF_DTAPROV");
            this.Map(x => x.DataGeracaoCertificado).Column("LOTECEF_DTGERACERTIFIC");
            this.Map(x => x.DataAssinaturaCertificado).Column("LOTECEF_DTASSINACERTIFIC");
            
            this.References(x => x.UsuarioAprovou).Column("USR_APROV");
            this.References(x => x.UsuarioGerou).Column("USR_GEROU");
            
            this.HasMany(x => x.Lotes)
                .KeyColumn("LOTECEF_CODE")
                .Cascade.None()
                .Inverse();

            this.DynamicUpdate();
        }
    }
}
