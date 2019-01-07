namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class ColetaMap : ClassMap<Coleta>
    {
        public ColetaMap()
        {
            this.Table("COLETA");
            this.Id(x => x.Id).Column("COLETA_CODE").GeneratedBy.Native("SEQ_COLETA");
            this.Map(x => x.Data).Column("COLETA_DATA");
            this.Map(x => x.Endereco).Column("COLETA_ENDERECO");
            this.Map(x => x.Descricao).Column("COLETA_DESCRICAO");
            this.Map(x => x.Periodo).Column("COLETA_PERIODO");
            this.Map(x => x.QuantidadeDeCaixasTipo1).Column("COLETA_QTD1");
            this.Map(x => x.QuantidadeDeCaixasTipo2).Column("COLETA_QTD2");
            this.Map(x => x.DataCadastro).Column("COLETA_DTCAD");
            this.Map(x => x.Status).Column("COLETA_STATUS");
            this.Map(x => x.DataColetaRealizada).Column("COLETA_DTREALIZ");
            this.Map(x => x.DataDevolucao).Column("COLETA_DTDEVOLUCAO");
            this.References(x => x.UsuarioCadastro).Column("USR_CAD");
            this.References(x => x.UsuarioRealizaColeta).Column("USR_REALIZ");
            this.Map(x => x.DataRecepcao).Column("COLETA_DTRECEPCAO");
            this.References(x => x.UsuarioRecepcao).Column("USR_RECEP");
            this.References(x => x.Transportadora).Column("TRANSP_CODE");
            this.Map(x => x.Uf).Column("COLETA_UF");
            this.Map(x => x.Arquivo).Column("COLETA_ARQUIVO");

            this.HasMany(x => x.Pacotes)
                .KeyColumn("COLETA_CODE")
                .Cascade.None()
                .Inverse();
        }
    }
}
