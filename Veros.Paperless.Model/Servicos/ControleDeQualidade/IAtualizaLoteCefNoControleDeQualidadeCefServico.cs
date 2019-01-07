namespace Veros.Paperless.Model.Servicos.ControleDeQualidade
{
    public interface IAtualizaLoteCefNoControleDeQualidadeCefServico
    {
        void Executar(int loteCefId, string acao, string observacao);
    }
}
