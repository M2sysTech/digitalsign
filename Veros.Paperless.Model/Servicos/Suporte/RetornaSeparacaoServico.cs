namespace Veros.Paperless.Model.Servicos.Suporte
{
    using System;
    using Entidades;
    using Framework;
    using Framework.Modelo;
    using Repositorios;

    public class RetornaSeparacaoServico : IRetornaSeparacaoServico
    {
        private readonly IProcedureRepositorio procedureRepositorio;
        private readonly IGravaLogDoLoteServico gravaLogDoLoteServico;
        private readonly ILoteRepositorio loteRepositorio;

        public RetornaSeparacaoServico(IProcedureRepositorio procedureRepositorio, 
            IGravaLogDoLoteServico gravaLogDoLoteServico, 
            ILoteRepositorio loteRepositorio)
        {
            this.procedureRepositorio = procedureRepositorio;
            this.gravaLogDoLoteServico = gravaLogDoLoteServico;
            this.loteRepositorio = loteRepositorio;
        }

        public void Executar(int loteId)
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
                this.gravaLogDoLoteServico.Executar(LogLote.AcaoPortalSuporte, loteId, "PORTAL SUPORTE: Lote retornado para Separação Automatica");
                this.procedureRepositorio.RetornarLoteParaSeparacao(loteId);
            }
            catch (Exception exception)
            {
                Log.Application.Error("Erro ao executar procedure RETORNAPARASEPARACAO : ", exception);
                throw;
            }
        }
    }
}
