namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class PacoteProcessadoMap : ClassMap<PacoteProcessado>
    {
        public PacoteProcessadoMap()
        {
            this.Table("PACOTEPROCESSADO");
            this.Id(x => x.Id).Column("PACOTEPROCESSADO_CODE").GeneratedBy.Native("SEQ_PACOTEPROCESSADO");
            this.Map(x => x.Arquivo).Column("PACOTEPROCESSADO_ARQUIVO");
            this.Map(x => x.ArquivoImportadoEm).Column("PACOTEPROCESSADO_IMPORTADOEM");
            this.Map(x => x.ArquivoRecebidoEm).Column("PACOTEPROCESSADO_RECEBIDOEM");
            this.Map(x => x.EnviadoEm).Column("PACOTEPROCESSADO_ENVIADOEM");
            this.Map(x => x.FimRecepcao).Column("PACOTEPROCESSADO_FIMRECEPCAO");
            this.Map(x => x.UltimoArquivoRecebido).Column("PACOTEPROCESSADO_ULTIMOPACK");
            this.Map(x => x.ContaRecepcionadas).Column("PACOTEPROCESSADO_TOTALCONTAS");
            this.Map(x => x.StatusPacote).Column("PACOTEPROCESSADO_STATUS");
            this.Map(x => x.Faturado).Column("FATURADO");
            this.Map(x => x.Ativado).Column("PACOTEPROCESSADO_ATIVADO");
            this.Map(x => x.ExibirNaQualidadeCef).Column("PACOTEPROCESSADO_QUALICEF");
            
            this.HasMany(x => x.Lotes)
            .KeyColumn("PACOTEPROCESSADO_CODE")
            .Cascade.None()
            .Inverse();
        }
    }
}
