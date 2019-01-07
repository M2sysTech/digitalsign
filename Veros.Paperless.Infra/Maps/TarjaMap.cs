namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class TarjaMap : ClassMap<Tarja>
    {
        public TarjaMap()
        {
            this.Table("TARJA");
            this.Id(x => x.Id).Column("TARJA_CODE").GeneratedBy.Native("SEQ_TARJA");
            this.Map(x => x.QtdeRetangulos).Column("TARJA_QTDRETANGULOS");
            this.Map(x => x.PosicoesRetangulos).Column("TARJA_POSICOES");

            this.References(x => x.Documento).Column("MDOC_CODE");
            this.References(x => x.Pagina).Column("DOC_CODE");
            this.References(x => x.Campo).Column("TDCAMPOS_CODE");
        }
    }
}