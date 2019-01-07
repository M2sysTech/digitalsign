namespace Veros.Paperless.Model.Consultas
{
    using System;
    using Entidades;

    public class TotaisPorPacoteProcessado
    {
        public int PacoteProcessadoId { get; set; }

        public DateTime? RecebidoEm { get; set; }

        public int Status { get; set; }

        public string Ativado { get; set; }

        public string ExibeNaQualidade { get; set; }

        public int Capturado { get; set; }

        public int EmOcr { get; set; }

        public int Triagem { get; set; }

        public int IdentManual { get; set; }

        public int OcrPdfa { get; set; }

        public int EmClassificacao { get; set; }

        public int EmQualidadeM2Sys { get; set; }

        public int EmAjuste { get; set; }

        public int EmAssinatura { get; set; }

        public int Concluido { get; set; }

        public int EmQualidadeCef { get; set; }

        public int EmFaturamento { get; set; }

        public string StatusDescricao()
        {
            switch (this.Status)
            {
                case (int) StatusPacote.AprovadoNaQualidade:
                    return "Aprovado na Qualidade";

                case (int) StatusPacote.Cancelado:
                    return "Cancelado";

                case (int) StatusPacote.EmProcessamento:
                    return "Em Processamento";

                case (int) StatusPacote.Pendente:
                    return "Pendente";

                case (int) StatusPacote.Processado:
                    return "Processado";

                case (int) StatusPacote.Recepcionando:
                    return "Recepcionado";

                case (int) StatusPacote.ReprovadoNaQualidade:
                    return "Reprovado na Qualidade";
            }

            return this.Status.ToString();
        }

        public string AtivadoDescricao()
        {
            return this.Ativado == "S" ? "Sim" : "Não";
        }
    }
}
