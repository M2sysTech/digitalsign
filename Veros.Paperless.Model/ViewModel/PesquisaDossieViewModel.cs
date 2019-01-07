namespace Veros.Paperless.Model.ViewModel
{
    using System;

    public class PesquisaDossieViewModel
    {
        public int PaginaId { get; set; }

        public int TipoProcessoId { get; set; }

        public int ColetaId { get; set; }

        public int LotecefId { get; set; }

        public string DataInicio { get; set; }

        public string DataFim { get; set; }

        public string DataInicioMovimento { get; set; }

        public string DataFimMovimento { get; set; }

        public string Identificacao { get; set; }

        public string IdentificacaoCompleta { get; set; }

        public string Dossie { get; set; }

        public string Agente { get; set; }

        public string Contrato { get; set; }

        public string Caixa { get; set; }

        public string Folder { get; set; }

        public string FolderCompleto { get; set; }

        public int LoteId { get; set; }

        public int ProcessoId { get; set; }

        public int UltimoLoteId { get; set; }

        public string TipoDeOrdenacao { get; set; }

        public string ColunaDeOrdenacao { get; set; }

        public string Fase { get; set; }

        public DateTime ObjetoDataInicio()
        {
            return Convert.ToDateTime(this.DataInicio);
        }

        public DateTime ObjetoDataFim()
        {
            return Convert.ToDateTime(this.DataFim);
        }

        public DateTime ObjetoDataInicioMovimento()
        {
            return Convert.ToDateTime(this.DataInicioMovimento);
        }

        public DateTime ObjetoDataFimMovimento()
        {
            return Convert.ToDateTime(this.DataFimMovimento);
        }
    }
}
