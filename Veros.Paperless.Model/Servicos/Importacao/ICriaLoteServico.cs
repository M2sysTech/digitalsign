namespace Veros.Paperless.Model.Servicos.Importacao
{
    using Veros.Paperless.Model.Entidades;

    public interface ICriaLoteServico
    {
        Lote Criar(string identificacao);

        Lote CriarNaFaseCaptura();
    }
}
