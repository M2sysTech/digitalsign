namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class OcorrenciaTipoMap : ClassMap<OcorrenciaTipo>
    {
        public OcorrenciaTipoMap()
        {
            this.Table("TIPOOCORRENCIA");
            this.Id(x => x.Id).Column("TIPOOCORRENCIA_CODE").GeneratedBy.Native("SEQ_TIPOOCORRENCIA");
            this.Map(x => x.Nome).Column("TIPOOCORRENCIA_NOME");
            this.Map(x => x.TipoOcorrenciaParaFilhos).Column("TIPOOCORRENCIA_FILHOS");
        }
    }
}
