namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Model.Entidades;

    public class TagMap : ClassMap<Tag>
    {
        public TagMap()
        {
            this.Table("TAG");

            this.Id(x => x.Id).Column("TAG_CODE").GeneratedBy.Native("SEQ_TAG");
            this.Map(x => x.Descricao).Column("TAG_DESC");
            this.Map(x => x.Valor).Column("TAG_VALUE");
            this.Map(x => x.Chave).Column("TAG_CHAVE");
        }
    }
}
