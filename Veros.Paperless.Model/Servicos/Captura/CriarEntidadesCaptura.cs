namespace Veros.Paperless.Model.Servicos.Captura
{
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.ViewModel;
    using Veros.Paperless.Model.Repositorios;

    public class CriarEntidadesCaptura : ICriarEntidadesCaptura
    {
        private readonly IPacoteRepositorio pacoteRepositorio;
        private readonly ILoteRepositorio loteRepositorio;
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly IDossieEsperadoRepositorio dossieEsperadoRepositorio;
        private readonly IObtemPacotePorBarcodeServico obtemPacotePorBarcodeServico;
        private readonly IObtemLotePorCapturaViewModelServico obtemLotePorCapturaViewModelServico;
        private readonly IObtemProcessoPorCapturaViewModelServico obtemProcessoPorCapturaViewModelServico;

        public CriarEntidadesCaptura(IPacoteRepositorio pacoteRepositorio, 
            ILoteRepositorio loteRepositorio, 
            IProcessoRepositorio processoRepositorio, 
            IDossieEsperadoRepositorio dossieEsperadoRepositorio, 
            IObtemPacotePorBarcodeServico obtemPacotePorBarcodeServico,
            IObtemLotePorCapturaViewModelServico obtemLotePorCapturaViewModelServico, 
            IObtemProcessoPorCapturaViewModelServico obtemProcessoPorCapturaViewModelServico)
        {
            this.pacoteRepositorio = pacoteRepositorio;
            this.loteRepositorio = loteRepositorio;
            this.processoRepositorio = processoRepositorio;
            this.dossieEsperadoRepositorio = dossieEsperadoRepositorio;
            this.obtemPacotePorBarcodeServico = obtemPacotePorBarcodeServico;
            this.obtemLotePorCapturaViewModelServico = obtemLotePorCapturaViewModelServico;
            this.obtemProcessoPorCapturaViewModelServico = obtemProcessoPorCapturaViewModelServico;
        }

        public Lote Executar(CapturaViewModel model)
        {
            this.CarregarDadosDoDossie(model);

            var pacote = this.obtemPacotePorBarcodeServico.Executar(model.BarcodeCaixa);
            var lote = this.obtemLotePorCapturaViewModelServico.Executar(model, pacote);
            var processo = this.obtemProcessoPorCapturaViewModelServico.Executar(model, lote);
            
            this.pacoteRepositorio.Salvar(lote.Pacote);
            this.loteRepositorio.Salvar(lote);
            this.processoRepositorio.Salvar(processo);
            
            model.Processo = processo;
            lote.Processos.Add(processo);
            return lote;
        }

        private void CarregarDadosDoDossie(CapturaViewModel model)
        {
            if (model.DossieId < 1)
            {
                return;
            }

            var dossie = this.dossieEsperadoRepositorio.ObterPorId(model.DossieId);

            if (dossie.Id < 1)
            {
                return;
            }

            model.ContratoDossie = dossie.NumeroContrato;
            model.MatriculaDossie = dossie.MatriculaAgente;
        }
    }
}
