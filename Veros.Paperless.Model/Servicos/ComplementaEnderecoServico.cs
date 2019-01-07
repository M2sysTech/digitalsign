namespace Veros.Paperless.Model.Servicos
{
    using Framework;
    using Repositorios;
    using Veros.Paperless.Model.Entidades;

    public class ComplementaEnderecoServico : IComplementaEnderecoServico
    {
        private readonly ICepRepositorio cepRepositorio;
        private readonly IIndexacaoRepositorio indexacaoRepositorio;
        private readonly IDocumentoRepositorio documentoRepositorio;

        public ComplementaEnderecoServico(
            ICepRepositorio cepRepositorio,
            IIndexacaoRepositorio indexacaoRepositorio,
            IDocumentoRepositorio documentoRepositorio)
        {
            this.cepRepositorio = cepRepositorio;
            this.indexacaoRepositorio = indexacaoRepositorio;
            this.documentoRepositorio = documentoRepositorio;
        }

        public void Executar(Documento documento)
        {
            var campoCep = documento.ObterIndexacao(Campo.ReferenciaDeArquivoCepDaResidenciaDoParticipante);

            if (campoCep == null || string.IsNullOrEmpty(campoCep.PrimeiroValor) == false)
            {
                this.ComplementarEndereco(documento, campoCep);

                this.documentoRepositorio.AlterarStatusDeConsulta(documento.Id, DocumentoStatus.StatusDeConsultaRealizado);
            }
        }

        private void ComplementarEndereco(Documento documento, Indexacao campoCep)
        {
            var endereco = this.ObterEndereco(campoCep);

            if (endereco == null || endereco.Cep == 0)
            {
                return;
            }

            this.SalvarIndice(
                documento.ObterIndexacao(Campo.ReferenciaDeArquivoEstadoDaResidenciaDoParticipante),
                endereco.Estado.RemoveAcentuacao());

            this.SalvarIndice(
                documento.ObterIndexacao(Campo.ReferenciaDeArquivoCidadeDaResidenciaDoParticipante),
                endereco.Cidade.RemoveAcentuacao());
            
            this.SalvarIndice(
                documento.ObterIndexacao(Campo.ReferenciaDeArquivoBairroDaResidenciaDoParticipante),
                endereco.Bairro.RemoveAcentuacao());

            this.SalvarIndice(
                documento.ObterIndexacao(Campo.ReferenciaDeArquivoLogradouroDaResidenciaDoParticipante),
                endereco.Logradouro.RemoveAcentuacao());
        }

        private dynamic ObterEndereco(Indexacao campoCep)
        {
            if (campoCep == null || string.IsNullOrEmpty(campoCep.PrimeiroValor))
            {
                return null;
            }

            return this.cepRepositorio.ObterEndereco(campoCep.PrimeiroValor.ToInt());
        }

        private void SalvarIndice(Indexacao indexacao, string valor)
        {
            if (indexacao != null)
            {
                this.indexacaoRepositorio.AlterarPrimeiroValor(indexacao.Id, valor);
            }
        }
    }
}