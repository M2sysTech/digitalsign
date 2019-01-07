namespace DigitalSign.Jobs
{
    using System;
    using Veros.Data;
    using Veros.Data.Jobs;
    using Veros.Framework;
    using Veros.Paperless.Model;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;
    using Veros.Paperless.Model.Servicos;

    public class SignErrorJob : Job 
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;
        private readonly ITagRepositorio tagRepositorio;

        public SignErrorJob(
            IUnitOfWork unitOfWork, 
            IDocumentoRepositorio documentoRepositorio, 
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico, 
            ITagRepositorio tagRepositorio)
        {
            this.unitOfWork = unitOfWork;
            this.documentoRepositorio = documentoRepositorio;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
            this.tagRepositorio = tagRepositorio;
        }

        public override void Execute()
        {
            var listaDocumentos = this.unitOfWork.Obter(() => this.documentoRepositorio.ObterDocumentosComErroDeAssinatura());

            foreach (var documento in listaDocumentos)
            {
                this.unitOfWork.Transacionar(() =>
                {
                    try
                    {
                        if (PodeAtualizarDocumento(documento, Contexto.TempoParaAssinarDocumentosComErro))
                        {
                            this.documentoRepositorio.AtualizaStatusDocumento(documento.Id, DocumentoStatus.IdentificacaoConcluida);

                            this.gravaLogDoDocumentoServico.Executar(LogDocumento.AcaoAssinaturaDigital, documento.Id, "Documento com erro atualizado para assinar");
                        }
                    }
                    catch (Exception exception)
                    {
                        Log.Application.Error(string.Format("Erro ao atualizar status do documento #{0}, no lote #{1}", documento.Id, documento.Lote.Id), exception);
                    }
                });
            }
        }

        private bool PodeAtualizarDocumento(Documento documento, int tempo)
        {
            return documento.HoraInicio == null || 
                ((DateTime) documento.HoraInicio).AddSeconds(tempo) < DateTime.Now;
        }
    }
}
