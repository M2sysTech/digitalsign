namespace Veros.Paperless.Model.Servicos.Dossies
{
    using Consultas;

    public class RelatorioDossiersPorLoteCef : IRelatorioDossiersPorLoteCef
    {
        private readonly IDossiePorLoteCefConsulta dossiePorLoteCefConsulta;

        public RelatorioDossiersPorLoteCef(IDossiePorLoteCefConsulta dossiePorLoteCefConsulta)
        {
            this.dossiePorLoteCefConsulta = dossiePorLoteCefConsulta;
        }

        public DossiersPorLoteCef Gerar(int lotecefId)
        {
            var dossiesPorLoteCef = this.dossiePorLoteCefConsulta.Obter(lotecefId);
            var dossies = DossiersPorLoteCef.Criar(dossiesPorLoteCef);

            return dossies;
        }
    }
}