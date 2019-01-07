namespace Veros.Paperless.Model.Servicos.ControleDeQualidade
{
    public interface IGravaControleDeQualidadeCefServico
    {
        void Executar(int processoId, string acao, string observacao);
    }
}
