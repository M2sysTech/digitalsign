namespace Veros.Paperless.Model.Servicos
{
    using Veros.Paperless.Model.Entidades;

    public interface IGravaLogDoDossieEsperadoServico
    {
        void Executar(string acaoLogDossieEsperado, DossieEsperado dossieEsperado, string observacao);
    }
}