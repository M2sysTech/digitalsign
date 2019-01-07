namespace Veros.Paperless.Model.Consultas
{
    using System;

    public class AcompanhamentoProducaoConsulta
    {
        public int PacoteId { get; set; }

        public string NomeArquivo { get; set; }

        public DateTime DataRecepcao { get; set; }

        public DateTime DataImportacao { get; set; }

        public DateTime DataValidacao { get; set; }

        public DateTime DataProvaZero { get; set; }

        public DateTime DataAprovacao { get; set; }

        public DateTime DataExportacao { get; set; }

        public DateTime DataFormalistica { get; set; }
    }
}
