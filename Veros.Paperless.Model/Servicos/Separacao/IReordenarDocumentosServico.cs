namespace Veros.Paperless.Model.Servicos.Separacao
{
    using ViewModel;

    public interface IReordenarDocumentosServico
    {
        void Executar(LoteParaSeparacaoViewModel processoParaSeparacao);
    }
}
