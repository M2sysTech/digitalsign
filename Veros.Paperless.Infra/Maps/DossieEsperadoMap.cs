namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class DossieEsperadoMap : ClassMap<DossieEsperado>
    {
        public DossieEsperadoMap()
        {
            this.Table("DOSSIEESPERADO");
            this.Id(x => x.Id).Column("DOSSIEESPERADO_CODE").GeneratedBy.Native("SEQ_DOSSIEESPERADO");
            this.Map(x => x.Hipoteca).Column("DOSSIEESPERADO_HIPOTECA");
            this.Map(x => x.MatriculaAgente).Column("DOSSIEESPERADO_MATRICULAAGENTE");
            this.Map(x => x.NomeDoMutuario).Column("DOSSIEESPERADO_NOMEMUTUARIO");
            this.Map(x => x.NumeroContrato).Column("DOSSIEESPERADO_NOMECONTRATO");
            this.Map(x => x.Situacao).Column("DOSSIEESPERADO_SITUACAO");
            this.Map(x => x.UfArquivo).Column("DOSSIEESPERADO_UFARQUIVO");
            this.Map(x => x.CodigoDeBarras).Column("DOSSIEESPERADO_CODIGOBARRAS");
            this.Map(x => x.Status).Column("DOSSIEESPERADO_STATUS");
            this.References(x => x.UltimaOcorrencia).Column("OCORRENCIA_CODE");
            this.References(x => x.Pacote).Column("PACOTE_CODE");
            this.References(x => x.Coleta).Column("COLETA_CODE");

            this.HasMany(x => x.Lotes)
                .KeyColumn("DOSSIEESPERADO_CODE")
                .Cascade.None()
                .Inverse();
        }
    }
}
