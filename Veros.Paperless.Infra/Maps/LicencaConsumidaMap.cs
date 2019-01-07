namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class LicencaConsumidaMap : ClassMap<LicencaConsumida>
    {
        public LicencaConsumidaMap()
        {
            this.Table("LICENCACONSUMIDA");
            this.Id(x => x.Id).Column("LICENCACONSUMIDA_CODE").GeneratedBy.Native("SEQ_LICENCACONSUMIDA");
            this.Map(x => x.Quantidade).Column("LICENCACONSUMIDA_QTDE");
            this.References(x => x.Pagina).Column("DOC_CODE");
        }
    }
}
