namespace Veros.Paperless.Model.Consultas
{
    using System;

    public class CaixaPorUniColRecArm
    {
        public DateTime DataColeta { get; set; }

        public DateTime DataRecepcao { get; set; }

        public DateTime DataArmazenamento { get; set; }

        public int QuantidadeDeCaixas { get; set; }

        public string Unidade { get; set; }
    }
}