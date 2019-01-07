namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class RankReclassificacaoMap : ClassMap<RankingReclassificacao>
    {
        public RankReclassificacaoMap()
        {
            this.Table("RANKRECLASSIFICACAO");
            this.Id(x => x.Id).Column("RANKRECLASSIFICACAO_CODE").GeneratedBy.Native("SEQ_RANKRECLASSIFICACAO");
            this.Map(x => x.Quantidade).Column("QTD");
            this.References(x => x.TipoDocumento).Column("TYPEDOC_ID");
        }
    }
}