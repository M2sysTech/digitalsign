namespace Veros.Paperless.Model.Servicos.Separacao
{
    using ViewModel;

    public interface IRessuscitaPaginaNaSeparacaoServico
    {
        void Executar(AcaoDeSeparacao acao, LoteParaSeparacaoViewModel loteParaSeparacao);
    }
}
