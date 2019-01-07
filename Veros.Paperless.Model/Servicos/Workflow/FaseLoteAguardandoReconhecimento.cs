namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Repositorios;

    //// Pega lotes com status 55 (aguard. reconhec), verifica se todas as paginas ja passaram pelo OCR (todas estão com 5A)
    public class FaseLoteAguardandoReconhecimento : FaseDeWorkflow<Lote, LoteStatus>
    {
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IValorReconhecidoRepositorio valorReconhecidoRepositorio;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IProcessoRepositorio processoRepositorio;

        public FaseLoteAguardandoReconhecimento(IPaginaRepositorio paginaRepositorio,
            IValorReconhecidoRepositorio valorReconhecidoRepositorio,
            IDocumentoRepositorio documentoRepositorio, 
            IProcessoRepositorio processoRepositorio)
        {
            this.paginaRepositorio = paginaRepositorio;
            this.valorReconhecidoRepositorio = valorReconhecidoRepositorio;
            this.documentoRepositorio = documentoRepositorio;
            this.processoRepositorio = processoRepositorio;

            this.StatusDaFase = LoteStatus.AguardandoReconhecimento;
            this.FaseEstaAtiva = x => x.ReconhecimentoAtivo;
            this.StatusSeFaseEstiverInativa = LoteStatus.ReconhecimentoExecutado;
        }

        protected override void ProcessarFase(Lote lote)
        {
            var paginas = this.paginaRepositorio.ObterPorLote(lote);
            
            if (Contexto.GargaloDeOcr)
            {
                if (paginas.Any(x => x.Status == PaginaStatus.StatusParaReconhecimento))
                {
                    return;
                }
            }

            ////this.GravaTipoDeDocumentoReconhecido(paginas);
            var docs = this.documentoRepositorio.ObterTodosPorLote(lote.Id);

            if (docs.Any(x =>
                x.Virtual &&
                x.Status != DocumentoStatus.Excluido))
            {
                lote.Status = LoteStatus.AguardandoClassifier;
                this.processoRepositorio.AlterarStatusPorLote(lote.Id, ProcessoStatus.AguardandoMontagemPdf);
                foreach (var processo in lote.Processos)
                {
                    processo.Status = ProcessoStatus.AguardandoMontagemPdf;
                    processo.HoraInicio = null;
                }
            }
        }

        private void GravaTipoDeDocumentoReconhecido(IEnumerable<Pagina> paginas)
        {
            var documentosNaoIdentificados = paginas.Where(x => x.Documento.TipoDocumento.Id == TipoDocumento.CodigoNaoIdentificado);

            foreach (var pagina in documentosNaoIdentificados)
            {
                var valorReconhecido = this.valorReconhecidoRepositorio.ObtemPrimeiroPorPagina(pagina.Id);

                if (valorReconhecido != null && string.IsNullOrEmpty(valorReconhecido.TemplateName) == false)
                {
                    var tipoDocumento = TipoDocumento.ObterPorNome(valorReconhecido.TemplateName.ToUpper(), TipoDocumento.CodigoNaoIdentificado);

                    if (tipoDocumento != TipoDocumento.CodigoNaoIdentificado)
                    {
                        this.documentoRepositorio.AlterarTipo(pagina.Documento.Id, tipoDocumento);
                    }    
                }
            }
        }
    }
}