namespace Veros.Paperless.Model.Servicos.Coletas
{
    using Repositorios;
    using ViewModel;

    public class CriaSolicitacaoDeColetaViewModelServico
    {
        private readonly IColetaRepositorio coletaRepositorio;
        private readonly IPacoteRepositorio pacoteRepositorio;

        public CriaSolicitacaoDeColetaViewModelServico(
            IColetaRepositorio coletaRepositorio,
            IPacoteRepositorio pacoteRepositorio)
        {
            this.coletaRepositorio = coletaRepositorio;
            this.pacoteRepositorio = pacoteRepositorio;
        }

        public SolicitacaoDaColetaViewModel Executar(int coletaId)
        {
            var model = new SolicitacaoDaColetaViewModel();
            model.Coleta = this.coletaRepositorio.ObterPorId(coletaId);
            model.PacotesCapturados = this.pacoteRepositorio.ObterPorColeta(model.Coleta);

            return model;
        }
    }
}
