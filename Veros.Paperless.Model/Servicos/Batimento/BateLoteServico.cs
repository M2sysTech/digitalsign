namespace Veros.Paperless.Model.Servicos.Batimento
{
    using Entidades;
    using Experimental;
    using Indexacoes;
    using Repositorios;

    public class BateLoteServico : IBateLoteServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IComplementaIndexacaoDocumentoServico complementaIndexacaoDocumentoServico;
        private readonly IFixaIndexacaoDocumentoServico fixaIndexacaoDocumentoServico;
        private readonly IBaterDocumento baterDocumento;
        private readonly IAtualizaIndexacao atualizaIndexacao;
        private readonly IInformacoesReconhecimento informacoesReconhecimento;
        //// private readonly IClassificaDocumentoServico classificaDocumentoServico;
        
        public BateLoteServico(
            IDocumentoRepositorio documentoRepositorio, 
            IComplementaIndexacaoDocumentoServico complementaIndexacaoDocumentoServico, 
            IFixaIndexacaoDocumentoServico fixaIndexacaoDocumentoServico, 
            IBaterDocumento baterDocumento, 
            IAtualizaIndexacao atualizaIndexacao, 
            IInformacoesReconhecimento informacoesReconhecimento)
        //// IClassificaDocumentoServico classificaDocumentoServico)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.complementaIndexacaoDocumentoServico = complementaIndexacaoDocumentoServico;
            this.fixaIndexacaoDocumentoServico = fixaIndexacaoDocumentoServico;
            this.baterDocumento = baterDocumento;
            this.atualizaIndexacao = atualizaIndexacao;
            this.informacoesReconhecimento = informacoesReconhecimento;
            //// this.classificaDocumentoServico = classificaDocumentoServico;
        }

        public void Bater(int loteId)
        {
            var documentos = this.documentoRepositorio.ObterTodosPorLoteComPaginasEIndexacao(loteId);

            foreach (var documento in documentos)
            {
                var imagemReconhecida = this.informacoesReconhecimento.Obter(documento);
                var resultadoBatimento = this.baterDocumento.Iniciar(documento, imagemReconhecida);
                this.atualizaIndexacao.ApartirDoReconhecimento(resultadoBatimento);
                this.complementaIndexacaoDocumentoServico.Execute(documento, imagemReconhecida, resultadoBatimento);

                this.fixaIndexacaoDocumentoServico.Execute(documento);
            }
        }
    }
}