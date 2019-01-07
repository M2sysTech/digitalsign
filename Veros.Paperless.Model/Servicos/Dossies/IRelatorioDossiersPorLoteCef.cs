namespace Veros.Paperless.Model.Servicos.Dossies
{
    public interface IRelatorioDossiersPorLoteCef
    {
        DossiersPorLoteCef Gerar(int lotecefId);
    }
}