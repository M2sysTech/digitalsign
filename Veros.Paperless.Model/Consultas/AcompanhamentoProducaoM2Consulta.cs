namespace Veros.Paperless.Model.Consultas
{
    using System;

    public class AcompanhamentoProducaoM2Consulta
    {
        public int PacoteId { get; set; }

        public string NomeArquivo { get; set; }

        public DateTime DataRecepcao { get; set; }

        public DateTime DataImportacao { get; set; }

        public DateTime DataOcr { get; set; }

        public DateTime DataIdentificacao { get; set; }

        public DateTime DataMontagem { get; set; }

        public DateTime DataDigitacao { get; set; }

        public DateTime DataValidacao { get; set; }

        public DateTime DataProvaZero { get; set; }

        public DateTime DataFormalistica { get; set; }

        public DateTime DataAprovacao { get; set; }

        public DateTime DataExportacao { get; set; }

        public DateTime DataFim { get; set; }
    }
}
