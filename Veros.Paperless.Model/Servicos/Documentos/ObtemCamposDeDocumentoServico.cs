namespace Veros.Paperless.Model.Servicos.Documentos
{
    using System.Collections.Generic;
    using System.Linq;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class ObtemCamposDeDocumentoServico : IObtemCamposDeDocumentoServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IObtemDetalhamentoDoDocumentoCampoServico obtemDetalhamentoDoDocumentoCampoServico;
        private readonly IIndexacaoRepositorio indexacaoRepositorio;

        public ObtemCamposDeDocumentoServico(
            IDocumentoRepositorio documentoRepositorio,
            IObtemDetalhamentoDoDocumentoCampoServico obtemDetalhamentoDoDocumentoCampoServico,
            IIndexacaoRepositorio indexacaoRepositorio)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.obtemDetalhamentoDoDocumentoCampoServico = obtemDetalhamentoDoDocumentoCampoServico;
            this.indexacaoRepositorio = indexacaoRepositorio;
        }

        public CamposDeDocumento Obter(int documentoId)
        {
            var documento = this.documentoRepositorio.ObterPorId(documentoId);

            return new CamposDeDocumento
            {
                Campos = this.ObterCampos(documento.Indexacao)
            };
        }

        public CamposDeDocumento ObterPorProcesso(int processoId)
        {
            var indexacoesProcesso = this.indexacaoRepositorio.ObterPorProcesso(processoId);

            return new CamposDeDocumento
            {
                Campos = this.ObterCampos(indexacoesProcesso.Where(x => x.Campo.ParaValidacao))
            };
        }

        private IList<DetalhamentoDoDocumentoCampo> ObterCampos(IEnumerable<Indexacao> indexacoes)
        {
            return indexacoes.Select(indexacao => this.obtemDetalhamentoDoDocumentoCampoServico.Obter(indexacao)).ToList();
        }
    }
}
