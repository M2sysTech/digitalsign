namespace Veros.Paperless.Model.Servicos.Identificacao
{
    using System.Linq;
    using Repositorios;
    using Veros.Paperless.Model.Entidades;

    public class AlteraIndexacaoServico : IAlteraIndexacaoServico
    {
        private readonly ICampoRepositorio campoRepositorio;
        private readonly IIndexacaoRepositorio indexacaoRepositorio;

        public AlteraIndexacaoServico(
                    ICampoRepositorio campoRepositorio,
                    IIndexacaoRepositorio indexacaoRepositorio)
        {
            this.campoRepositorio = campoRepositorio;
            this.indexacaoRepositorio = indexacaoRepositorio;
        }

        public void Alterar(Documento documento, TipoDocumento tipoDocumentoNovo)
        {
            foreach (var index in documento.Indexacao)
            {
                index.Campo = string.IsNullOrEmpty(index.Campo.ReferenciaArquivo) ? 
                    null : 
                    this.campoRepositorio.ObterPorReferenciaDeArquivo(tipoDocumentoNovo, index.Campo.ReferenciaArquivo);

                if (index.Campo != null)
                {
                    this.indexacaoRepositorio.AlterarCampo(index.Id, index.Campo.Id);
                }
                else
                {
                    this.indexacaoRepositorio.ApagarPorId(index.Id);
                }
            }

            this.AdicionarIndexadoresFaltantes(documento, tipoDocumentoNovo);
        }

        private void AdicionarIndexadoresFaltantes(Documento documento, TipoDocumento tipoDocumentoNovo)
        {
            var campos = this.campoRepositorio.ObterPorCodigoTipoDocumento(tipoDocumentoNovo.Id);
            documento.AdicionarIndexadoresFaltantes(campos);

            foreach (var indexacao in documento.Indexacao.Where(x => x.Id == 0))
            {
                this.indexacaoRepositorio.Salvar(indexacao);
            }
        }
    }
}