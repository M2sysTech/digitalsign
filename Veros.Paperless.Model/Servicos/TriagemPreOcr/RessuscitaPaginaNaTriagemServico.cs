namespace Veros.Paperless.Model.Servicos.TriagemPreOcr
{
    using Entidades;
    using Repositorios;
    using ViewModel;

    public class RessuscitaPaginaNaTriagemServico : IRessuscitaPaginaNaTriagemServico
    {
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IGravaLogDaPaginaServico gravaLogDaPaginaServico;

        public RessuscitaPaginaNaTriagemServico(IPaginaRepositorio paginaRepositorio, 
            IDocumentoRepositorio documentoRepositorio, 
            IGravaLogDaPaginaServico gravaLogDaPaginaServico)
        {
            this.paginaRepositorio = paginaRepositorio;
            this.documentoRepositorio = documentoRepositorio;
            this.gravaLogDaPaginaServico = gravaLogDaPaginaServico;
        }

        public void Executar(AcaoDeTriagemPreOcr acao, LoteTriagemViewModel lote)
        {
            var paginaViewModel = lote.ObterPagina(acao.PrimeiraPagina);
            var documentoId = paginaViewModel.DocumentoAtualId();

            if (lote.EstaNaTriagem())
            {
                this.paginaRepositorio.AlterarStatus(paginaViewModel.Id, PaginaStatus.StatusParaReconhecimento);    
                this.documentoRepositorio.AlterarStatus(documentoId, DocumentoStatus.TransmissaoOk);
            }

            switch (acao.Tipo)
            {
                case AcaoDeTriagemPreOcr.RessuscitarPagina:
                    this.gravaLogDaPaginaServico.Executar(
                        LogPagina.AcaoPaginaRessucitada, 
                        paginaViewModel.Id, 
                        documentoId, 
                        string.Format("Página {0} foi ressucitada {1}.", paginaViewModel.Id, lote.Fase));
                    break;
                default:
                    this.gravaLogDaPaginaServico.Executar(
                        LogPagina.AcaoFolhaRessucitada, 
                        paginaViewModel.Id, 
                        documentoId, 
                        string.Format("Folha separadora {0} foi ressucitada {1}.", paginaViewModel.Id, lote.Fase));
                    break;
            }
            
            this.documentoRepositorio.AlterarMarca(documentoId, Documento.MarcaDeAlteradoNaSeparacao);

            lote.MarcaDocumentoManipulado(documentoId);
        }
    }
}
