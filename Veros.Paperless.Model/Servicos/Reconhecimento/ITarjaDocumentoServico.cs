namespace Veros.Paperless.Model.Servicos.Reconhecimento
{
    using Image.Reconhecimento;
    using Veros.Paperless.Model.Entidades;

    public interface ITarjaDocumentoServico
    {
        void Execute(Pagina pagina, ResultadoReconhecimento palavras);
    }
}