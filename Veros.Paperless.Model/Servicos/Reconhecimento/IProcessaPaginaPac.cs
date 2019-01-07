namespace Veros.Paperless.Model.Servicos.Reconhecimento
{
    using Entidades;

    public interface IProcessaPaginaPac
    {
        void Executar(Pagina pagina);
    }
}