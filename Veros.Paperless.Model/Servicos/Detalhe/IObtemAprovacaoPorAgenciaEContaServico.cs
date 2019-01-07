namespace Veros.Paperless.Model.Servicos.Detalhe
{
    using Aprovacao;

    public interface IObtemAprovacaoPorAgenciaEContaServico
    {
        Aprovacao Obter(
            string agencia,
            string conta);

        Aprovacao ObterForcado(
            string agencia,
            string conta);
    }
}