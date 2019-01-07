namespace Veros.Paperless.Model.Servicos.ControleDeQualidade
{
    public interface IGravaControleDeQualidadeM2Servico
    {
        void Executar(int processoId, string acao, string observacao);
    }
}
