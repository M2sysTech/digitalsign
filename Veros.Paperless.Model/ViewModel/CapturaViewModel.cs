namespace Veros.Paperless.Model.ViewModel
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Entidades;

    public class CapturaViewModel
    {
        public string BarcodeCaixa { get; set; }

        public string DataCaptura { get; set; }

        public int DossieId { get; set; }

        public int LoteId { get; set; }

        public string TipoDossie { get; set; }

        public string TipoDossieDescricao { get; set; }

        public string IdentificacaoDossie { get; set; }

        public string MatriculaDossie { get; set; }

        public string ContratoDossie { get; set; }

        public string Hipoteca { get; set; }

        public string DataSinistro { get; set; }

        public string QuantidadeDePaginas { get; set; }

        public string ItensDocumentais { get; set; }

        public string MatriculaPreparador { get; set; }

        public Lote Lote { get; set; }

        public Processo Processo { get; set; }

        public int ColetaId { get; set; }

        ////public IList<TipoDossie> TiposDossie { get; set; }

        public IList<TipoProcesso> TiposDeProcesso { get; set; }

        public IList<TipoDocumento> TiposDeDocumento { get; set; }

        public string TriadoEm
        {
            get;
            set;
        }

        public string CodigoBarras
        {
            get;
            set;
        }

        public static CapturaViewModel Criar(Processo processo)
        {
            return new CapturaViewModel
            {
                TipoDossieDescricao = processo.TipoDeProcesso.Descricao,
                IdentificacaoDossie = processo.Identificacao,
                QuantidadeDePaginas = processo.QtdePaginas.ToString(),
                BarcodeCaixa = processo.Lote.Pacote.Identificacao,
                CodigoBarras = processo.Lote.DossieEsperado.CodigoDeBarras
            };
        }

        public string IdentificacaoFormatada()
        {
            return string.Format("{0}.{1}/{2}", 
                this.MatriculaDossie, 
                this.ContratoDossie, 
                this.Hipoteca);
        }

        public string IdentificacaoSemFormatacao()
        {
            return Regex.Replace(this.IdentificacaoDossie, @"[@/_,\\.\;'\\\\]", string.Empty);
        }
    }
}
