namespace Veros.Paperless.Model.Servicos.Identificacao
{
    using System.Linq;
    using Repositorios;
    using Veros.Paperless.Model.Entidades;

    public class CriaPaginaNaPacServico : ICriaPaginaNaPacServico
    {
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IPostaArquivoFileTransferServico postaArquivoFileTransferServico;
        private readonly IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;

        public CriaPaginaNaPacServico(
            IProcessoRepositorio processoRepositorio, 
            IPaginaRepositorio paginaRepositorio, 
            IPostaArquivoFileTransferServico postaArquivoFileTransferServico, 
            IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico, 
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico)
        {
            this.processoRepositorio = processoRepositorio;
            this.paginaRepositorio = paginaRepositorio;
            this.postaArquivoFileTransferServico = postaArquivoFileTransferServico;
            this.baixaArquivoFileTransferServico = baixaArquivoFileTransferServico;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
        }

        public void Criar(Documento documento)
        {
            var pac = this.processoRepositorio.ObterDocumentoPac(documento.Processo.Id);

            if (pac == null)
            {
                return;
            }

            var paginaNova = this.AdicionarPaginaNaPac(pac, documento.Paginas.FirstOrDefault());

            this.PostarPaginaNoFileTransfer(documento.Paginas.FirstOrDefault(), paginaNova);

            this.gravaLogDoDocumentoServico.Executar(
                LogDocumento.AcaoDocumentoGenerico,
                pac.Id,
                string.Format("Página [{0}] foi adicionada na tela de identificação", paginaNova.Id));
        }

        private Pagina AdicionarPaginaNaPac(Documento pac, Pagina pagina)
        {
            var novaPagina = pagina.CloneWithoutId();
            novaPagina.Id = 0;
            novaPagina.Documento = pac;
            novaPagina.Ordem = 6;

            this.paginaRepositorio.Salvar(novaPagina);

            return novaPagina;
        }

        private void PostarPaginaNoFileTransfer(Pagina paginaAntiga, Pagina paginaNova)
        {
            var caminhoArquivo = this.baixaArquivoFileTransferServico.BaixarArquivo(paginaAntiga.Id, paginaAntiga.TipoArquivo, false, true);
            this.postaArquivoFileTransferServico.PostarPagina(paginaNova, caminhoArquivo);
        }
    }
}