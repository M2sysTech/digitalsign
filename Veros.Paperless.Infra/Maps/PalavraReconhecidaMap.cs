namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class PalavraReconhecidaMap : ClassMap<PalavraReconhecida>
    {
        public PalavraReconhecidaMap()
        {
            this.Table("PALAVRARECONHECIDA");
            this.Id(x => x.Id).Column("PALAVRARECONHECIDA_CODE").GeneratedBy.Native("SEQ_PALAVRARECONHECIDA");
            this.Map(x => x.Direita).Column("PALAVRARECONHECIDA_DIREITA");
            this.Map(x => x.Altura).Column("PALAVRARECONHECIDA_ALTURA");
            this.Map(x => x.Esquerda).Column("PALAVRARECONHECIDA_ESQUERDA");
            this.Map(x => x.Largura).Column("PALAVRARECONHECIDA_LARGURA");
            this.Map(x => x.Texto).Column("PALAVRARECONHECIDA_TEXTO");
            this.Map(x => x.Topo).Column("PALAVRARECONHECIDA_TOPO");
            this.References(x => x.Pagina).Column("DOC_CODE");
        }
    }
}