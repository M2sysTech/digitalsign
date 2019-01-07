namespace Veros.Paperless.Model.Servicos.Complementacao
{
    using Ph3;

    public interface IPortalPh3
    {
        ConsultaPf ObterDadosPessoais(string cpf);
    }
}