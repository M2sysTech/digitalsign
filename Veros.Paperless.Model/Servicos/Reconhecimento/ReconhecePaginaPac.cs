namespace Veros.Paperless.Model.Servicos.Reconhecimento
{
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Framework;
    using Image.Reconhecimento;

    public class ReconhecePaginaPac : IReconhecePaginaPac
    {
        private readonly IBaixaArquivoFileTransferServico baixarArquivoFileTransfer;
        private readonly ISalvarReconhecimentoPaginaServico salvarReconhecimentoPaginaServico;
        private readonly IAdicionaQuantidadeLicencaConsumida adicionaQuantidadeLicencaConsumida;
        private readonly IReconheceImagem reconheceImagem;

        public ReconhecePaginaPac(
            IBaixaArquivoFileTransferServico baixarArquivoFileTransfer, 
            ISalvarReconhecimentoPaginaServico salvarReconhecimentoPaginaServico, 
            IAdicionaQuantidadeLicencaConsumida adicionaQuantidadeLicencaConsumida, 
            IReconheceImagem reconheceImagem)
        {
            this.baixarArquivoFileTransfer = baixarArquivoFileTransfer;
            this.salvarReconhecimentoPaginaServico = salvarReconhecimentoPaginaServico;
            this.adicionaQuantidadeLicencaConsumida = adicionaQuantidadeLicencaConsumida;
            this.reconheceImagem = reconheceImagem;
        }

        public void Executar(int numeroPagina, IList<Pagina> paginas)
        {
            var pagina = paginas.FirstOrDefault(x => x.Ordem == numeroPagina);

            if (pagina == null)
            {
                return;
            }

            var imagemPaginaPac = this.baixarArquivoFileTransfer
                .BaixarArquivo(pagina.Id, pagina.TipoArquivo);

            var numeroPaginaReconhecivel = (numeroPagina == 5 || numeroPagina == 3 || numeroPagina == 2)
                ? Image.TipoDocumentoReconhecivel.PacPagina5
                : Image.TipoDocumentoReconhecivel.PacPagina6;

            var resultadoReconhecimento = this.reconheceImagem
                .Reconhecer(imagemPaginaPac, numeroPaginaReconhecivel);

            this.salvarReconhecimentoPaginaServico
                .Executar(pagina, resultadoReconhecimento);

            this.adicionaQuantidadeLicencaConsumida
              .Executar(pagina, resultadoReconhecimento.LicencasUtilizadas);
        }
    }
}