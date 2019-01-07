namespace Veros.Paperless.Model.Servicos.TriagemPreOcr
{
    using ViewModel;

    public interface IRessuscitaPaginaNaTriagemServico
    {
        void Executar(AcaoDeTriagemPreOcr acao, LoteTriagemViewModel lote);
    }
}
