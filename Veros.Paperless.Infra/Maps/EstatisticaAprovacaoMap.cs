namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class EstatisticaAprovacaoMap : ClassMap<EstatisticaAprovacao>
    {
        public EstatisticaAprovacaoMap()
        {
            this.Table("ESTATISTICAAPROVACAO");
            this.Id(x => x.Id).Column("ESTATISTICAAPROVACAO_CODE").GeneratedBy.Native("SEQ_ESTATISTICAAPROVACAO");
            this.References(x => x.Usuario).Column("USR_CODE");
            this.Map(x => x.DataRegistro).Column("ESTATISTICAAPROVACAO_DATE");
            this.Map(x => x.TotalDevolvidas).Column("TOTAL_DEVOLVIDAS");
            this.Map(x => x.TotalLiberadas).Column("TOTAL_LIBERADAS");
            this.Map(x => x.TotalFraudes).Column("TOTAL_FRAUDE");
            this.Map(x => x.TotalAbandonadas).Column("TOTAL_ABANDONADAS");
            this.Map(x => x.TotalDevolvidasComFraude).Column("TOTAL_DEVOLVIDAS_FRAUDE");
            this.Map(x => x.TotalLiberadasComFraude).Column("TOTAL_LIBERADAS_FRAUDE");
        }
    }
}
