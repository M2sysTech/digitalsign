namespace Veros.Paperless.Model.Entidades
{
    public enum ProcessoDecisao
    {
        Liberado = 1,
        Devolvido = 2,
        LiberadoPeloBanco = 3,
        Ok = 4,
        NegativaTotal = 5,
        NegativaParcial = 6,
        Nenhum = 0
    }
}
