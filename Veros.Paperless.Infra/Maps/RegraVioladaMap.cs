namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class RegraVioladaMap : ClassMap<RegraViolada>
    {
        public RegraVioladaMap()
        {
            this.Table("REGRAPROC");
            this.Id(x => x.Id).Column("REGRAPROC_CODE").GeneratedBy.Native("SEQ_REGRAPROC");

            this.Map(x => x.Status).Column("REGRAPROC_STATUS");
            ////TODO: Acredito que o correto seria criar o objeto RegraImportada
            this.Map(x => x.Vinculo).Column("REGRAIMP_VINCULO");
            this.Map(x => x.CpfParticipante).Column("REGRAPROC_CPF");
            this.Map(x => x.SequencialDoTitular).Column("REGRAPROC_SEQTIT");
            this.Map(x => x.SomaDoBinario).Column("REGRACOND_BINARIO");
            this.Map(x => x.Observacao).Column("REGRAPROC_OBS");
            this.Map(x => x.Revisada).Column("REGRAPROC_REVISADA");
            this.Map(x => x.Pagina).Column("REGRAPROC_PAGINA");
            this.Map(x => x.Hora).Column("REGRAPROC_DTSTART");

            this.References(x => x.Usuario).Column("USR_CODE");
            this.References(x => x.Processo).Column("PROC_CODE");
            this.References(x => x.Regra).Column("REGRA_CODE");
            this.References(x => x.Documento).Column("MDOC_CODE");
        }
    }
}
