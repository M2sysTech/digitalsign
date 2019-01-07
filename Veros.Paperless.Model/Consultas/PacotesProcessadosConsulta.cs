namespace Veros.Paperless.Model.Consultas
{
    using System;

    public class PacotesProcessadosConsulta 
    {
        public DateTime DataRegistro { get; set; }
        
        public int TotalDePacotesImportados { get; set; }

        public int TotalDePacotesCancelados { get; set; }

        public int TotalDePacotesExportados { get; set; }
    }
}
