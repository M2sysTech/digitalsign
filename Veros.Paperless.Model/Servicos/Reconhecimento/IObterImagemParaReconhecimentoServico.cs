namespace Veros.Paperless.Model.Servicos.Reconhecimento
{
    using Entidades;

    public interface IObterImagemParaReconhecimentoServico
    {
        Imagem Executar(Pagina pagina);

        Imagem ExecutarUsandoCache(Pagina pagina, string extensaoPreferida);
    }
}