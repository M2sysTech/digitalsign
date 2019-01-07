namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class PendenciaPortalMap : ClassMap<PendenciaPortal>
    {
        public PendenciaPortalMap()
        {
            this.Table("pendenciaportal");
            this.Id(x => x.Id).Column("pendenciaportal_code").GeneratedBy.Native("seq_pendenciaportal");
            this.Map(x => x.SolicitacaoId).Column("solicitacao_id");
            this.Map(x => x.Tentativas).Column("tentativas");
            this.Map(x => x.UltimaTentativa).Column("ultima_tentativa");
            this.Map(x => x.UltimoErro).Column("ultimo_erro");
            this.Map(x => x.Operacao).Column("operacao");
        }
    }
}
