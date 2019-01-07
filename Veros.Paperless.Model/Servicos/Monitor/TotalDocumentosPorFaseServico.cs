namespace Veros.Paperless.Model.Servicos.Monitor
{
    using System.Collections.Generic;
    using System.Linq;
    using Consultas;
    using Framework;
    using Repositorios;

    public class TotalDocumentosPorFaseServico : ITotalDocumentosPorFaseServico
    {
        private readonly ILoteRepositorio loteRepositorio;
        private readonly IDocumentoRepositorio documentoRepositorio;

        public TotalDocumentosPorFaseServico(ILoteRepositorio loteRepositorio,
            IDocumentoRepositorio documentoRepositorio)
        {
            this.loteRepositorio = loteRepositorio;
            this.documentoRepositorio = documentoRepositorio;
        }

        public IList<TotalDocumentoPorFaseConsulta> Executar()
        {
            var totalDeDocumentosPorFase = this.loteRepositorio.ObterTotalDeDocumentosPorFase();

            var totaisPorDocumento = this.documentoRepositorio.ObterTotalDeDocumentosPorFase();

            totalDeDocumentosPorFase.AddRange(totaisPorDocumento);

            return totalDeDocumentosPorFase;
        }
    }
}