namespace Veros.Paperless.Model.Servicos.Documentos
{
    using System;
    using System.Linq;
    using Entidades;
    using Framework;
    using Framework.Servicos;
    using Repositorios;

    public class GerarNovoDocumentoServico : IGerarNovoDocumentoServico
    {
        private readonly ILoteRepositorio loteRepositorio;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IGerarPaginasServico gerarPaginasServico;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;
        private readonly ISessaoDoUsuario userSession;

        public GerarNovoDocumentoServico(
            ILoteRepositorio loteRepositorio,
            IDocumentoRepositorio documentoRepositorio,
            IGerarPaginasServico gerarPaginasServico, 
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico, 
            ISessaoDoUsuario userSession)
        {
            this.loteRepositorio = loteRepositorio;
            this.documentoRepositorio = documentoRepositorio;
            this.gerarPaginasServico = gerarPaginasServico;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
            this.userSession = userSession;
        }

        public void Executar(int loteId, int tipoDocumentoId, string caminhoImagens)
        {
            var lote = this.loteRepositorio.ObterPorId(loteId);

            var codigo = DateTime.Now.ToString("fffssmmhhdd");

            var documentoNovo = Documento.Novo(
                new TipoDocumento { Id = tipoDocumentoId },
                codigo,
                lote,
                lote.Processos.FirstOrDefault());
            
            this.documentoRepositorio.Salvar(documentoNovo);

            this.gerarPaginasServico.Executar(documentoNovo, caminhoImagens);

            this.gravaLogDoDocumentoServico.Executar(LogDocumento.AcaoInclusaoDeDocumento, documentoNovo.Id, "Documento incluído no processo.");
        }

        public void GerarArquivoUpload(int loteId, int tipoDocumentoId, string caminhoImagens, int documentoPai = 0)
        {
            var lote = this.loteRepositorio.ObterPorId(loteId);
            var usuario = (Usuario) this.userSession.UsuarioAtual;
            var documentoNovo = Documento.Novo(
                new TipoDocumento { Id = tipoDocumentoId },
                usuario.Login,
                lote,
                lote.Processos.FirstOrDefault());

            documentoNovo.Status = DocumentoStatus.StatusFinalizado;
            documentoNovo.UsuarioResponsavelId = usuario.Id;
            documentoNovo.ArquivoDigital = 1;
            documentoNovo.Virtual = true;

            if (documentoPai > 0)
            {
                documentoNovo.DocumentoPaiId = documentoPai;
                documentoNovo.Versao = (this.documentoRepositorio.ObterPorId(documentoPai).Versao.ToInt() + 1).ToString();
                //// seta documento antigo como excluido
                this.documentoRepositorio.AlterarStatus(documentoPai, DocumentoStatus.Excluido);
                this.gravaLogDoDocumentoServico.Executar(LogDocumento.AcaoAlteracaoDeDocumentoUpload, documentoPai, "Arquivo Digital excluido por causa de nova versão.");
            }
            
            this.documentoRepositorio.Salvar(documentoNovo);
            this.gerarPaginasServico.InserirArquivoUnico(documentoNovo, caminhoImagens, lote.CloudOk);
            this.gravaLogDoDocumentoServico.Executar(LogDocumento.AcaoInclusaoDeDocumentoUpload, documentoNovo.Id, "Arquivo Digital incluído no processo.");
        }
    }
}
