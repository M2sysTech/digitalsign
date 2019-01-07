namespace Veros.Paperless.Model.Servicos.Recepcionar
{
    using Entidades;
    using Framework;
    using Repositorios;

    public class PrepararLoteRecaptura : IPrepararLoteRecaptura
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IGravaLogDoLoteServico gravaLogDoLoteServico;
        private readonly IRegraVioladaRepositorio regraVioladaRepositorio;
        private readonly ILoteRepositorio loteRepositorio;

        public PrepararLoteRecaptura(
            IDocumentoRepositorio documentoRepositorio, 
            IPaginaRepositorio paginaRepositorio, 
            IGravaLogDoLoteServico gravaLogDoLoteServico, 
            IRegraVioladaRepositorio regraVioladaRepositorio, 
            ILoteRepositorio loteRepositorio)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.paginaRepositorio = paginaRepositorio;
            this.gravaLogDoLoteServico = gravaLogDoLoteServico;
            this.regraVioladaRepositorio = regraVioladaRepositorio;
            this.loteRepositorio = loteRepositorio;
        }

        public void Executar(Lote lote)
        {
            if (lote.Recapturado == false)
            {
                return;
            }

            this.paginaRepositorio.ApagarPorLote(lote);
            this.documentoRepositorio.ApagarPorLote(lote);
            this.loteRepositorio.MarcarParaEnviarParaFileTransfer(lote);

            this.regraVioladaRepositorio.ExcluirRegraPorLoteId(lote.Id);

            this.gravaLogDoLoteServico.Executar(LogLote.AcaoLoteCapturado, lote.Id, "Lote foi recapturado. Paginas antigas removidas");

            Log.Application.InfoFormat("Lote é uma recaptura. Todos os documentos e paginas antigos foram excluidos. LoteId #{0}", lote.Id);
        }
    }
}