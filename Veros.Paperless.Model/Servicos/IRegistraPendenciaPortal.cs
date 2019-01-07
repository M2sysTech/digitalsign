namespace Veros.Paperless.Model.Servicos
{
    using System;
    using Entidades;

    public interface IRegistraPendenciaPortal
    {
        PendenciaPortal Incrementar(int solicitacaoId, Exception exception, Operacao operacao);
    }
}