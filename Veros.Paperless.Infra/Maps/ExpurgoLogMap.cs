namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Model.Entidades;

    public class ExpurgoLogMap : ClassMap<ExpurgoLog> 
    {
        public ExpurgoLogMap()
        {
            this.Table("EXPURGOLOG");
            this.Id(x => x.Id).Column("EXPURGOLOG_CODE").GeneratedBy.Native("SEQ_EXPURGOLOG");
            this.Map(x => x.Mensagem).Column("EXPURGOLOG_MENSAGEM");
            this.Map(x => x.DataHora).Column("EXPURGOLOG_DATAHORA");
        }
    }
}
