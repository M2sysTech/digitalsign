namespace Veros.Paperless.Model.Servicos.Separacao
{
    using ViewModel;

    public interface ICriaDocumentoNaSeparacaoServico
    {
        void Executar(AcaoDeSeparacao acao, LoteParaSeparacaoViewModel processoParaSeparacao);
    }
}
