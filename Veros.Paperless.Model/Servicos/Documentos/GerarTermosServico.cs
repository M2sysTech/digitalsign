namespace Veros.Paperless.Model.Servicos.Documentos
{
    using System;
    using System.Linq;
    using Data;
    using Framework;
    using Repositorios;

    public class GerarTermosServico : IGerarTermosServico
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IContarPaginasPdfDoProcesso contarPaginasPdfDoProcesso;
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly IGerarFolhaRostoVirtual gerarFolhaRostoVirtual;
        private readonly IGerarTermoDeAutuacaoVirtual gerarTermoDeAutuacaoVirtual;
        
        public GerarTermosServico(
            IUnitOfWork unitOfWork, 
            IDocumentoRepositorio documentoRepositorio, 
            IContarPaginasPdfDoProcesso contarPaginasPdfDoProcesso, 
            IProcessoRepositorio processoRepositorio, 
            IGerarFolhaRostoVirtual gerarFolhaRostoVirtual, 
            IGerarTermoDeAutuacaoVirtual gerarTermoDeAutuacaoVirtual)
        {
            this.unitOfWork = unitOfWork;
            this.documentoRepositorio = documentoRepositorio;
            this.contarPaginasPdfDoProcesso = contarPaginasPdfDoProcesso;
            this.processoRepositorio = processoRepositorio;
            this.gerarFolhaRostoVirtual = gerarFolhaRostoVirtual;
            this.gerarTermoDeAutuacaoVirtual = gerarTermoDeAutuacaoVirtual;
        }

        public void Executar(int processoId)
        {
            this.unitOfWork.Transacionar(() =>
            {
                var totalPaginas = this.contarPaginasPdfDoProcesso.Executar(processoId);
                this.processoRepositorio.AtualizarQuantidadeDePaginas(processoId, totalPaginas);
            });

            try
            {
                this.unitOfWork.Transacionar(() =>
                {
                    Log.Application.InfoFormat("Gerando termos do processo #{0}", processoId);

                    var processo = this.processoRepositorio.ObterPorIdComTipoDeProcesso(processoId);

                    this.gerarFolhaRostoVirtual.Executar(processo.Id);

                    this.gerarTermoDeAutuacaoVirtual.Executar(processo.Id);

                    Log.Application.InfoFormat(
                        "Geração de termos realizado com sucesso. processo #{0}. lote #{1}", 
                        processoId, 
                        processo.Lote.Id);
                });
            }
            catch (Exception exception)
            {
                Log.Application.ErrorFormat(exception, "Termos não gerados para o processo #{0}", processoId);
                throw;
            }
        }
    }
}