namespace Veros.Paperless.Model.Servicos.Batimento
{
    using Veros.Paperless.Model.Entidades;

    public interface IBateLoteServico
    {
        void Bater(int loteId);
    }
}