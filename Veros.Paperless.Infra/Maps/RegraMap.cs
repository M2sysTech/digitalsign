namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class RegraMap : ClassMap<Regra>
    {
        public RegraMap()
        {
            this.Table("REGRA");
            this.Id(x => x.Id).Column("REGRA_CODE").GeneratedBy.Native("SEQ_REGRA");
            this.Map(x => x.Fase).Column("REGRA_FASE");
            this.Map(x => x.Descricao).Column("REGRA_DESC");
            this.Map(x => x.Identificador).Column("REGRA_IDENTIFICADOR");
            this.Map(x => x.Vinculo).Column("REGRA_VINCULO");
            this.Map(x => x.Classificacao).Column("REGRA_CLASSIF");
            this.Map(x => x.RegraDeFraude).Column("REGRA_FRAUDE");
            this.Map(x => x.Ativada).Column("REGRA_ATIVADA");
            this.Map(x => x.ConectivoLogico).Column("REGRA_CONECLOGICO");
            this.Map(x => x.TipoProcessoCode).Column("TYPEPROC_ID");
            this.Map(x => x.RegraDeRevisao).Column("REGRA_REVISAR");
            this.Map(x => x.ProcessarNoMotor).Column("REGRA_PROCESSARMOTOR");
            this.Map(x => x.DescricaoDaFraude).Column("REGRA_DESCFRAUDE");

            this.HasMany(x => x.Tratamentos)
                .KeyColumn("REGRA_CODE")
                .Cascade.None()
                .Inverse();
        }
    }
}
