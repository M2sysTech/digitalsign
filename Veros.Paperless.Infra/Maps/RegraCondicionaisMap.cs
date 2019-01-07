namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Model.Entidades;

    public class RegraCondicionaisMap : ClassMap<RegraCondicional>
    {
        public RegraCondicionaisMap()
        {
            this.Table("regracond");
            this.Id(x => x.Id).Column("regracond_code").GeneratedBy.Native("SEQ_REGRACOND");
            
            this.References(x => x.Regra).Column("regra_code");
            this.References(x => x.TipoDocumento).Column("typedoc_id");
            this.References(x => x.CampoParaComparar).Column("tdcampos_codecomparar");
            this.References(x => x.Campo).Column("tdcampos_code");

            this.Map(x => x.Binario).Column("regracond_binario");
            this.Map(x => x.Conectivo).Column("regracond_operlogico");
            this.Map(x => x.ValorFixo).Column("regracond_valorfixo");
            this.Map(x => x.Coluna).Column("tdcampos_coluna");
            this.Map(x => x.ColunaParaComparar).Column("tdcampos_colunacomparar");
            this.Map(x => x.OperadorMatematico).Column("regracond_opermatematico");
            this.Map(x => x.FatorMatematico).Column("regracond_fatormatematico");
            this.Map(x => x.Funcao).Column("regracond_funcao");
            this.Map(x => x.FuncaoComparar).Column("regracond_funcaocomparar");
            this.Map(x => x.Ordem).Column("regracond_order");
        }
    }
}
