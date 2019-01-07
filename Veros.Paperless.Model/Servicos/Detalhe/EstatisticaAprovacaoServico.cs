namespace Veros.Paperless.Model.Servicos.Detalhe
{
    using System;
    using Veros.Paperless.Model.Entidades;
    using Veros.Framework.Modelo;
    using Veros.Paperless.Model.Repositorios;

    public class EstatisticaAprovacaoServico
    {
        private readonly IEstatisticaAprovacaoRepositorio estatisticaAprovacaoRepositorio;
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly IUsuarioRepositorio usuarioRepositorio;

        public EstatisticaAprovacaoServico(IEstatisticaAprovacaoRepositorio estatisticaAprovacaoRepositorio, 
            IProcessoRepositorio processoRepositorio, 
            IUsuarioRepositorio usuarioRepositorio)
        {
            this.estatisticaAprovacaoRepositorio = estatisticaAprovacaoRepositorio;
            this.processoRepositorio = processoRepositorio;
            this.usuarioRepositorio = usuarioRepositorio;
        }

        public void IncrementarDevolvidasParaHoje(IUsuario usuarioAtual, int processoId)
        {
            var estatistica = this.estatisticaAprovacaoRepositorio.ObterDeHojePorUsuario(usuarioAtual.Id);

            if (estatistica == null)
            {
                estatistica = new EstatisticaAprovacao()
                {
                    DataRegistro = DateTime.Today.Date,
                    Usuario = this.usuarioRepositorio.ObterPorId(usuarioAtual.Id)
                };
            }

            if (this.processoRepositorio.ExisteDocumentoComFraude(processoId))
            {
                estatistica.TotalDevolvidasComFraude++;
            }
            else
            {
                estatistica.TotalDevolvidas++;
            }
            
            this.estatisticaAprovacaoRepositorio.Salvar(estatistica);   
        }

        public void IncrementarLiberadasParaHoje(IUsuario usuarioAtual, int processoId)
        {
            var estatistica = this.estatisticaAprovacaoRepositorio.ObterDeHojePorUsuario(usuarioAtual.Id);

            if (estatistica == null)
            {
                estatistica = new EstatisticaAprovacao()
                {
                    DataRegistro = DateTime.Today.Date,
                    Usuario = this.usuarioRepositorio.ObterPorId(usuarioAtual.Id)
                };
            }

            if (this.processoRepositorio.ExisteDocumentoComFraude(processoId))
            {
                estatistica.TotalLiberadasComFraude++;
            }
            else
            {
                estatistica.TotalLiberadas++;
            }

            this.estatisticaAprovacaoRepositorio.Salvar(estatistica);   
        }

        public void IncrementarAbandonadasParaHoje(int usuarioId)
        {
            var estatistica = this.estatisticaAprovacaoRepositorio.ObterDeHojePorUsuario(usuarioId);

            if (estatistica == null)
            {
                estatistica = new EstatisticaAprovacao()
                {
                    DataRegistro = DateTime.Today.Date,
                    Usuario = this.usuarioRepositorio.ObterPorId(usuarioId)
                };
            }

            estatistica.TotalAbandonadas++;

            this.estatisticaAprovacaoRepositorio.Salvar(estatistica);
        }
    }
}
