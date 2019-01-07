namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class AgenciaMap : ClassMap<Agencia>
    {
        public AgenciaMap()
        {
            this.Table("AGENCIA");
            this.Id(x => x.Id).Column("AGENCIA_CODE").GeneratedBy.Native("SEQ_AGENCIA");
            this.Map(x => x.Numero).Column("AGENCIA_NUM");
            this.Map(x => x.Nome).Column("AGENCIA_DESC");
            this.Map(x => x.BancoId).Column("BANCO_CODE");
            this.Map(x => x.MunicCode).Column("MUNIC_CODE");
        }
    }
}
