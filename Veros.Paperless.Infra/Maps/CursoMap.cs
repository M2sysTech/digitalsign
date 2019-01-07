namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class CursoMap : ClassMap<Curso>
    {
        public CursoMap()
        {
            this.Table("CURSO");
            this.Id(x => x.Id).Column("CURSO_CODE").GeneratedBy.Assigned();
            this.Map(x => x.Nome).Column("CURSO_NAME");
        }
    }
}