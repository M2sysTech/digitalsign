namespace Veros.Paperless.Model.Servicos.TriagemPreOcr
{
    using ViewModel;

    public interface ICriaDocumentoNaTriagemServico
    {
        void Executar(AcaoDeTriagemPreOcr acao, LoteTriagemViewModel lote);
    }
}
