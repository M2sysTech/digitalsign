namespace Veros.Paperless.Model.Servicos.ArquivosDeColeta
{
    using Entidades;

    public interface IAdicionaPendenciaDeColetaServico
    {
        void AddPendenciaDeArquivo(ArquivoColeta arquivo, string texto);

        void AddPendenciaDeCaixa(ArquivoColeta arquivo, string texto);
        
        void AddPendenciaDeDossie(ArquivoColeta arquivoDeColeta, string texto);
        
        void AddPendenciaDeDossie(ArquivoColeta arquivoDeColeta, DossieEsperado dossieNoBanco, DossieEsperado dossieEsperadoNovo);

        void AtualizaQuantidadeDeCaixas(ArquivoColeta arquivoColeta, int quantidade);
        
        void AtualizaQuantidadeDeCaixasDuplicadas(ArquivoColeta arquivoColeta);

        void AtualizaQuantidadeDeDossiesIncluidos(ArquivoColeta arquivoColeta, int quantidade);
    }
}
