namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class ArquivoColetaMap : ClassMap<ArquivoColeta>
    {
        public ArquivoColetaMap()
        {
            this.Table("ARQUIVOCOLETA");
            this.Id(x => x.Id).Column("ARQVCOL_CODE").GeneratedBy.Native("SEQ_ARQUIVOCOLETA");
            this.References(x => x.Coleta).Column("COLETA_CODE");
            this.References(x => x.UsuarioUpado).Column("USR_CODE");
            this.Map(x => x.DataUpado).Column("ARQVCOL_DATETIME");
            this.Map(x => x.NomeArquivo).Column("ARQVCOL_NOME");
            this.Map(x => x.Status).Column("ARQVCOL_STATUS");
            this.Map(x => x.TamanhoBytes).Column("ARQVCOL_TAMANHOBYTES");
        }
    }
}
