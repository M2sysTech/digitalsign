namespace Veros.Paperless.Model.Entidades
{
    using System;
    using System.Linq;
    using Framework.Modelo;
    using Iesi.Collections.Generic;
    using System.Collections.Generic;

    [Serializable]
    public class Lote : Entidade, IProcessavelPeloWorkflow<LoteStatus>
    {
        public Lote()
        {
            this.Processos = new List<Processo>();
        }

        public virtual LoteStatus Status { get; set; }

        public virtual Pacote Pacote { get; set; }

        public virtual string Batido { get; set; }

        public virtual string Agencia { get; set; }

        public virtual string PrioridadeNasFilas { get; set; }

        public virtual string Grupo { get; set; }

        public virtual DateTime DataCriacao { get; set; }

        public virtual int QuantidadeDocumentos { get; set; }

        public virtual PacoteProcessado PacoteProcessado { get; set; }

        public virtual DateTime? DataFimIcr { get; set; }

        public virtual DateTime? DataFimIdentificacao { get; set; }

        public virtual DateTime? DataFimDaMontagem { get; set; }

        public virtual DateTime? DataFimDigitacao { get; set; }

        public virtual DateTime? DataFimConsulta { get; set; }

        public virtual DateTime? DataFimValidacao { get; set; }

        public virtual DateTime? DataFimProvaZero { get; set; }

        public virtual DateTime? DataFimFormalistica { get; set; }

        public virtual DateTime? DataFimRevisao { get; set; }

        public virtual DateTime? DataAguardandoAprovacao { get; set; }

        public virtual DateTime? DataFimExportacao { get; set; }

        public virtual DateTime? DataFimEnvio { get; set; }

        public virtual DateTime? DataFimRetorno { get; set; }

        public virtual DateTime? DataFinalizacao { get; set; }

        public virtual List<Processo> Processos { get; set; }

        public virtual DateTime? DataFimArquivoXml { get; set; }

        public virtual string Identificacao { get; set; }

        public virtual int SequencialDeSolicitacao { get; set; }

        public virtual int QualidadeM2sys { get; set; }

        public virtual int QualidadeCef { get; set; }

        public virtual Usuario UsuarioCaptura { get;  set; }

        public virtual DateTime? DataFimCaptura { get; set; }

        public virtual bool ConsultaPh3Realizada { get; set; }

        public virtual bool ConsultaVertrosRealizada { get; set; }

        public virtual DossieEsperado DossieEsperado { get; set; }

        public virtual DateTime? DataFaturamento { get; set; }

        public virtual string ResultadoQualidadeCef { get; set; }

        public virtual string StatusAuxiliar { get; set; }

        public virtual string ProblemaQualidade { get; set; }

        /// <summary>
        /// Essa propriedade foi gerada apenas para identificar os lotes que tiveram os termos re-gerados 
        /// Por aplicativo m2 corrigir termos [parametros]
        /// </summary>
        public virtual bool FoiGeradoTermoAvulso
        {
            get;
            set;
        }

        public virtual bool AtualizacaoBrancoFinalizada
        {
            get;
            set;
        }

        public virtual string Hash
        {
            get;
            set;
        }

        public virtual bool Recapturado
        {
            get;
            set;
        }

        public virtual LoteCef LoteCef
        {
            get;
            set;
        }

        public virtual bool JpegsEnviadosParaCloud
        {
            get;
            set;
        }

        public virtual bool CloudOk
        {
            get;
            set;
        }

        public virtual bool RemovidoFileTransferM2
        {
            get;
            set;
        }

        public virtual DateTime? PdfNoCloudEm
        {
            get;
            set;
        }

        public virtual DateTime? JpegNoCloudEm
        {
            get;
            set;
        }

        public virtual DateTime? RemovidoFileTransferEm
        {
            get;
            set;
        }

        public static Lote CriarNovo(
            Pacote pacote,
            LoteStatus status,
            PacoteProcessado pacoteProcessado,
            string identificacao = "")
        {
            return new Lote
            {
                Identificacao = identificacao,
                Pacote = pacote,
                Status = status,
                Batido = "N",
                DataCriacao = DateTime.Now,
                Agencia = pacote.Estacao,
                PrioridadeNasFilas = "N",
                Grupo = string.Format(@"{0:0000}",
                    pacote.Estacao).Substring(3),
                PacoteProcessado = pacoteProcessado
            };
        }

        public virtual bool ProcessosEstaoFinalizados()
        {
            return this.Processos.Any(x => x.Status != ProcessoStatus.Finalizado) == false;
        }

        public virtual int TipoDeProcessoId()
        {
            var processo = this.Processos.FirstOrDefault();
            return processo.TipoDeProcesso.Id;
        }

        public virtual bool FoiTratado()
        {
            return this.DataFimDigitacao != null;
        }

        public virtual bool NaoTemProcessosNasFases(params ProcessoStatus[] status)
        {
            return this.Processos.Any(x => status.Contains(x.Status)) == false;
        }

        public virtual bool EstaFaltandoAlgo()
        {
            return this.Processos.Any() == false || this.Processos.Any(x => x.EstaFaltandoAlgo());
        }

        public virtual string TempoProcessamento()
        {
            var tempo = 0.0;

            var dataInicio = this.DataFimCaptura.HasValue ? this.DataFimCaptura.GetValueOrDefault() : this.DataCriacao;

            if (this.Status == LoteStatus.Finalizado && this.DataFimFormalistica.HasValue)
            {
                tempo = this.DataFimFormalistica.GetValueOrDefault().Subtract(dataInicio).TotalMinutes;
            }
            else
            {
                tempo = DateTime.Now.Subtract(dataInicio).TotalMinutes;
            }

            return string.Format("{0} minutos",
                Math.Truncate(tempo));
        }

        public virtual string NomeDaLoja()
        {
            if (this.UsuarioCaptura == null || this.UsuarioCaptura.Loja == null)
            {
                return string.Empty;
            }

            return this.UsuarioCaptura.Loja.Nome;
        }
    }
}
