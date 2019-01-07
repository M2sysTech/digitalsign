namespace Veros.Paperless.Model.Servicos.TriagemPreOcr
{
    using ViewModel;

    public interface IExcluiPaginaNaTriagemServico
    {
        void Executar(AcaoDeTriagemPreOcr acao, LoteTriagemViewModel lote);
    }
}
