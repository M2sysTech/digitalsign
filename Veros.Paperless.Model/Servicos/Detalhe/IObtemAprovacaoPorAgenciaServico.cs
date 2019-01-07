namespace Veros.Paperless.Model.Servicos.Aprovacao
{
    public interface IObtemAprovacaoPorAgenciaServico
    {
        Aprovacao Obter(string agencia);
    }
}