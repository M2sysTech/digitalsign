namespace Veros.Paperless.Model.Servicos.Captura
{
    using Veros.Paperless.Model.Entidades;

    public interface IPrecisaSolicitarFichaServico
    {
        bool Executar(int loteId);
    }
}
