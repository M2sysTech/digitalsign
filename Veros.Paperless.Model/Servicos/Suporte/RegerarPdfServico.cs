namespace Veros.Paperless.Model.Servicos.Suporte
{
    using System;
    using Entidades;
    using Framework;
    using Framework.Modelo;
    using Repositorios;

    public class RegerarPdfServico : IRegerarPdfServico
    {
        private readonly IProcedureRepositorio procedureRepositorio;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IGravaLogDoLoteServico gravaLogDoLoteServico;

        public RegerarPdfServico(IProcedureRepositorio procedureRepositorio, 
            IDocumentoRepositorio documentoRepositorio, 
            IGravaLogDoLoteServico gravaLogDoLoteServico)
        {
            this.procedureRepositorio = procedureRepositorio;
            this.documentoRepositorio = documentoRepositorio;
            this.gravaLogDoLoteServico = gravaLogDoLoteServico;
        }

        public void Executar(int documentoId)
        {
            var documento = this.documentoRepositorio.ObterComPacote(documentoId);

            if (documento == null)
            {
                throw new RegraDeNegocioException(string.Format("Documento [{0}] não encontrado.", documentoId));   
            }

            if (documento.Lote.Status != LoteStatus.AguardandoAjustes)
            {
                throw new RegraDeNegocioException(string.Format("O Dossie (batch_code [{0}]) desse documento não está na fase de Ajuste.", documento.Lote.Id));
            }

            try
            {
                this.gravaLogDoLoteServico.Executar(LogLote.AcaoPortalSuporte, documento.Lote.Id, string.Format("PORTAL SUPORTE: Solicitação de regerar PDF [{0}]", documentoId));
                this.procedureRepositorio.RegerarPdfPorDocumento(documentoId);
            }
            catch (Exception exception)
            {
                Log.Application.Error("Erro ao executar procedure REGERARPDFPORDOCUMENTO : ", exception);   
                throw;
            }
        }
    }
}
