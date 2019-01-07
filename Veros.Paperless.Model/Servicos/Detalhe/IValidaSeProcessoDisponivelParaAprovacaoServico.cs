namespace Veros.Paperless.Model.Servicos.Aprovacao
{
    using Entidades;

    public interface IValidaSeProcessoDisponivelParaAprovacaoServico
    {
        bool Validar(Processo processo);

        bool ValidarStatus(Processo processo);
    }
}