namespace Veros.Paperless.Model.Servicos.Complementacao
{
    public interface IPortalVertros
    {
        VertrosStatus Analisar(string cpf);
    }
}