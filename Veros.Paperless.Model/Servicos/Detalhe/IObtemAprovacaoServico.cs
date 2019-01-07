namespace Veros.Paperless.Model.Servicos.Detalhe
{
    using Aprovacao;
    using Veros.Paperless.Model.Entidades;

    public interface IObtemAprovacaoServico
    {
        Aprovacao Obter(Processo processo);
    }
}