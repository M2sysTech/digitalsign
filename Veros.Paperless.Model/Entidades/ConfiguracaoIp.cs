namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class ConfiguracaoIp : Entidade
    {
        public const string TagFileTransfer = "FILE";
        public const string TagMonitorOcr = "MONOCR";

        public virtual int Porta
        {
            get;
            set;
        }

        public virtual string Tag
        {
            get;
            set;
        }

        public virtual string Host
        {
            get; 
            set;
        }

        public virtual int DataCenterId
        {
            get;
            set;
        }

        public virtual string PassPhrase
        {
            get;
            set;
        }

        public virtual string RemotePath
        {
            get;
            set;
        }

        public virtual string SenhaSsh
        {
            get;
            set;
        }

        public virtual string UsuarioSsh
        {
            get;
            set;
        }

        public virtual bool UtilizaParDeChaves
        {
            get;
            set;
        }

        public virtual string CaminhoChavePrivada
        {
            get;
            set;
        }

        public virtual string EnderecoWeb
        {
            get;
            set;
        }

        public virtual bool ConfiguracaoSshValida()
        {
            return string.IsNullOrEmpty(this.RemotePath) == false &&
                string.IsNullOrEmpty(this.SenhaSsh) == false &&
                string.IsNullOrEmpty(this.UsuarioSsh) == false;
        }

        public virtual string EnderecoWebValido()
        {
            return string.IsNullOrEmpty(this.EnderecoWeb) ?
                this.Host :
                this.EnderecoWeb;
        }
    }
}