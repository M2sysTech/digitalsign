namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class AjusteDeDocumentoMap : ClassMap<AjusteDeDocumento>
    {
        public AjusteDeDocumentoMap()
        {
            this.Table("AJUSTEMDOC");
            this.Id(x => x.Id).Column("AJUSTEMDOC_CODE").GeneratedBy.Native("SEQ_AJUSTEMDOC");
            this.References(x => x.Documento).Column("MDOC_CODE");
            this.References(x => x.TipoDocumentoNovo).Column("TYPEDOC_ID");
            this.Map(x => x.Pagina).Column("AJUSTEMDOC_PAG");
            this.Map(x => x.Acao).Column("AJUSTEMDOC_ACAO");
            this.Map(x => x.Status).Column("AJUSTEMDOC_STATUS");
            this.Map(x => x.DataRegistro).Column("AJUSTEMDOC_DTREGISTRO");
            this.Map(x => x.DataFim).Column("AJUSTEMDOC_DTFIM");
            this.References(x => x.UsuarioCadastro).Column("USR_CODE");
        }
    }
}
