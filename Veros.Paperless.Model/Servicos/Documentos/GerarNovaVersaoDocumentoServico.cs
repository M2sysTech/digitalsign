namespace Veros.Paperless.Model.Servicos.Documentos
{
    using Entidades;
    using Framework;
    using Repositorios;

    public class GerarNovaVersaoDocumentoServico : IGerarNovaVersaoDocumentoServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IGerarPaginasServico gerarPaginasServico;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;

        public GerarNovaVersaoDocumentoServico(
            IDocumentoRepositorio documentoRepositorio,
            IGerarPaginasServico gerarPaginasServico, 
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.gerarPaginasServico = gerarPaginasServico;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
        }

        public void Executar(int documentoId, string caminhoImagens)
        {
            var documentoAtual = this.documentoRepositorio.ObterPorId(documentoId);
            documentoAtual.Status = DocumentoStatus.Excluido;
            this.documentoRepositorio.Salvar(documentoAtual);

            var documentoNovo = this.CriarDocumentoNovo(documentoAtual);
            this.gerarPaginasServico.Executar(documentoNovo, caminhoImagens);

            this.gravaLogDoDocumentoServico.Executar(LogDocumento.AcaoNovaVersao, documentoId, "Nova versão do documento.");
        }

        private Documento CriarDocumentoNovo(Documento documentoAtual)
        {
            var versaoNova = documentoAtual.Versao.ToInt() + 1;

            var documentoNovo = Documento.Novo(
                documentoAtual.TipoDocumento,
                documentoAtual.Cpf,
                documentoAtual.Lote,
                documentoAtual.Processo,
                versaoNova.ToString());

            documentoNovo.DocumentoPaiId = documentoAtual.Id;
            this.documentoRepositorio.Salvar(documentoNovo);

            return documentoNovo;
        }
    }
}
