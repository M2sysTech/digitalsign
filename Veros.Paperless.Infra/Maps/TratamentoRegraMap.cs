namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class TratamentoRegraMap : ClassMap<TratamentoRegra>
    {
        public TratamentoRegraMap()
        {
            this.Table("REGRATRAT");
            this.Id(x => x.Id).Column("REGRATRAT_CODE").GeneratedBy.Native("SEQ_REGRATRAT");
            
            this.Map(x => x.PaginaDocumentoComparacao).Column("TYPEDOC_PAGINACOMPARAR");
            this.Map(x => x.PaginaDocumentoViolado).Column("TYPEDOC_PAGINA");
            
            this.References(x => x.Regra).Column("REGRA_CODE");
            this.References(x => x.TipoDocumentoComparado).Column("TYPEDOC_IDCOMPARAR");
            this.References(x => x.TipoDocumentoViolado).Column("TYPEDOC_ID");
        }
    }
}
