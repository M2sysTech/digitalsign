namespace Veros.Paperless.Model.Servicos.Captura
{
    using Framework;
    using Framework.Modelo;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.ViewModel;
    using Veros.Paperless.Model.Repositorios;
    using System;

    public class ObtemProcessoPorCapturaViewModelServico : IObtemProcessoPorCapturaViewModelServico
    {
        private readonly IPacoteRepositorio pacoteRepositorio;
        private readonly ILoteRepositorio loteRepositorio;
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly IColetaRepositorio coletaRepositorio;
        private readonly IDossieEsperadoRepositorio dossieEsperadoRepositorio;
        private readonly IObtemPacotePorBarcodeServico obtemPacotePorBarcodeServico;

        public ObtemProcessoPorCapturaViewModelServico(IPacoteRepositorio pacoteRepositorio, 
            ILoteRepositorio loteRepositorio, 
            IProcessoRepositorio processoRepositorio, 
            IColetaRepositorio coletaRepositorio, 
            IDossieEsperadoRepositorio dossieEsperadoRepositorio, 
            IObtemPacotePorBarcodeServico obtemPacotePorBarcodeServico)
        {
            this.pacoteRepositorio = pacoteRepositorio;
            this.loteRepositorio = loteRepositorio;
            this.processoRepositorio = processoRepositorio;
            this.coletaRepositorio = coletaRepositorio;
            this.dossieEsperadoRepositorio = dossieEsperadoRepositorio;
            this.obtemPacotePorBarcodeServico = obtemPacotePorBarcodeServico;
        }

        public Processo Executar(CapturaViewModel model, Lote lote)
        {
            var processo = new Processo();

            if (lote.Id > 0)
            {
                processo = this.processoRepositorio.ObterPorLoteId(lote.Id);

                if (processo == null)
                {
                    processo = new Processo();
                }
            }

            processo.Lote = lote;
            processo.Status = ProcessoStatus.AguardandoTransmissao;
            processo.Identificacao = model.IdentificacaoFormatada();
            processo.Barcode = model.MatriculaDossie + model.ContratoDossie + model.Hipoteca;
            processo.TipoDeProcesso = new TipoProcesso { Id = model.TipoDossie.ToInt() };
            processo.QtdePaginas = model.QuantidadeDePaginas.ToInt();
            processo.ObservacaoProcesso = model.ItensDocumentais;

            return processo;
        }
    }
}
