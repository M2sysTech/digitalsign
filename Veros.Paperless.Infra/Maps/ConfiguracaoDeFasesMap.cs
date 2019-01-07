namespace Veros.Paperless.Infra.Maps
{
    using Data.Hibernate;
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    /// <summary>
    /// TODO: teste
    /// </summary>
    public class ConfiguracaoDeFasesMap : ClassMap<ConfiguracaoDeFases>
    {
        public ConfiguracaoDeFasesMap()
        {
            this.Table("CONFIGFASES");
            this.Id(x => x.Id).Column("CONFIGFASES_CODE").GeneratedBy.Native("SEQ_CONFIGFASES");
            this.Map(x => x.IdentificacaoAtivo).Column("CONFIGFASES_IDENTIFICACAO").BooleanoSimNao();
            this.Map(x => x.MontagemAtivo).Column("CONFIGFASES_MONTAGEM").BooleanoSimNao();
            this.Map(x => x.ReconhecimentoAtivo).Column("CONFIGFASES_OCR").BooleanoSimNao();
            this.Map(x => x.FormalisticaAtiva).Column("CONFIGFASES_FORMALISTICA").BooleanoSimNao();
            this.Map(x => x.ValidacaoAtivo).Column("CONFIGFASES_VALIDACAO").BooleanoSimNao();
            ////this.Map(x => x.AjusteOrigemAtivo).Column("CONFIGFASES_AJUSTEORIGEM").BooleanoSimNao();
            ////this.Map(x => x.ConsultaAtiva).Column("CONFIGFASES_CONSULTA").BooleanoSimNao();
            this.Map(x => x.DigitacaoAtivo).Column("CONFIGFASES_DIGITACAO").BooleanoSimNao();
            this.Map(x => x.RetornoAtivo).Column("CONFIGFASES_RETORNO").BooleanoSimNao();
            this.Map(x => x.ExportacaoAtiva).Column("CONFIGFASES_EXPORTACAO").BooleanoSimNao();
            this.Map(x => x.AprovacaoAtivo).Column("CONFIGFASES_APROVACAO").BooleanoSimNao();
            this.Map(x => x.ProvaZeroAtivo).Column("CONFIGFASES_PROVAZERO").BooleanoSimNao();
            this.Map(x => x.RemessaAtivo).Column("CONFIGFASES_REMESSA").BooleanoSimNao();
            this.Map(x => x.EnvioAtivo).Column("CONFIGFASES_REMESSA").BooleanoSimNao();
            this.Map(x => x.ClassifierAtivo).Column("CONFIGFASES_CLASSIFIER").BooleanoSimNao();
            this.Map(x => x.AssinaturaDigitalAtivo).Column("CONFIGFASES_ASSINATURA").BooleanoSimNao();
            this.Map(x => x.TermoDeAtuacao).Column("CONFIGFASES_TERMODEAUTUACAO").BooleanoSimNao();
            this.Map(x => x.TriagemPreOcr).Column("CONFIGFASES_TRIAGEM").BooleanoSimNao();
        }
    }
}
