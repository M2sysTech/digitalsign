namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class TipoDocumentoMap : ClassMap<TipoDocumento>
    {
        public TipoDocumentoMap()
        {
            this.Table("TYPEDOC");

            this.Id(x => x.Id).Column("TYPEDOC_ID").GeneratedBy.Native("SEQ_TYPEDOC");
            this.Map(x => x.Description).Column("TYPEDOC_DESC");
            this.Map(x => x.IsPac).Column("TYPEDOC_PAC");
            this.Map(x => x.Abreviacao).Column("TYPEDOC_DESCLIMIT");
            this.Map(x => x.TypeDocCode).Column("TYPEDOC_CODE");
            this.Map(x => x.PassivelDeFraude).Column("TYPEDOC_FRAUDAVEL");
            this.Map(x => x.PassivelDeReclassificacao).Column("TYPEDOC_RECLASSIFICAVEL");
            this.Map(x => x.QuantidadeDePaginas).Column("TYPEDOC_QTDPAG");
            this.References(x => x.Ranking).Column("TYPEDOC_RANKING");
            this.Map(x => x.DataCriacao).Column("TYPEDOC_DATACRIACAO");
            this.Map(x => x.CodigoOcorrencia).Column("OCORRENCIA_CODE");
            this.Map(x => x.GrupoId).Column("TYPEDOC_CODEGRUPO");
        }
    }
}
