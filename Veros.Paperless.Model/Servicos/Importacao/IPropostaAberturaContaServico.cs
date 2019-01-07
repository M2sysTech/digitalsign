namespace Veros.Paperless.Model.Servicos.Importacao
{
    using Entidades;

    public interface IPropostaAberturaContaServico
    {
        Documento CriarPacVirtual(Processo processo, string cpf);
    }
}