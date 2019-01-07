namespace Veros.Paperless.Model.Servicos.ArquivosDeColeta
{
    using Entidades;
    using Repositorios;

    public interface IGravaArquivoDeColetaServico
    {
        void Executar(ArquivoColeta arquivoColeta);
    }
}
