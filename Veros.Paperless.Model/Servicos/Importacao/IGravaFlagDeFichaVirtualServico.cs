namespace Veros.Paperless.Model.Servicos.Importacao
{
    public interface IGravaFlagDeFichaVirtualServico
    {
        void Gravar(int loteId, bool possuiFichaReal);
    }
}
