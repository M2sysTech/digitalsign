namespace Veros.Paperless.Model.ViewModel
{
    using System;
    using System.Collections.Generic;

    public class DossieStatusViewModel
    {
        private const string SitPreparo = "1 - Preparação/higienização";
        private const string SitCaptura = "2 - Aguardando Captura";
        private const string SitProcessamento = "3 - Em Processamento";
        private const string SitQualiCef = "4 - Aprovação qualidade CEF";
        private const string SitDevolucao = "5 - Para Devolução";
        private const string SitDevolvido = "6 - Devolvido"; 
        
        public int LoteId { get; set; }

        public int ProcessoId { get; set; }

        public int ProcessoTipo { get; set; }

        public string ProcessoNumero { get; set; }

        public string FolderDossie { get; set; }

        public string CodCaixa { get; set; }

        public string LoteStatus { get; set; }

        public string ProcStatus { get; set; }

        public string CaixaStatus { get; set; }

        public int ColetaCode { get; set; }

        public int LoteCefCode { get; set; }

        public string LoteCefStatus { get; set; }

        public int QtdePaginas { get; set; }

        public DateTime? LoteCefDataCriacao { get; set; }

        public DateTime? LoteCefDataAprovacao { get; set; }

        public DateTime? LoteCefDataFim { get; set; }

        public DateTime? CaixaData { get; set; }

        public DateTime? CaixaDataConferencia { get; set; }

        public DateTime? CaixaDataPreparo { get; set; }

        public DateTime? LoteDataTransmissao { get; set; }

        public DateTime? LoteDataOcr { get; set; }

        public DateTime? LoteDataEnvio { get; set; }

        public DateTime? LoteDataReavaliado { get; set; }

        public DateTime? LoteDataFaturamento { get; set; }

        public DateTime? ProcDataStart { get; set; }

        public DateTime? CaixaDataDevolucao { get; set; }

        public int EsperadoCode { get; set; }

        public string EsperadoMatricula { get; set; }

        public string EsperadorHipoteca { get; set; }

        public string EsperadoContrato { get; set; }

        public string UltimaOcorrencia { get; set; }

        private string SituacaoInterna { get; set; }

        private string SituacaoDataInterna { get; set; }

        public string ContratoNumero()
        {
            if (string.IsNullOrEmpty(this.ProcessoNumero))
            {
                return this.EsperadoContrato;
            }
            
            if (this.ProcessoNumero.IndexOf(".") < 0)
            {
                return this.ProcessoNumero;
            }

            if (this.ProcessoNumero.IndexOf(@"/") < 0)
            {
                return this.ProcessoNumero;
            }

            var ponto = this.ProcessoNumero.IndexOf(".");
            var barra = this.ProcessoNumero.IndexOf(@"/");
            return this.ProcessoNumero.Substring(ponto + 1, barra - ponto - 1);
        }

        public string MatriculAgente()
        {
            if (string.IsNullOrEmpty(this.ProcessoNumero) || this.ProcessoNumero.IndexOf(".") < 0)
            {
                var matriculatemp = "00000" + this.EsperadoMatricula;
                return matriculatemp.Substring(matriculatemp.Length - 5);
            }

            var matricula = this.ProcessoNumero.Substring(0, this.ProcessoNumero.IndexOf("."));
            if (matricula.Length != 5)
            {
                matricula = "00000" + matricula;
                matricula = matricula.Substring(matricula.Length - 5);
            }

            return matricula;
        }

        public string TipoProcessoDesc()
        {
            switch (this.ProcessoTipo)
            {
                case 1:
                    return "FCVS";
                case 2:
                    return "CADMUT";
                default:
                    return string.Empty;
            }
        }

        public string HipotecaGrau()
        {
            if (string.IsNullOrEmpty(this.ProcessoNumero))
            {
                return this.EsperadorHipoteca;
            }

            return this.ProcessoNumero.Substring(this.ProcessoNumero.Length - 1);
        }

        public string Situacao()
        {
            if (string.IsNullOrEmpty(this.SituacaoInterna))
            {
                this.CalcularStatus();
            }

            return this.SituacaoInterna;
        }

        public string DataSituacao()
        {
            if (string.IsNullOrEmpty(this.SituacaoDataInterna))
            {
                this.CalcularUltimaDataValida();
            }

            return this.SituacaoDataInterna;
        }

        public string DataLoteCef()
        {
            if (this.LoteCefStatus == "AB")
            {
                return string.Empty;
            }

            if (this.LoteCefDataFim != null && this.LoteCefDataFim >= new DateTime(1900, 1, 1))
            {
                return ((DateTime) this.LoteCefDataFim).ToString("dd/MM/yyyy");
            }

            return string.Empty;
        }

        public string DataLoteCefAprovacao()
        {
            if (this.LoteCefDataAprovacao != null && this.LoteCefDataAprovacao >= new DateTime(1900, 1, 1))
            {
                return ((DateTime)this.LoteCefDataAprovacao).ToString("dd/MM/yyyy HH:mm");
            }

            return string.Empty;
        }

        public string NumeroProcessoMontado()
        {
            if (string.IsNullOrEmpty(this.ProcessoNumero))
            {
                return this.MatriculAgente() + "." + this.EsperadoContrato + "/" + this.EsperadorHipoteca;
            }

            return this.ProcessoNumero;
        }

        public string StatusCaixa()
        {
            if (this.CaixaStatus == "D" || this.CaixaStatus == "F")
            {
                return "Devolvida";
            }

            return "Recepcionada";
        }

        private void CalcularUltimaDataValida()
        {
            var listaDatas = new List<DateTime?>();
            if (this.CaixaData != null && this.CaixaData > new DateTime(1900, 1, 1))
            {
                listaDatas.Add(this.CaixaData);
            }

            if (this.CaixaDataConferencia != null && this.CaixaDataConferencia > new DateTime(1900, 1, 1))
            {
                listaDatas.Add(this.CaixaDataConferencia);
            }

            if (this.CaixaDataPreparo != null && this.CaixaDataPreparo > new DateTime(1900, 1, 1))
            {
                listaDatas.Add(this.CaixaDataPreparo);
            }

            if (this.LoteCefDataCriacao != null && this.LoteCefDataCriacao > new DateTime(1900, 1, 1))
            {
                listaDatas.Add(this.LoteCefDataCriacao);
            }

            if (this.LoteCefDataFim != null && this.LoteCefDataFim > new DateTime(1900, 1, 1))
            {
                listaDatas.Add(this.LoteCefDataFim);
            }

            if (this.LoteDataEnvio != null && this.LoteDataEnvio > new DateTime(1900, 1, 1))
            {
                listaDatas.Add(this.LoteDataEnvio);
            }

            if (this.LoteDataFaturamento != null && this.LoteDataFaturamento > new DateTime(1900, 1, 1))
            {
                listaDatas.Add(this.LoteDataFaturamento);
            }

            if (this.LoteDataOcr != null && this.LoteDataOcr > new DateTime(1900, 1, 1))
            {
                listaDatas.Add(this.LoteDataOcr);
            }

            if (this.LoteDataReavaliado != null && this.LoteDataReavaliado > new DateTime(1900, 1, 1))
            {
                listaDatas.Add(this.LoteDataReavaliado);
            }

            if (this.LoteDataTransmissao != null && this.LoteDataTransmissao > new DateTime(1900, 1, 1))
            {
                listaDatas.Add(this.LoteDataTransmissao);
            }

            if (this.ProcDataStart != null && this.ProcDataStart > new DateTime(1900, 1, 1))
            {
                listaDatas.Add(this.ProcDataStart);
            }

            if (this.CaixaDataDevolucao != null && this.CaixaDataDevolucao > new DateTime(1900, 1, 1))
            {
                listaDatas.Add(this.CaixaDataDevolucao);
            }

            if (listaDatas.Count == 0)
            {
                this.SituacaoDataInterna = string.Empty;
                return;
            }

            this.SituacaoDataInterna = this.EncontrarMaisRecenteData(listaDatas);
        }

        private string EncontrarMaisRecenteData(List<DateTime?> listaDatas)
        {
            var maisRecente = new DateTime(1900, 1, 1);
            foreach (var dataAtual in listaDatas)
            {
                if (maisRecente.CompareTo(dataAtual) < 0)
                {
                    maisRecente = (DateTime) dataAtual;
                }
            }

            if (maisRecente.CompareTo(new DateTime(1900, 1, 1)) == 0)
            {
                return string.Empty;
            }

            return maisRecente.ToString("dd/MM/yyyy HH:mm");
        }

        private void CalcularStatus()
        {
            if (this.LoteId == 0)
            {
                this.SituacaoInterna = SitPreparo;
                return;
            }

            if (this.CaixaStatus == "D" || this.CaixaStatus == "F")
            {
                this.SituacaoInterna = SitDevolvido;
                return;
            }

            if (this.LoteStatus == "10" || this.LoteStatus == "R5" )
            {
                this.SituacaoInterna = SitCaptura;
                return;
            }

            if (this.LoteCefStatus == "FE" || this.LoteCefStatus == "RP")
            {
                this.SituacaoInterna = SitQualiCef;
                return;
            }

            if (this.LoteCefStatus == "AP")
            {
                this.SituacaoInterna = SitDevolucao;
                return;
            }

            //// o que sobrou, fica em Processamento
            this.SituacaoInterna = SitProcessamento;
        }
    }
}
