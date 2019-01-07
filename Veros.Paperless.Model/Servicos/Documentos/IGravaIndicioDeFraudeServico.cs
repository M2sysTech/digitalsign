namespace Veros.Paperless.Model.Servicos.Documentos
{
    public interface IGravaIndicioDeFraudeServico
    {
        void Executar(int documentoId, string indicioDeFraude);
    }
}