namespace Veros.Paperless.Model.Servicos.Separacao
{
    using ViewModel;

    public interface IExcluiPaginaNaSeparacaoServico
    {
        void Executar(AcaoDeSeparacao acao, LoteParaSeparacaoViewModel processoParaSeparacao);
    }
}
