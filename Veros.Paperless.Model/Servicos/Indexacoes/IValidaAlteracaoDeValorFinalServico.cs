namespace Veros.Paperless.Model.Servicos.Indexacoes
{
    using Entidades;

    public interface IValidaAlteracaoDeValorFinalServico
    {
        bool Validar(Indexacao indexacao, string valor);
    }
}