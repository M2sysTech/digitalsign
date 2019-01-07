namespace Veros.Paperless.Model.Servicos.Recepcionar
{
    using Entidades;

    public interface IPrepararLoteRecaptura
    {
        void Executar(Lote lote);
    }
}