namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework.Modelo;

    [Serializable]
    public class Usuario : Entidade, IUsuario
    {
        public const string StatusAtivo = "0";
        public const string StatusExcluido = "*";
        public const int CodigoDoUsuarioSistema = -1;

        public Usuario()
        {
            this.Tipo = "W";
            this.Status = "0";
        }

        public virtual string Login
        {
            get;
            set;
        }

        public virtual string Senha
        {
            get;
            set;
        }

        public virtual string Nome
        {
            get; 
            set;
        }

        public virtual string PerfilSigla
        {
            get;
            set;
        }

        public virtual Perfil Perfil
        {
            get; 
            set;
        }

        public virtual string Status 
        { 
            get; 
            set;
        }

        public virtual string Tipo
        {
            get; 
            set;
        }

        public virtual string Foto
        {
            get;
            set;
        }

        public virtual string LoginENome
        {
            get
            {
                return this.Login + " - " + this.Nome;
            }
        }

        public virtual string ForcarAlteracaoSenha
        {
            get;
            set;
        }

        public virtual string NomeArquivoFoto
        {
            get; 
            set;
        }

        public virtual Loja Loja
        {
            get; 
            set;
        }

        public virtual string Token
        {
            get; 
            set;
        }

        public virtual DateTime? DataUltimoLogin
        {
            get;
            set;
        }

        public virtual DateTime? UltimoAcesso
        {
            get;
            set;
        }

        public virtual string EstaLogado
        {
            get;
            set;
        }
    }
}
