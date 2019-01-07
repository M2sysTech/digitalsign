namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class Tag : Entidade
    {
        public static string CaminhoArquivoColeta = "web.caminhoarquivoColeta";
        public static string PercentualQualidadeCef = "QUALIDADE_PORCENTAGEM_CEF";
        public static string UtilizarImgDoLinux = "web.usarimglinux";
        public static string DimensaoThumb = "DIMENSAO_THUMB";

        public virtual string Descricao
        {
            get;
            set;
        }

        public virtual string Valor
        {
            get;
            set;
        }

        public virtual string Chave
        {
            get;
            set;
        }
    }
}
