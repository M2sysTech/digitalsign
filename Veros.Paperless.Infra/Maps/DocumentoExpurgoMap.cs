namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Model.Entidades;

    public class DocumentoExpurgoMap : ClassMap<DocumentoExpurgo>
    {
        public DocumentoExpurgoMap()
        {
            this.Table("DOCEXPURGO");
            this.Id(x => x.Id).Column("DOCEXPURGO_CODE").GeneratedBy.Native("SEQ_DOCEXPURGO");
            this.Map(x => x.DocCode).Column("DOCEXPURGO_CODEDOCUMENTO");
            this.Map(x => x.TipoArquivo).Column("DOCEXPURGO_TIPOARQUIVO");
        }
    }
}
