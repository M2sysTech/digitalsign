namespace Veros.Paperless.Model.Servicos.Coletas
{
    using Repositorios;
    using ViewModel;

    public class CriaDevolucaoColetaViewModelServico
    {
        private readonly IColetaRepositorio coletaRepositorio;
        private readonly IPacoteRepositorio pacoteRepositorio;

        public CriaDevolucaoColetaViewModelServico(
            IColetaRepositorio coletaRepositorio,
            IPacoteRepositorio pacoteRepositorio)
        {
            this.coletaRepositorio = coletaRepositorio;
            this.pacoteRepositorio = pacoteRepositorio;
        }

        public DevolucaoColetaViewModel Executar(int coletaId)
        {
            var model = new DevolucaoColetaViewModel();
            model.Coleta = this.coletaRepositorio.ObterPorId(coletaId);
            model.PacotesCapturados = this.pacoteRepositorio.ObterPorColeta(model.Coleta);

            return model;
        }
    }
}
