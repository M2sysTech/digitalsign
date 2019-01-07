namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class RecepcaoMap : ClassMap<Recepcao>
    {
        public RecepcaoMap()
        {
            this.Table("recepcao");
            this.Id(x => x.Id).Column("recepcao_code").GeneratedBy.Native("seq_recepcao");
            this.Map(x => x.CadastradoEm).Column("cadastrado_em");
            this.Map(x => x.UsuarioId).Column("usr_code");
            this.Map(x => x.QuantidadeImportado).Column("qtde_importado");
            this.Map(x => x.QuantidadeSolicitado).Column("qtde_solicitado");
            this.Map(x => x.Status).Column("status");
            this.Map(x => x.FinalizadoEm).Column("finalizado_em");
        }
    }
}
