namespace Veros.Paperless.Model.Servicos.Documentos
{
    using System.Collections.Generic;
    using System.Linq;
    using Campos;
    using Repositorios;
    using Veros.Paperless.Model.Entidades;

    public class ObtemOpcoesDeValorDeIndexacaoServico : IObtemOpcoesDeValorDeIndexacaoServico
    {
        private readonly IDominioCampoRepositorio dominioCampoRepositorio;
        private readonly IIndexacaoRepositorio indexacaoRepositorio;

        public ObtemOpcoesDeValorDeIndexacaoServico(
            IDominioCampoRepositorio dominioCampoRepositorio, 
            IIndexacaoRepositorio indexacaoRepositorio)
        {
            this.dominioCampoRepositorio = dominioCampoRepositorio;
            this.indexacaoRepositorio = indexacaoRepositorio;
        }

        public IList<DominioCampo> Obter(Indexacao indexacao)
        {
            var tabelaDinamica = indexacao.Campo.TabelaDinamica();

            if (indexacao.Campo.ReferenciaArquivo == Campo.ReferenciaDeArquivoOrgaoEmissorDoDocumento)
            {
                tabelaDinamica = this.ObterTabelaDinamicaDoOrgaoDeDi(indexacao);
            }

            if (string.IsNullOrEmpty(tabelaDinamica))
            {
                return null;
            }

            return this.dominioCampoRepositorio.ObterDominiosPorCodigo(tabelaDinamica);
        }

        private string ObterTabelaDinamicaDoOrgaoDeDi(Indexacao indexacao)
        {
            var tiposDeDi = this.indexacaoRepositorio.ObterPorReferenciaDeArquivo(indexacao.Documento.Id, Campo.ReferenciaDeArquivoTipoDeDocumentoDeIdentificacao);

            if (tiposDeDi.FirstOrDefault() == null || string.IsNullOrEmpty(tiposDeDi.FirstOrDefault().ObterValor()))
            {
                return indexacao.Campo.TabelaDinamica();
            }

            return string.Format("DOMINIO_{0}", tiposDeDi.FirstOrDefault().ObterValor().Trim());
        }
    }
}