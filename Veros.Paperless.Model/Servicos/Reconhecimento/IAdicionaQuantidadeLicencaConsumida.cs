namespace Veros.Paperless.Model.Servicos.Reconhecimento
{
    using Entidades;

    public interface IAdicionaQuantidadeLicencaConsumida
    {
        LicencaConsumida Executar(Pagina pagina, int quantidadeLicencasUtilizadas);
    }
}