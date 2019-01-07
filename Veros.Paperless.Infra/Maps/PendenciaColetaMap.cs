namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class PendenciaColetaMap : ClassMap<PendenciaColeta>
    {
        public PendenciaColetaMap()
        {
            this.Table("PENDENCIACOLETA");
            this.Id(x => x.Id).Column("PENDCOL_CODE").GeneratedBy.Native("SEQ_PENDENCIACOLETA");
            this.References(x => x.ArquivoColeta).Column("ARQVCOL_CODE");
            this.Map(x => x.Tipo).Column("PENDCOL_TIPO");
            this.Map(x => x.Texto).Column("PENDCOL_TEXTO");
            this.Map(x => x.Ordem).Column("PENDCOL_ORDEM");
            this.Map(x => x.ProcessoCsv).Column("PENDCOL_PROCESSOCSV");
            this.Map(x => x.FolderCsv).Column("PENDCOL_FOLDERCSV");
            this.Map(x => x.CaixaCsv).Column("PENDCOL_CAIXACSV");
            this.Map(x => x.QuantidadeDossieCsv).Column("PENDCOL_QTDDOSSIECSV");
            this.Map(x => x.ProcessoBd).Column("PENDCOL_PROCESSOBD");
            this.Map(x => x.FolderBd).Column("PENDCOL_FOLDERBD");
            this.Map(x => x.CaixaBd).Column("PENDCOL_CAIXABD");
            this.Map(x => x.QuantidadeDossieBd).Column("PENDCOL_QTDDOSSIEBD");
            this.References(x => x.ColetaBd).Column("COLETA_CODEBD");
            this.Map(x => x.StatusBd).Column("PENDCOL_STATUSBD");
            this.Map(x => x.DataAnalise).Column("PENDCOL_DATAANALISE");
            this.Map(x => x.StatusDaPendencia).Column("PENDCOL_STATUS");            
        }
    }
}
