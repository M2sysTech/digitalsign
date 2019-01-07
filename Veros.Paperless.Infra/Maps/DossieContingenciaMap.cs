namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class DossieContingenciaMap : ClassMap<DossieContingencia>
    {
        public DossieContingenciaMap()
        {
            this.Table("DOSSIECONT");
            this.Id(x => x.Id).Column("DOSSIECONT_CODE").GeneratedBy.Native("SEQ_DOSSIECONT");
            this.Map(x => x.Hipoteca).Column("DOSSIECONT_HIPOTECA");
            this.Map(x => x.MatriculaAgente).Column("DOSSIECONT_MATRICULAAGENTE");
            this.Map(x => x.NomeDoMutuario).Column("DOSSIECONT_NOMEMUTUARIO");
            this.Map(x => x.NumeroContrato).Column("DOSSIECONT_NUMEROCONTRATO");
            this.Map(x => x.Situacao).Column("DOSSIECONT_SITUACAO");
            this.Map(x => x.UfArquivo).Column("DOSSIECONT_UFARQUIVO");
            this.Map(x => x.CodigoDeBarras).Column("DOSSIECONT_CODIGOBARRAS");
            this.Map(x => x.CaixaEtiqueta).Column("DOSSIECONT_CAIXAETIQUETA");
            this.Map(x => x.CaixaSequencial).Column("DOSSIECONT_CAIXASEQ");
        }
    }
}
