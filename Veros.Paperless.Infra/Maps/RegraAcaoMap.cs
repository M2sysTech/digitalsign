namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class RegraAcaoMap : ClassMap<RegraAcao>
    {
        public RegraAcaoMap()
        {
            this.Table("REGRAACAO");
            this.Id(x => x.Id).Column("REGRAACAO_CODE").GeneratedBy.Native("SEQ_REGRAACAO");
            this.Map(x => x.ColunaOrigem).Column("REGRAACAO_COLUNAORIGEM");
            this.Map(x => x.ColunaDestino).Column("REGRAACAO_COLUNADESTINO");
            this.Map(x => x.ValorFixo).Column("REGRAACAO_VALORFIXO");
            this.Map(x => x.TipoOrigem).Column("REGRAACAO_TIPOORIGEM");
            this.References(x => x.Regra).Column("REGRA_CODE");
            this.References(x => x.Origem).Column("REGRAACAO_CODEORIGEM");
            this.References(x => x.Destino).Column("REGRAACAO_CODEDESTINO");
        }
    }
}
