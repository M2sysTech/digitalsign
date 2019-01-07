namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class FaceMap : ClassMap<Face>
    {
        public FaceMap()
        {
            this.Table("FACE");
            this.Id(x => x.Id).Column("FACE_CODE").GeneratedBy.Native("SEQ_FACE");
            this.References(x => x.Pagina).Column("DOC_CODE");
            this.Map(x => x.TipoDeArquivo).Column("FACE_FILETYPE");
            this.Map(x => x.NomeArquivo).Column("FACE_ARQUIVO");
            this.Map(x => x.StatusDaComparacao).Column("FACE_STATUSCOMP");
            this.Map(x => x.Cpf).Column("FACE_CPF");
            this.Map(x => x.Comum).Column("FACE_COMUM");
            this.Map(x => x.ListaNegra).Column("FACE_BLACKLIST");
        }
    }
}
