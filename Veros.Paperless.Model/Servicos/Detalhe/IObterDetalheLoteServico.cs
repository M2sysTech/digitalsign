namespace Veros.Paperless.Model.Servicos.Detalhe
{
    using Veros.Paperless.Model.Entidades;

    public interface IObterDetalheLoteServico
    {
        DetalheLote Obter(string tipoDeSolicitacao, int processoId);
    }
}