namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class BancoMap : ClassMap<Banco>
    {
        public BancoMap()
        {
            this.Table("BANCO");
            this.Id(x => x.Id).Column("BANCO_CODE").GeneratedBy.Native("SEQ_BANCO");
            this.Map(x => x.Numero).Column("BANCO_NUM");
            this.Map(x => x.Nome).Column("BANCO_DESC");
        }
    }
}
