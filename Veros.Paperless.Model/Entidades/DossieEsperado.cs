namespace Veros.Paperless.Model.Entidades
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Modelo;
    using Iesi.Collections.Generic;

    [Serializable]
    public class DossieEsperado : Entidade
    {
        public const string Conferido = "C";
        public const string NaoConferido = "N";
        public const string EmConferencia = "E";

        public DossieEsperado()
        {
            this.Lotes = new List<Lote>();
        }

        public virtual Pacote Pacote { get; set; }

        public virtual string MatriculaAgente { get; set; }

        public virtual string NumeroContrato { get; set; }

        public virtual string Hipoteca { get; set; }

        public virtual string NomeDoMutuario { get; set; }

        public virtual string CodigoDeBarras { get; set; }

        public virtual string UfArquivo { get; set; }

        public virtual string Situacao { get; set; }

        public virtual string Status { get; set; }

        public virtual string NomeArquivoOrigem { get; set; }

        public virtual DateTime? DataImportacao { get; set; }

        public virtual string LinhaDoArquivo { get; set; }

        public virtual int NumeroDaLinha { get; set; }

        public virtual bool ProcessadoPeloImportador { get; set; }

        public virtual Ocorrencia UltimaOcorrencia { get; set; }

        public virtual string Identificacao { get; set; }        

        public virtual int PacoteId
        {
            get
            {
                return this.Pacote != null ? this.Pacote.Id : 0;
            }

            set
            {
                this.Pacote = new Pacote { Id = value };
            }
        }

        public virtual List<Lote> Lotes { get; set; }

        public virtual Coleta Coleta { get; set; }

        public virtual int LoteId()
        {
            var lote = this.Lote();

            return lote != null ? lote.Id : 0;
        }

        public virtual int QtdPaginas()
        {
            var processo = this.Processo();
            return processo == null ? 0 : processo.QtdePaginas;
        }

        public virtual int TipoDeProcessoId()
        {
            var processo = this.Processo();
            return processo == null ? 0 : processo.TipoDeProcesso.Id;
        }

        public virtual Processo Processo()
        {
            var lote = this.Lote();

            if (lote == null || lote.Id < 1)
            {
                return null;
            }

            return lote.Processos.FirstOrDefault();
        }

        public virtual string IdentificacaoFormatada()
        {
            return string.Format("{0}.{1}/{2}", 
                this.MatriculaAgente, 
                this.NumeroContrato, 
                this.Hipoteca);
        }

        public virtual string CodigoDeBarra()
        {
            return string.Format("{0}{1}{2}",
                this.MatriculaAgente,
                this.NumeroContrato,
                this.Hipoteca);
        }

        public virtual string LoteStatus()
        {
            var lote = this.Lote();

            return lote == null ? string.Empty : lote.Status.Value;
        }

        public virtual Lote Lote()
        {
            if (this.Lotes == null || this.Lotes.Any() == false)
            {
                return null;
            }

            return this.Lotes.FirstOrDefault();
        }
    }
}
