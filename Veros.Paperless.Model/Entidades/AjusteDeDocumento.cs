namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework.Modelo;

    public class AjusteDeDocumento : Entidade
    {
        public static string SituacaoAberto = "SA";
        public static string SituacaoFechado = "G0";
        public static string SituacaoErro = "GX";

        public virtual Documento Documento
        {
            get;
            set;
        }

        public virtual int Pagina
        {
            get;
            set;
        }

        public virtual TipoDocumento TipoDocumentoNovo
        {
            get;
            set;
        }

        public virtual AcaoAjusteDeDocumento Acao
        {
            get;
            set;
        }

        public virtual string Status
        {
            get;
            set;
        }

        public virtual DateTime DataRegistro
        {
            get;
            set;
        }

        public virtual DateTime? DataFim
        {
            get;
            set;
        }

        public virtual Usuario UsuarioCadastro
        {
            get;
            set;
        }

        public virtual int ProcessoId
        {
            get
            {
                return this.Documento.Processo.Id;
            }
        }

        public virtual int LoteId
        {
            get
            {
                return this.Documento.Lote.Id;
            }
        }

        public virtual string AcaoDetalhada()
        {
            if (this.Acao == AcaoAjusteDeDocumento.Reclassificar)
            {
                return string.Format("{0} - {1}", this.Acao.DisplayName, this.TipoDocumentoNovo.Description);
            }

            return this.Acao.DisplayName;
        }
    }
}
