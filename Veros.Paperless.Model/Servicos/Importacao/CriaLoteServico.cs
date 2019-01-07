namespace Veros.Paperless.Model.Servicos.Importacao
{
    using System;
    using Consultas;
    using Repositorios;
    using Veros.Framework;
    using Veros.Paperless.Model.Entidades;

    public class CriaLoteServico : ICriaLoteServico
    {
        private readonly IPacoteFactory pacoteFactory;
        private readonly ICriaPacoteProcessadoServico criaPacoteProcessado;
        private readonly ISequencialDeIdentificadorConsulta sequencialDeIdentificadorConsulta;
        private readonly IPacoteRepositorio pacoteRepositorio;
        private readonly ILoteRepositorio loteRepositorio;

        public CriaLoteServico(
            IPacoteFactory pacoteFactory, 
            ICriaPacoteProcessadoServico criaPacoteProcessado,
            ISequencialDeIdentificadorConsulta sequencialDeIdentificadorConsulta,
            IPacoteRepositorio pacoteRepositorio,
            ILoteRepositorio loteRepositorio)
        {
            this.pacoteFactory = pacoteFactory;
            this.criaPacoteProcessado = criaPacoteProcessado;
            this.sequencialDeIdentificadorConsulta = sequencialDeIdentificadorConsulta;
            this.pacoteRepositorio = pacoteRepositorio;
            this.loteRepositorio = loteRepositorio;
        }

        public Lote Criar(string identificacao)
        {
            var pacote = this.pacoteFactory.Criar();

            Log.Application.Debug("Criando lote");

            var lote = new Lote
            {
                Identificacao = identificacao,
                Pacote = pacote,
                Status = LoteStatus.AguardandoTransmissao,
                Batido = "N",
                DataCriacao = DateTime.Now,
                Agencia = pacote.Estacao,
                PrioridadeNasFilas = "N",
                Grupo = string.Format(@"{0:0000}", pacote.Estacao).Substring(3),
                PacoteProcessado = this.criaPacoteProcessado.Criar()
            };

            return lote;
        }

        public Lote CriarNaFaseCaptura()
        {
            var lote = this.Criar(DateTime.Now.ToString("ddHHmmss"));
            lote.Status = LoteStatus.EmCaptura;
            
            this.pacoteRepositorio.Salvar(lote.Pacote);
            this.loteRepositorio.Salvar(lote);
            
            return lote;
        }
    }
}
