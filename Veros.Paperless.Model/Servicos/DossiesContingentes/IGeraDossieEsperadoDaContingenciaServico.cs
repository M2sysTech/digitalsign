namespace Veros.Paperless.Model.Servicos.DossiesContingentes
{
    using Entidades;

    public interface IGeraDossieEsperadoDaContingenciaServico
    {
        DossieEsperado Executar(int dossieContintenteId, int caixaId, string status);
    }
}
