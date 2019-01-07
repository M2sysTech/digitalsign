namespace Veros.Paperless.Model.Servicos.Indexacoes
{
    using Entidades;
    using Framework.Modelo;
    using Framework.Servicos;
    using Veros.Paperless.Model.Repositorios;

    public class AlteraValorFinalIndexacaoServico : IAlteraValorFinalIndexacaoServico
    {
        private readonly IIndexacaoRepositorio indexacaoRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;
        private readonly IGravaLogDoProcessoServico gravaLogDoProcessoServico;
        private readonly IValidaAlteracaoDeValorFinalServico validaAlteracaoDeValorFinalServico;
        private readonly ISessaoDoUsuario userSession;

        public AlteraValorFinalIndexacaoServico(
            IIndexacaoRepositorio indexacaoRepositorio, 
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico, 
            IGravaLogDoProcessoServico gravaLogDoProcessoServico, 
            IValidaAlteracaoDeValorFinalServico validaAlteracaoDeValorFinalServico, 
            ISessaoDoUsuario userSession)
        {
            this.indexacaoRepositorio = indexacaoRepositorio;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
            this.gravaLogDoProcessoServico = gravaLogDoProcessoServico;
            this.validaAlteracaoDeValorFinalServico = validaAlteracaoDeValorFinalServico;
            this.userSession = userSession;
        }

        public void Executar(int indexacaoId, string valor)
        {
            if (this.userSession.EstaAutenticado == false)
            {
                throw new RegraDeNegocioException("Usuário não está logado na aplicação");
            }

            var indexacao = this.indexacaoRepositorio.ObterPorId(indexacaoId);

            if (this.validaAlteracaoDeValorFinalServico.Validar(indexacao, valor) == false)
            {
                return;
            }

            valor = valor.Trim().ToUpper();
            this.GravarLog(indexacao, valor);

            this.indexacaoRepositorio.AlterarValorFinal(indexacaoId, valor);

            if (string.IsNullOrEmpty(valor) && indexacao.Campo.Obrigatorio == false)
            {
                this.ApagarValores(indexacao);
            }
        }

        private void ApagarValores(Indexacao indexacao)
        {
            this.indexacaoRepositorio.LimparValor1E2(indexacao.Id);

            this.gravaLogDoDocumentoServico.Executar(
            LogDocumento.AcaoDocumentoAlteracaoValorFinal,
            indexacao.Documento.Id,
            string.Format("Primeiro valor do campo {0} era [{1}] e foi excluído na aprovação", indexacao.Id, indexacao.PrimeiroValor));

            this.gravaLogDoDocumentoServico.Executar(
            LogDocumento.AcaoDocumentoAlteracaoValorFinal,
            indexacao.Documento.Id,
            string.Format("Segundo valor do campo {0} era [{1}] e foi excluído na aprovação", indexacao.Id, indexacao.SegundoValor));
        }

        private void GravarLog(Indexacao indexacao, string valorNovo)
        {
            this.gravaLogDoDocumentoServico.Executar(
                LogDocumento.AcaoDocumentoAlteracaoValorFinal,
                indexacao.Documento.Id,
                string.Format("Valor final do campo {0} foi alterado para {1}", indexacao.Id, valorNovo));

            this.gravaLogDoProcessoServico.Executar(
                LogProcesso.AcaoAlteracaoDeCampo, 
                indexacao.Documento.Processo, 
                string.Format("Valor final do campo {0} foi alterado na aprovação", indexacao.Id));
        }
    }
}