namespace Veros.Paperless.Model.Entidades
{
    using System;
    using System.Linq;
    using Framework.Modelo;

    public class Ocorrencia : Entidade
    {
        public static string AcaoFinalizar = "FO";
        public static string AcaoAdicionarObservacao = "AO";
        public static string AcaoAguardandoCef = "AC";
        public static string AcaoAguardandoM2 = "AM";

        public Ocorrencia()
        {
            this.DataRegistro = DateTime.Now;
        }

        public virtual OcorrenciaTipo Tipo { get; set; }

        public virtual OcorrenciaStatus Status { get; set; }

        public virtual Pacote Pacote { get; set; }

        public virtual DossieEsperado DossieEsperado { get; set; }

        public virtual Lote Lote { get; set; }

        public virtual Documento Documento { get; set; }

        public virtual DateTime? DataRegistro { get; set; }

        public virtual Usuario UsuarioRegistro { get; set; }

        public virtual string Observacao { get; set; }

        public virtual int GrupoId { get; set; }

        public virtual int TipoId
        {
            get
            {
                return this.Tipo == null ? 0 : this.Tipo.Id;
            }

            set
            {
                this.Tipo = value > 0 ? new OcorrenciaTipo { Id = value } : null;
            }
        }

        public virtual int PacoteId
        {
            get
            {
                return this.Pacote == null ? 0 : this.Pacote.Id;
            }

            set
            {
                this.Pacote = value > 0 ? new Pacote { Id = value } : null;
            }
        }

        public virtual int LoteId
        {
            get
            {
                return this.Lote == null ? 0 : this.Lote.Id;
            }

            set
            {
                this.Lote = value > 0 ? new Lote { Id = value } : null;
            }
        }

        public virtual int DossieEsperadoId
        {
            get
            {
                return this.DossieEsperado == null ? 0 : this.DossieEsperado.Id;
            }

            set
            {
                this.DossieEsperado = value > 0 ? new DossieEsperado { Id = value } : null;
            }
        }

        public virtual int DocumentoId
        {
            get
            {
                return this.Documento == null ? 0 : this.Documento.Id;
            }

            set
            {
                this.Documento = value > 0 ? new Documento { Id = value } : null;
            }
        }

        public virtual Usuario UsuarioResponsavel
        {
            get;
            set;
        }

        public virtual DateTime? HoraInicio
        {
            get;
            set;
        }

        public virtual int OcorrenciaPaiId
        {
            get;
            set;
        }

        public virtual int ProcessoId()
        {
            return this.Lote == null || this.Lote.Processos == null ? 0 : this.Lote.Processos.First().Id;
        }

        public virtual string PacoteIdentificacao()
        {
            return this.Pacote != null ? this.Pacote.Identificacao : string.Empty;
        }
    }
}
