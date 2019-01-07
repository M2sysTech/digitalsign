namespace Veros.Paperless.Model.Entidades
{
    public class SshContext
    {
        public string Host
        {
            get; 
            set;
        }

        public string Usuario
        {
            get; 
            set;
        }

        public string Senha
        {
            get; 
            set;
        }

        public string RemotePath
        {
            get; 
            set;
        }

        public bool UtilizarSshKeys
        {
            get; 
            set;
        }

        public string CaminhoChavePrivada
        {
            get; 
            set;
        }

        public string PassPhrase
        {
            get; 
            set;
        }
    }
}