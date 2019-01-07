namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class ConfiguracaoRemessaMap : ClassMap<ConfiguracaoRemessa>
    {
        public ConfiguracaoRemessaMap()
        {
            this.Table("EXPORTCONFIG");

            this.Id(x => x.Id).Column("EXPORTCONFIG_CODE").GeneratedBy.Native("SEQ_EXPORTCONFIG");
            this.Map(x => x.Arquivo).Column("EXPORTCONFIG_FILENAME");
            this.Map(x => x.Cabecalho).Column("EXPORTCONFIG_CABECALHO");
            this.Map(x => x.CabecalhoTexto).Column("EXPORTCONFIG_CABECALHOTEXTO");
            this.Map(x => x.Rodape).Column("EXPORTCONFIG_RODAPE");
            this.Map(x => x.RodapeTexto).Column("EXPORTCONFIG_RODAPETEXTO");
            this.Map(x => x.SeparadorDeColuna).Column("EXPORTCONFIG_SEPARADORCOLUNA");
            this.Map(x => x.TipoDeProcesso).Column("TYPEPROC_CODE");

            this.HasMany(x => x.ConfiguracoesDosCampos)
                .KeyColumn("EXPORTCONFIG_CODE")
                .Inverse()
                .Cascade.None();
        }
    }
}
