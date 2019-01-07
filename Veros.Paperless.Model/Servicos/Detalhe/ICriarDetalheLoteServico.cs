namespace Veros.Paperless.Model.Servicos.Detalhe
{
    using Veros.Paperless.Model.Entidades;

    public interface ICriarDetalheLoteServico
    {
        DetalheLote Criar(Processo processo);
    }
}
