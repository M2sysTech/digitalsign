namespace Veros.Paperless.Model.Servicos.Separacao
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework;
    using FrameworkLocal;

    public class AcaoDeSeparacao
    {
        public const string NovoDocumento = "ND";
        public const string ExcluirPagina = "EX";
        public const string RessuscitarPagina = "DEX";
        public const string ReclassificarDocumento = "REC";

        public int Id { get; set; }

        public string Tipo { get; set; }

        public int NovoDocumentoId { get; set; }

        public int TipoDocumentoId { get; set; }

        public string TextoDePaginas { get; set; }

        public int LoteId { get; set; }

        public int NovoDocumentoOrdem { get; set; }

        public IList<int> Paginas { get; set; }

        public int PrimeiraPagina { get; set; }
        
        public static IList<AcaoDeSeparacao> MontarLista(string textoDeAcoes)
        {
            var lista = new List<AcaoDeSeparacao>();
            var linhas = textoDeAcoes.Split('|');
            
            foreach (var linha in linhas.Where(x => string.IsNullOrEmpty(x) == false))
            {
                var colunas = linha.Split(';');

                var acao = new AcaoDeSeparacao
                {
                    Id = lista.Count,
                    Tipo = colunas[0],
                    NovoDocumentoId = colunas[1].ToInt(),
                    TipoDocumentoId = colunas[2].ToInt(),
                    TextoDePaginas = colunas[3],
                    LoteId = colunas[4].ToInt(),
                    NovoDocumentoOrdem = colunas[5].ToInt()
                };

                if (string.IsNullOrEmpty(acao.TextoDePaginas) == false)
                {
                    acao.Paginas = acao.TextoDePaginas.ToIntList(',');
                    acao.PrimeiraPagina = acao.Paginas.FirstOrDefault();
                }

                lista.Add(acao);
            }
            
            return lista;
        }
    }
}
