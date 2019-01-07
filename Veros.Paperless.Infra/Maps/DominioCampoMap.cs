namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class DominioCampoMap : ClassMap<DominioCampo>
    {
        public DominioCampoMap()
        {
            this.Table("TABELAITENS");
            this.Id(x => x.Id).Column("TITENS_CODE").GeneratedBy.Native("SEQ_TABELAITENS");
            this.Map(x => x.Codigo).Column("TITENS_ID");
            this.Map(x => x.Chave).Column("TITENS_CHAVE");
            this.Map(x => x.Descricao).Column("TITENS_DESC");
        }
    }
}