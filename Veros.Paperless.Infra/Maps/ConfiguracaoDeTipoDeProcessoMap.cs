namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class ConfiguracaoDeTipoDeProcessoMap : ClassMap<ConfiguracaoDeTipoDeProcesso>
    {
        public ConfiguracaoDeTipoDeProcessoMap()
        {
            this.ReadOnly();
            this.Table("TDOCTPROC");
            this.Id(x => x.Id).Column("TDOCTPROC_CODE").GeneratedBy.Native("SEQ_TDOCTPROC");
            this.References(x => x.TipoDocumento).Column("TYPEDOC_ID");
            this.References(x => x.TipoProcesso).Column("TYPEPROC_CODE");
            this.Map(x => x.QuantidadeDePaginas).Column("TDOCTPROC_QTDPAG");
            this.Map(x => x.Obrigatorio).Column("TDOCTPROC_OBRIGATORIO");
        }
    }
}
