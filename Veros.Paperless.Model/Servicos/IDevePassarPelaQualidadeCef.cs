namespace Veros.Paperless.Model.Servicos
{
    using Veros.Paperless.Model.Entidades;

    public interface IDevePassarPelaQualidadeCef
    {
        bool Validar(Lote lote);
    }
}