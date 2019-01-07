namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class FileTransfer : Entidade
    {
        public virtual string Tag
        {
            get;
            set;
        }

        public virtual double Tamanho
        {
            get;
            set;
        }

        public virtual double Usado
        {
            get;
            set;
        }

        public virtual ConfiguracaoIp ConfiguracaoIp
        {
            get;
            set;
        }

        public virtual long TamanhoArquivoAtual
        {
            get;
            set;
        }

        public virtual bool EhCloud
        {
            get;
            set;
        }

        public virtual bool AceitaArquivo(long tamanhoArquivoEmBytes)
        {
            var tamanhoEmGiga = tamanhoArquivoEmBytes / 1024 / 1024 / 1024;
            return this.Tamanho > (this.Usado + tamanhoEmGiga);
        }

        public virtual double ArquivoAtualEmGigas()
        {
            return (double)this.TamanhoArquivoAtual / 1024 / 1024 / 1024;
        }
    }
}
