namespace Veros.Paperless.Model.Servicos.Reconhecimento
{
    using Entidades;

    public interface IReconhecePaginaServico
    {
        void Executar(Pagina pagina);
    }
}