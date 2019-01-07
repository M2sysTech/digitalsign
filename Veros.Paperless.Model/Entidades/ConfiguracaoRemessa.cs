namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;
    using Iesi.Collections.Generic;
    using System.Collections.Generic;

    public class ConfiguracaoRemessa : Entidade
    {
        public ConfiguracaoRemessa()
        {
            this.ConfiguracoesDosCampos = new List<ConfiguracaoRemessaCampo>();
        }

        public virtual string Arquivo
        {
            get;
            set;
        }

        public virtual string Cabecalho
        {
            get;
            set;
        }

        public virtual string Rodape
        {
            get;
            set;
        }

        public virtual string SeparadorDeColuna
        {
            get;
            set;
        }

        public virtual int TipoDeProcesso
        {
            get;
            set;
        }

        public virtual List<ConfiguracaoRemessaCampo> ConfiguracoesDosCampos
        {
            get;
            set;
        }

        public virtual string CabecalhoTexto
        {
            get;
            set;
        }

        public virtual string RodapeTexto
        {
            get;
            set;
        }

        public virtual bool UsarCabeçalho()
        {
            return this.Cabecalho == "S";
        }

        public virtual bool UsarRodape()
        {
            return this.Rodape == "S";
        }
    }
}
