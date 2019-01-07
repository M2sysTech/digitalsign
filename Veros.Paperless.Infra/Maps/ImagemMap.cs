namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class ImagemMap : ClassMap<Imagem>
    {
        public ImagemMap()
        {
            this.Table("IMAGEM");
            this.Id(x => x.Id).Column("IMAGEM_CODE").GeneratedBy.Native("SEQ_IMAGEM");
            this.Map(x => x.Altura).Column("IMAGEM_ALTURA");
            this.Map(x => x.Largura).Column("IMAGEM_LARGURA");
            this.Map(x => x.ResolucaoHorizontal).Column("IMAGEM_RHORIZONTAL");
            this.Map(x => x.ResolucaoVertical).Column("IMAGEM_RVERTICAL");
            this.Map(x => x.Tamanho).Column("IMAGEM_TAMANHO");
            this.Map(x => x.Formato).Column("IMAGEM_FORMATO");
            this.Map(x => x.QuantidadeCores).Column("IMAGEM_CORES");
            this.References(x => x.Pagina).Column("DOC_CODE");
        }
    }
}