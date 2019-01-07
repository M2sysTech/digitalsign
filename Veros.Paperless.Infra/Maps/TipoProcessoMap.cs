namespace Veros.Paperless.Infra.Maps
{
    using Data.Hibernate;
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class TipoProcessoMap : ClassMap<TipoProcesso>
    {
        public TipoProcessoMap()
        {
            this.ReadOnly();
            this.Table("TYPEPROC");
            this.Id(x => x.Id).Column("TYPEPROC_CODE").GeneratedBy.Native("SEQ_TYPEPROC");
            this.Map(x => x.Descricao).Column("TYPEPROC_DESC");
            this.Map(x => x.Prioridade).Column("TYPEPROC_PRIORITY");
            this.Map(x => x.CapturaParcial).Column("TYPEPROC_CAPTURAPARCIAL").BooleanoSimNao();
            this.Map(x => x.Code).Column("TYPEPROC_ID");
        }
    }
}
