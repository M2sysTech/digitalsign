namespace Veros.Paperless.Model.Servicos.Importacao
{
    using Veros.Paperless.Model.Entidades;

    public interface IAproveitaDadosDeSolicitacoesAntigasServico
    {
        void Executar(Processo processo);
    }
}
