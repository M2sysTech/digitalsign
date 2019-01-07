namespace Veros.Paperless.Model.Consultas
{
    using System;

    public class TotaisAguardandoQualidadeCef
    {
        public int LoteCefId { get; set; }

        public string LoteCefStatus { get; set; }

        public DateTime LoteCefData { get; set; }

        public DateTime? LoteCefAprovacao { get; set; }

        public DateTime? LoteCefAssinatura { get; set; }

        public int TipoDeAmostra { get; set; }
        
        public int TotalPacote { get; set; }

        public int QtdMarcados { get; set; }

        public int QtdAprovados { get; set; }

        public int QtdSelecionados { get; set; }

        public int QtdEmReprocessamento { get; set; }
    }
}
