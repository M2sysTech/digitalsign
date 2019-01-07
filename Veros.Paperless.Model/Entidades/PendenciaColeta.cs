namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework.Modelo;

    public class PendenciaColeta : Entidade
    {
        public const string TipoArquivoCsv = "CSV";
        public const string TipoDossie = "DOS";
        public const string TipoCaixa = "CAX";

        public const string SubTipoQuantidadeDeCaixas = "QCX";
        public const string SubTipoQuantidadeDeCaixasDuplicadas = "QCD";
        public const string SubTipoQuantidadeDeDossies = "QDO";

        public const string StatusAtiva = "AT";
        public const string StatusExcluida = "EX";

        public virtual DateTime DataAnalise { get; set; }

        public virtual ArquivoColeta ArquivoColeta { get; set; }

        public virtual string Tipo { get; set; }

        public virtual string Texto { get; set; }

        public virtual int Ordem { get; set; }

        public virtual string ProcessoCsv { get; set; }

        public virtual string FolderCsv { get; set; }

        public virtual string CaixaCsv { get; set; }

        public virtual string QuantidadeDossieCsv { get; set; }

        public virtual string ProcessoBd { get; set; }

        public virtual string FolderBd { get; set; }

        public virtual string CaixaBd { get; set; }

        public virtual string QuantidadeDossieBd { get; set; }

        public virtual string StatusBd { get; set; }

        public virtual Coleta ColetaBd { get; set; }

        public virtual string StatusDaPendencia { get; set; }

        public virtual string SubTipo { get; set; }

        public virtual string IdentificacaoColeta()
        {
            return this.ColetaBd == null ?
                string.Empty :
                this.ColetaBd.Identificacao();
        }
    }
}
