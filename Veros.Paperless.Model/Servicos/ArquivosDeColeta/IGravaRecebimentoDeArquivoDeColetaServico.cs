namespace Veros.Paperless.Model.Servicos.ArquivosDeColeta
{
    public interface IGravaRecebimentoDeArquivoDeColetaServico
    {
        void Executar(int coletaId, string caminhoArquivo);
    }
}
