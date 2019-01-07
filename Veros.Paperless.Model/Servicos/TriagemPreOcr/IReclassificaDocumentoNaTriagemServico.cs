namespace Veros.Paperless.Model.Servicos.TriagemPreOcr
{
    using ViewModel;

    public interface IReclassificaDocumentoNaTriagemServico
    {
        void Executar(AcaoDeTriagemPreOcr acao, LoteTriagemViewModel lote);
    }
}
