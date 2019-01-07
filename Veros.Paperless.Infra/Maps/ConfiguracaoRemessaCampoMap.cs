namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class ConfiguracaoRemessaCampoMap : ClassMap<ConfiguracaoRemessaCampo>
    {
        public ConfiguracaoRemessaCampoMap()
        {
            this.Table("EXPORTFIELD");

            this.Id(x => x.Id).Column("EXPORTFIELD_CODE").GeneratedBy.Native("SEQ_EXPORTFIELD");
            this.Map(x => x.NomeExibicao).Column("TDCAMPOS_DESCEXPORT");
            this.Map(x => x.Ordem).Column("EXPORTFIELD_ORDER");
            this.Map(x => x.ValorFixo).Column("EXPORTFIELD_VALORFIXO");
            this.Map(x => x.Mascara).Column("EXPORTFIELD_FORMAT");

            this.References(x => x.ConfiguracaoRemessa).Column("EXPORTCONFIG_CODE");
            this.References(x => x.Campo).Column("TDCAMPOS_CODE");
        }
    }
}
