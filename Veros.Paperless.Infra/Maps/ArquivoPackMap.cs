namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class ArquivoPackMap : ClassMap<ArquivoPack>
    {
        public ArquivoPackMap()
        {
            this.Table("ARQUIVOPACK");
            this.Id(x => x.Id).Column("ARQUIVOPACK_CODE").GeneratedBy.Native("SEQ_ARQUIVOPACK");
            this.References(x => x.PacoteProcessado).Column("PACOTEPROCESSADO_CODE");
            this.Map(x => x.Data).Column("ARQUIVOPACK_DATA");
            this.Map(x => x.NomePacote).Column("ARQUIVOPACK_NOMEPACOTE");
            this.Map(x => x.Tamanho).Column("ARQUIVOPACK_TAMANHO");
            this.Map(x => x.Status).Column("ARQUIVOPACK_STATUS");
            this.Map(x => x.Observacao).Column("ARQUIVOPACK_OBSERVACAO");
        }
    }
}
