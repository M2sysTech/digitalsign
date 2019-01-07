namespace Veros.Paperless.Model.Entidades
{
    public enum EmailPendenteStatus
    {
        NaoEnviado = 0,
        Enviado = 1,
        AguardandoTramitarRegulacao = 2,
        Verificado = 3,
        AguardandoTramitarCadastro = 4,
        Cancelado = 5
    }
}