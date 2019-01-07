namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class ListaNegraCpfMap : ClassMap<ListaNegraCpf>
    {
        public ListaNegraCpfMap()
        {
            this.Table("ListaNegraCpf");
            this.Id(x => x.Id).Column("LISTANEGRACPF_CODE").GeneratedBy.Native("SEQ_LISTANEGRACPF");
            this.Map(x => x.Numero).Column("LISTANEGRACPF_NUMERO");            
        }
    }   
}