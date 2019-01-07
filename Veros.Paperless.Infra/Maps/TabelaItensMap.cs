namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class TabelaItensMap : ClassMap<TabelaItens>
    {
        public TabelaItensMap()
        {
            this.Table("TABELAITENS");
            this.Id(x => x.Id).Column("TITENS_CODE");
            this.Map(x => x.Identificador).Column("TITENS_ID");
            this.Map(x => x.Chave).Column("TITENS_CHAVE");
            this.Map(x => x.Descricao).Column("TITENS_DESC");
            this.Map(x => x.Codigo).Column("TITENS_CODE");
        }
    }
}
