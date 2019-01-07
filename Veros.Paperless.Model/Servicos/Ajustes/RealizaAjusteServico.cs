namespace Veros.Paperless.Model.Servicos.Ajustes
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Entidades;
    using Filas;
    using Framework.IO;
    using Repositorios;
    using Veros.Data;
    using Veros.Framework;

    public class RealizaAjusteServico : IRealizaAjusteServico
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAjusteDeDocumentoRepositorio ajusteDeDocumentoRepositorio;
        private readonly IFilaClienteGenerica filaClienteGenerica;
        private readonly IAjusteImagemServico ajusteImagemServico;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly ILoteRepositorio loteRepositorio;

        public RealizaAjusteServico(
            IUnitOfWork unitOfWork,
            IAjusteDeDocumentoRepositorio ajusteDeDocumentoRepositorio,
            IFilaClienteGenerica filaClienteGenerica,
            IAjusteImagemServico ajusteImagemServico,
            IDocumentoRepositorio documentoRepositorio,
            IProcessoRepositorio processoRepositorio, 
            ILoteRepositorio loteRepositorio)
        {
            this.unitOfWork = unitOfWork;
            this.ajusteDeDocumentoRepositorio = ajusteDeDocumentoRepositorio;
            this.filaClienteGenerica = filaClienteGenerica;
            this.ajusteImagemServico = ajusteImagemServico;
            this.documentoRepositorio = documentoRepositorio;
            this.processoRepositorio = processoRepositorio;
            this.loteRepositorio = loteRepositorio;
        }

        public void Executar()
        {
            var processoId = 0;

            try
            {
                this.unitOfWork.Transacionar(() =>
                {
                    processoId = this.filaClienteGenerica.ObterProximo(TiposDeFila.ConsolidacaoAjusteService);
                });
            }
            catch (Exception exception)
            {
                Log.Application.ErrorFormat("Erro ao conectar com a fila: {0}", exception);
                return;
            }

            if (processoId == 0)
            {
                Log.Application.InfoFormat("Nenhum documento na fila");
                return;
            }

            var ajustesDoProcesso = this.unitOfWork.Obter(() =>
                this.ajusteDeDocumentoRepositorio.ObterPendentesPorProcessoComPaginas(processoId));

            if (ajustesDoProcesso.Count == 0)
            {
                this.ExcluirDocumentosCodigoEmAjustesSemAjustes(processoId);

                Log.Application.WarnFormat(
                    "Não foram encontrados ajustes pendentes para o processo #{0}",
                    processoId);

                this.unitOfWork.Transacionar(() =>
                {
                    var processo = this.processoRepositorio.ObterPorId(processoId);

                    if (processo != null)
                    {
                        this.loteRepositorio.AlterarStatus(processo.Lote.Id, LoteStatus.AjustesConcluidos);    
                    }
                });

                return;
            }

            try
            {
                this.unitOfWork.Transacionar(() =>
                {
                    this.ProcessarAjustesDocumento(ajustesDoProcesso);
                    this.processoRepositorio.LimparResponsavelEHoraInicio(processoId);
                    var processo = this.processoRepositorio.ObterPorId(processoId);
                    this.loteRepositorio.AlterarStatus(processo.Lote.Id, LoteStatus.AjustesConcluidos);
                });
            }
            catch (Exception exception)
            {
                Log.Application.Error(exception);
                throw;
            }
        }

        private void ExcluirDocumentosCodigoEmAjustesSemAjustes(int processoId)
        {
            this.unitOfWork.Transacionar(() =>
                this.documentoRepositorio.AtualizarStatusPorProcesso(processoId, DocumentoStatus.TelaAjusteFinalizado, DocumentoStatus.Excluido));
        }

        private void ProcessarAjustesDocumento(IList<AjusteDeDocumento> ajustes)
        {
            Log.Application.InfoFormat("Iniciando ajuste #{0} no documento #{1}", ajustes.First().Id, ajustes.First().Documento.Id);

            var processoId = ajustes.First().ProcessoId;

            ConfiguracaoAjusteImagem.ConfigurarCaminhoPadrao(processoId);

            this.ajusteImagemServico.Aplicar(ajustes);

            Log.Application.InfoFormat(
                "Ajuste(s) realizado com sucesso. Documento #{0}. Lote #{1}",
                ajustes.First().Documento.Id,
                ajustes.First().Documento.Lote.Id);

            try
            {
                var caminhoPadrao = Path.Combine(
                    Aplicacao.Caminho, 
                    string.Format("ImagesAjuste_{0}", processoId));

                Directories.DeleteIfExist(caminhoPadrao);
            }
            catch (Exception exception)
            {
                Log.Application.ErrorFormat(
                    exception,
                    "Erro ao excluir diretório de ajuste. Processo #{0}.", 
                    processoId);
            }
        }
    }
}
