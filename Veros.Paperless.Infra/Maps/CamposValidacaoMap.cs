namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class CamposValidacaoMap : ClassMap<CamposValidacao>
    {
        public CamposValidacaoMap()
        {
            this.Table("CAMPOVALIDACAO");
            this.Id(x => x.Id).Column("CAMPOVALIDACAO_CODE").GeneratedBy.Native("SEQ_CAMPOVALIDACAO");
            this.References(x => x.CampoDocumentoMestre).Column("TDCAMPO_DOCUMENTOPRINCIPAL");
            this.References(x => x.CampoDocumentoComprovacao).Column("TDCAMPO_DOCUMENTO");
        }
    }
}
