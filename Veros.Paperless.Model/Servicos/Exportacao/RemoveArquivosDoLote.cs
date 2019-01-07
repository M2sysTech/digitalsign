namespace Veros.Paperless.Model.Servicos.Exportacao
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Entidades;
    using Framework.IO;
    using Repositorios;

    public class RemoveArquivosDoLote : IRemoveArquivosDoLote
    {
        private readonly IRemoverPaginaFileTransfer removerPaginaFileTransfer;
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly ILoteRepositorio loteRepositorio;
        private readonly IGravaLogDaPaginaServico gravaLogDaPaginaServico;
        private readonly IUnitOfWork unitOfWork;

        public RemoveArquivosDoLote(
            IRemoverPaginaFileTransfer removerPaginaFileTransfer, 
            IPaginaRepositorio paginaRepositorio, 
            ILoteRepositorio loteRepositorio, 
            IGravaLogDaPaginaServico gravaLogDaPaginaServico, 
            IUnitOfWork unitOfWork)
        {
            this.removerPaginaFileTransfer = removerPaginaFileTransfer;
            this.paginaRepositorio = paginaRepositorio;
            this.loteRepositorio = loteRepositorio;
            this.gravaLogDaPaginaServico = gravaLogDaPaginaServico;
            this.unitOfWork = unitOfWork;
        }

        public void Executar(int loteId)
        {
            var lote = this.unitOfWork.Obter(() => this.loteRepositorio.ObterPorId(loteId));

            if (lote.CloudOk == false)
            {
                throw new Exception("FATAL:Lote ainda não foi enviado para o cloud. a operação de remoção não poderá ser concluída");
            }

            if (lote.JpegsEnviadosParaCloud == false)
            {
                throw new Exception("FATAL:Jpegs ainda não foram enviados para o cloud. A operação de remoção não poderá ser concluída");
            }

            var paginas = this.unitOfWork.Obter(() => this.paginaRepositorio.ObterPorLote(lote));
            var paginasParaRemover = paginas.Where(x => x.RemovidoFileTransferM2 == false && x.CloudOk);

            var arquivos = new Dictionary<Pagina, string>();

            foreach (var pagina in paginasParaRemover)
            {
                this.unitOfWork.Transacionar(() =>
                {
                    var nomeArquivo = string.Format(
                        "{0:000000000}.{1}",
                        pagina.Id,
                        pagina.TipoArquivo);

                    var caminhoRemoto = string.Format("{0}/F/{1}",
                        Files.GetEcmPath(pagina.Id),
                        nomeArquivo);

                    caminhoRemoto = caminhoRemoto.Replace("\\", "/").ToUpper();
                    arquivos.Add(pagina, caminhoRemoto);
                });
            }

            if (arquivos.Count == 0)
            {
                return;
            }

            this.unitOfWork.Transacionar(() =>
            {
                this.removerPaginaFileTransfer.Executar(arquivos);
                this.loteRepositorio.MarcarComoRemovidoFileTransferM2(loteId);
            });
        }
    }
}