namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class FileTransferMap : ClassMap<FileTransfer>
    {
        public FileTransferMap()
        {
            this.Table("FILETRANSFER");
            this.Id(x => x.Id).Column("FILETRANSFER_CODE").GeneratedBy.Native("SEQ_FILETRANSFER");
            this.Map(x => x.Tag).Column("FILETRANSFER_TAG");
            this.Map(x => x.Tamanho).Column("FILETRANSFER_SIZE");
            this.Map(x => x.Usado).Column("FILETRANSFER_USED");
            this.Map(x => x.EhCloud).Column("FILETRANSFER_CLOUD");
        }
    }
}
