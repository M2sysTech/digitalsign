namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class RelacionamentoDeCursoMap : ClassMap<RelacionamentoDeCurso>
    {
        public RelacionamentoDeCursoMap()
        {
            this.Table("IEENDERECOCURSO");
            this.Id(x => x.Id).Column("IEENDERECOCURSO_CODE").GeneratedBy.Assigned();
            this.References(x => x.EnderecoDeInstituicaoDeEnsino).Column("IEENDERECO_CODE");
            this.References(x => x.Curso).Column("CURSO_CODE");
        }
    }
}