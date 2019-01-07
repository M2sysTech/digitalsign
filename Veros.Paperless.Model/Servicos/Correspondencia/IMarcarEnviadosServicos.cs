namespace Veros.Paperless.Model.Servicos.Correspondencia
{
    using Entidades;

    public interface IMarcarEnviadosServicos
    {
        void ExecutarParaEmail(EmailPendente emailPendente);
    }
}