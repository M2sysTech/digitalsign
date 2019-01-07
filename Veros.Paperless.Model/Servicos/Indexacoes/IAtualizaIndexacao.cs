namespace Veros.Paperless.Model.Servicos.Indexacoes
{
    using Batimento.Experimental;

    public interface IAtualizaIndexacao
    {
        void ApartirDoReconhecimento(ResultadoBatimentoDocumento resultadoBatimentoDocumento);

        void ApartirDoBatimentoComProvaZero(ResultadoBatimentoDocumento resultadoBatimento);
    }
}