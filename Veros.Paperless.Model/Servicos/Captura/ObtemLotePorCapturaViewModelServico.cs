namespace Veros.Paperless.Model.Servicos.Captura
{
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.ViewModel;
    using Veros.Paperless.Model.Repositorios;
    using System;

    public class ObtemLotePorCapturaViewModelServico : IObtemLotePorCapturaViewModelServico
    {
        private readonly ILoteRepositorio loteRepositorio;

        public ObtemLotePorCapturaViewModelServico(ILoteRepositorio loteRepositorio)
        {
            this.loteRepositorio = loteRepositorio;
        }

        public Lote Executar(CapturaViewModel model, Pacote pacote)
        {
            var lote = new Lote();

            if (model.LoteId > 0)
            {
                lote = this.loteRepositorio.ObterPorId(model.LoteId) ?? new Lote();
            }

            lote.Status = LoteStatus.EmCaptura;
            lote.DataCriacao = DateTime.Now;
            lote.Identificacao = model.BarcodeCaixa;
            lote.Pacote = pacote;
            lote.Batido = "N";
            lote.Agencia = pacote.Estacao;
            lote.PrioridadeNasFilas = "N";
            lote.Grupo = string.Format(@"{0:0000}", pacote.Estacao).Substring(3);

            if (model.DossieId > 0)
            {
                lote.DossieEsperado = new DossieEsperado { Id = model.DossieId };
            }

            return lote;
        }
    }
}
