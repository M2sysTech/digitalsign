namespace Veros.Paperless.Model.Servicos.Validacao
{
    using System;
    using System.Linq;
    using Entidades;
    using Framework;
    using Repositorios;

    public class ValidaIndexacaoProcesso
    {
        private readonly ComplementaIndexacaoDocumentoMestreServico complementaIndexacaoDocumentoMestre;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IIndexacaoRepositorio indexacaoRepositorio;
        private readonly IProcessoRepositorio processoRepositorio;

        public ValidaIndexacaoProcesso(
            ComplementaIndexacaoDocumentoMestreServico complementaIndexacaoDocumentoMestre, 
            IDocumentoRepositorio documentoRepositorio,
            IIndexacaoRepositorio indexacaoRepositorio, 
            IProcessoRepositorio processoRepositorio)
        {
            this.complementaIndexacaoDocumentoMestre = complementaIndexacaoDocumentoMestre;
            this.documentoRepositorio = documentoRepositorio;
            this.indexacaoRepositorio = indexacaoRepositorio;
            this.processoRepositorio = processoRepositorio;
        }

        public void Execute(Processo processo)
        {
            Log.Application.InfoFormat("Validação do processo #{0}", processo.Id);  

            var documentosComprovacao = processo.ObterDocumentosComprovacao;
            var documentosMestre = processo.ObterDocumentosMestre();

            Log.Application.DebugFormat("Documentos de comprovacao #{0}", documentosComprovacao.Count());
            Log.Application.DebugFormat("Documentos mestre #{0}", documentosMestre.Count());

            foreach (var documentoComprovacao in documentosComprovacao)
            {
                if (documentoComprovacao.EstaBatido() == false)
                {
                    Log.Application.DebugFormat("Documento não batido #{0}", documentoComprovacao.Id);

                    this.SalvarIndexacaoBatida(documentoComprovacao);
                    this.documentoRepositorio.AtualizaStatusDocumento(
                        documentoComprovacao.Id,
                        DocumentoStatus.StatusParaProvaZero);

                    Log.Application.DebugFormat("Documento não batido #{0} salvo", documentoComprovacao.Id);
                }
                else
                {
                    this.SalvarIndexacaoBatida(documentoComprovacao);
                    
                    this.documentoRepositorio.AtualizaStatusDocumento(
                        documentoComprovacao.Id, 
                        DocumentoStatus.StatusValidado);

                    Log.Application.DebugFormat("Documento batido #{0} salvo", documentoComprovacao.Id);
                }

                this.complementaIndexacaoDocumentoMestre.Executar(documentosMestre, documentoComprovacao);

                this.processoRepositorio.AlterarStatus(processo.Id, ProcessoStatus.Validado);
                
                Log.Application.DebugFormat(
                    "Status do Processo #{0} alterado para {1}", 
                    processo.Id, 
                    ProcessoStatus.Validado);
            }

            Log.Application.InfoFormat("Processo #{0} avaliado com sucesso", processo.Id);
        }

        private void SalvarIndexacaoBatida(Documento documento)
        {
            foreach (var indexacao in documento.Indexacao)
            {
                this.indexacaoRepositorio.Salvar(indexacao);
                Log.Application.DebugFormat("Indexacao #{0} salva com sucesso.", indexacao.Id);
            }
        }
    }
}
