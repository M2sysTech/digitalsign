namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class EstoquePortalMap : ClassMap<EstoquePortal>
    {
        public EstoquePortalMap()
        {
            this.Table("ESTOQUEPORTAL");
            this.Id(x => x.Id).Column("ESTOQUEPORTAL_CODE").GeneratedBy.Native("SEQ_ESTOQUEPORTAL");
            this.Map(x => x.Quantidade).Column("QUANTIDADE");
            this.Map(x => x.AtualizadoEm).Column("ATUALIZADOEM");
        }
    }
}
