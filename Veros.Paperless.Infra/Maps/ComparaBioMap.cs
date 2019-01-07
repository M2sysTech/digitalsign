namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class ComparaBioMap : ClassMap<ComparaBio>
    {
        public ComparaBioMap()
        {
            this.Table("COMPARABIO");
            this.Id(x => x.Id).Column("COMPARA_CODE").GeneratedBy.Native("SEQ_COMPARABIO");
            this.Map(x => x.Percentual).Column("COMPARA_PERCENT");
            this.Map(x => x.Status).Column("COMPARA_STATUS");
            this.Map(x => x.Resultado).Column("COMPARA_RESULT");
            this.Map(x => x.HoraInicio).Column("COMPARA_STARTTIME");
            this.References(x => x.Usuario).Column("USR_CODE");
            this.References(x => x.Lote).Column("BATCH_CODE");
            this.References(x => x.Face1).Column("FACE_CODE1");
            this.References(x => x.Face2).Column("FACE_CODE2");
        }
    }
}
