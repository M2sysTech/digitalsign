namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class PacoteMap : ClassMap<Pacote>
    {
        public PacoteMap()
        {
            this.Table("PACK");
            this.Id(x => x.Id).Column("PACK_CODE").GeneratedBy.Native("SEQ_PACK");
            this.Map(x => x.Agencia).Column("PACK_AGENCIA");
            this.Map(x => x.Batido).Column("PACK_BATIDO");
            this.Map(x => x.Bdu).Column("PACK_BDU");
            this.Map(x => x.DataMovimento).Column("PACK_DTMOV");
            this.Map(x => x.FromHost).Column("PACK_FROMHOST");
            this.Map(x => x.HoraFim).Column("PACK_HFIM");
            this.Map(x => x.HoraInicio).Column("PACK_HINI");
            this.Map(x => x.Estacao).Column("PACK_STATION");
            this.Map(x => x.Status).Column("PACK_STATUS");
            this.Map(x => x.ToHost).Column("PACK_TOHOST");
            this.Map(x => x.TotEnv).Column("PACK_TOTENV");
            this.Map(x => x.UsuarioQueCapturouId).Column("PACK_USRCAP");
            this.Map(x => x.Identificacao).Column("PACK_IDENTIFICACAO");
            this.Map(x => x.DataConferencia).Column("PACK_DTCONFERENCIA");
            this.Map(x => x.CodigoAlternativo).Column("PACK_CODALTERNATIVO");
            this.Map(x => x.EtiquetaCefGerada).Column("PACK_ETIQUETACEF");
            this.References(x => x.UsuarioConferencia).Column("USR_CONFERENCIA");
            this.References(x => x.Coleta).Column("COLETA_CODE");
            this.Map(x => x.DataDevolucao).Column("PACK_DTDEVOLUCAO");
            this.Map(x => x.DataInicioPreparo).Column("PACK_DTINIPREPARO");
            this.Map(x => x.DataFimPreparo).Column("PACK_DTPREPARO");

            this.References(x => x.UsuarioPreparo2).Column("USR_PREPARO2");
            this.References(x => x.UsuarioPreparo).Column("USR_PREPARO");
            this.References(x => x.UsuarioPreparo3).Column("USR_PREPARO3");
            this.References(x => x.UsuarioPreparo4).Column("USR_PREPARO4");

            this.HasMany(x => x.DossiesEsperados)
                .KeyColumn("PACOTE_CODE")
                .Cascade.None()
                .Inverse();

            this.HasMany(x => x.Lotes)
                .KeyColumn("PACK_CODE")
                .Cascade.None()
                .Inverse();

            this.HasMany(x => x.Ocorrencias)
                .KeyColumn("PACOTE_CODE")
                .Cascade.None()
                .Inverse();
        }
    }
}