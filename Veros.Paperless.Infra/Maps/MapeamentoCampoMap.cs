namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class MapeamentoCampoMap : ClassMap<MapeamentoCampo>
    {
        public MapeamentoCampoMap()
        {
            this.Table("MAPPEDFIELD");
            this.Id(x => x.Id).Column("MAPPEDFIELD_CODE").GeneratedBy.Native("SEQ_MAPPEDFIELD");
            this.Map(x => x.NomeCampoNoTemplate).Column("MAPPEDFIELD_ABBYYFIELDNAME");
            this.Map(x => x.NomeTemplate).Column("MAPPEDFIELD_ABBYYTEMPLATENAME");
            this.References(x => x.Campo).Column("TDCAMPOS_CODE");
        }
    }
}