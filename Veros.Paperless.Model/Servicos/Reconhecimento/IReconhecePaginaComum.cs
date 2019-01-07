namespace Veros.Paperless.Model.Servicos.Reconhecimento
{
    using Entidades;

    public interface IReconhecePaginaComum
    {
        void Executar(Pagina pagina, Imagem imagem);
    }
}