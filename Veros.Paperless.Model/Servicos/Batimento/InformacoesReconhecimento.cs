namespace Veros.Paperless.Model.Servicos.Batimento
{
    using Entidades;
    using Repositorios;
    using Storages;

    public class InformacoesReconhecimento : IInformacoesReconhecimento
    {
        private readonly IValorReconhecidoRepositorio valorReconhecidoRepositorio;
        private readonly IPalavraReconhecidaRepositorio palavraReconhecidaRepositorio;
        private readonly IPalavraReconhecidaStorage palavraReconhecidaStorage;

        public InformacoesReconhecimento(
            IValorReconhecidoRepositorio valorReconhecidoRepositorio, 
            IPalavraReconhecidaRepositorio palavraReconhecidaRepositorio, 
            IPalavraReconhecidaStorage palavraReconhecidaStorage)
        {
            this.valorReconhecidoRepositorio = valorReconhecidoRepositorio;
            this.palavraReconhecidaRepositorio = palavraReconhecidaRepositorio;
            this.palavraReconhecidaStorage = palavraReconhecidaStorage;
        }

        public ImagemReconhecida Obter(Documento documento)
        {
            var imagemReconhecida = new ImagemReconhecida
            {
                ValoresReconhecidos = this.valorReconhecidoRepositorio.ObtemPorDocumento(documento.Id)
            };

            if ((documento.TipoDocumento.Id == TipoDocumento.CodigoDocumentoGeral || documento.EhPac) == false)
            {
                if (Contexto.UsarBatimentoAntigo)
                {
                    imagemReconhecida.Palavras = this.palavraReconhecidaRepositorio
                        .ObterPorDocumentoId(documento.Id);
                }
                else
                {
                    imagemReconhecida.Palavras = this.palavraReconhecidaStorage.Obter(documento.Id);
                }
            }

            return imagemReconhecida;
        }
    }
}