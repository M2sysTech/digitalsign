namespace Veros.Paperless.Model.Servicos.Ajustes
{
    public interface IGravaAjustesDoProcessoServico
    {
        void Executar(int processoId, string operacao, string observacao);
    }
}
