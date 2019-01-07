namespace Veros.Paperless.Model.Servicos.Suporte
{
    using System;
    using Entidades;
    using Framework;
    using Framework.Modelo;
    using Repositorios;

    public class SetarExcluidoServico : ISetarExcluidoServico
    {
        private readonly IGravaLogDoLoteServico gravaLogDoLoteServico;
        private readonly ILoteRepositorio loteRepositorio;
        private readonly IProcessoRepositorio processoRepositorio;

        public SetarExcluidoServico(
            IGravaLogDoLoteServico gravaLogDoLoteServico, 
            ILoteRepositorio loteRepositorio, IProcessoRepositorio processoRepositorio)
        {
            this.gravaLogDoLoteServico = gravaLogDoLoteServico;
            this.loteRepositorio = loteRepositorio;
            this.processoRepositorio = processoRepositorio;
        }

        public void Executar(int loteId, string motivo)
        {
            var lote = this.loteRepositorio.ObterComPacote(loteId);
            if (lote == null)
            {
                throw new RegraDeNegocioException(string.Format("Dossiê (lote) [{0}] não encontrado.", loteId));   
            }

            if (lote.PacoteProcessado.StatusPacote == StatusPacote.AprovadoNaQualidade || lote.ResultadoQualidadeCef == "A")
            {
                throw new RegraDeNegocioException(string.Format("Este Dossiê (lote) [{0}] já foi aprovado pela CEF.", loteId));
            }

            try
            {
                this.loteRepositorio.SetarExcluido(loteId, motivo);
                this.processoRepositorio.AlterarStatusPorLote(loteId, ProcessoStatus.Excluido);
                this.gravaLogDoLoteServico.Executar(LogLote.AcaoPortalSuporte, loteId, "PORTAL SUPORTE: Lote setado como excluido");
            }
            catch (Exception exception)
            {
                Log.Application.Error(string.Format("Erro ao setar lote [{0}] para excluido : ", loteId), exception);
                throw;
            }
        }
    }
}
