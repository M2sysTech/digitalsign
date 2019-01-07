namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class ProcessoMap : ClassMap<Processo>
    {
        public ProcessoMap()
        {
            this.Table("PROC");
            this.Id(x => x.Id).Column("PROC_CODE").GeneratedBy.Native("SEQ_PROC");
            this.Map(x => x.Agencia).Column("PROC_AGENCIA");
            this.Map(x => x.Conta).Column("PROC_CONTA");
            this.Map(x => x.Status).Column("PROC_STATUS");
            this.Map(x => x.ObservacaoProcesso).Column("PROC_OBS");
            this.Map(x => x.HoraInicio).Column("PROC_STARTTIME");
            this.Map(x => x.Decisao).Column("PROC_DECISAO");
            this.Map(x => x.TimeStamp).Column("PROC_TIMESTAMP");
            this.Map(x => x.AcaoClassifier).Column("PROC_ACAO");
            this.Map(x => x.Identificacao).Column("PROC_IDENTIFICACAO");
            this.Map(x => x.QtdePaginas).Column("PROC_QTDPAGINAS");
            this.Map(x => x.Barcode).Column("PROC_BARCODE");
            this.Map(x => x.Marca).Column("PROC_MARCA");
            this.Map(x => x.HoraInicioAjuste).Column("PROC_STARTAJUSTE");

            this.References(x => x.Lote).Column("BATCH_CODE");
            this.References(x => x.UsuarioResponsavel).Column("USR_RESP");
            this.References(x => x.TipoDeProcesso).Column("TYPEPROC_CODE");

            this.HasMany(x => x.Documentos)
                .KeyColumn("PROC_CODE")
                .Cascade.None()
                .Inverse();

            this.HasMany(x => x.RegrasVioladas)
                .KeyColumn("PROC_CODE")
                .Cascade.None()
                .Inverse();

            this.HasMany(x => x.Remessas)
                .KeyColumn("PROC_CODE")
                .Cascade.None()
                .Inverse();

            this.DynamicUpdate();
        }
    }
}