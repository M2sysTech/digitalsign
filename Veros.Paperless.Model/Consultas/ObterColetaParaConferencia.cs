namespace Veros.Paperless.Model.Consultas
{
    using System;

    public class ObterColetaParaConferencia
    {
        public int PacoteId { get; set; }

        public string Identificacao { get; set; }

        public int ColetaId { get; set; }

        public int TotalDossies { get; set; }

        public string Descricao { get; set; }

        public string Endereco { get; set; }

        public DateTime? DataColeta { get; set; }

        public DateTime? DataRecepcao { get; set; }        
    }
}
