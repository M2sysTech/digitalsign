namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Model.Entidades;

    public class ExpurgoConfigMap : ClassMap<ExpurgoConfig>
    {
        public ExpurgoConfigMap()
        {
            this.Table("EXPURGOCONFIG");
            this.Id(x => x.Id).Column("EXPURGOCONFIG_CODE").GeneratedBy.Native("SEQ_FATURAMENTO");
            this.Map(x => x.IntervaloDeDiasBk).Column("EXPURGOCONFIG_INTERVALODIAS");
            this.Map(x => x.IntervaloDeDiasHistorico).Column("EXPURGOCONFIG_INTERVALODIASH");
            this.Map(x => x.DeveApagarImagens).Column("EXPURGOCONFIG_APAGARIMAGENS");
            this.Map(x => x.HoraParaRodar).Column("EXPURGOCONFIG_HORARIO");
            this.Map(x => x.UltimoExpurgoBk).Column("EXPURGOCONFIG_ULTIMOEXPURGO");
            this.Map(x => x.UltimoExpurgo).Column("EXPURGOCONFIG_ULTIMOEXPURGOH");
            this.Map(x => x.QuantidadeDeMovimentosParaNaoExpurgar).Column("EXPURGOCONFIG_MOVIMENTOBASE");
            this.Map(x => x.IntervaloDeDiasParaExclusaoHistorico).Column("EXPURGOCONFIG_INTERVALOEXCL");
            this.Map(x => x.IntervaloDeDiasParaDesfragmentar).Column("EXPURGOCONFIG_DIASDESFRAG");
            this.Map(x => x.UltimaDesfragmentacao).Column("EXPURGOCONFIG_ULTDESFRAGMENT");
            this.Map(x => x.HoraDesfragmentacao).Column("EXPURGOCONFIG_HORADESFRAG");
        }
    }
}
