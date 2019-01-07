namespace Veros.Paperless.Model.Servicos.Pdf
{
    using System.Collections.Generic;
    using Entidades;

    public interface IPdfServico
    {
        string MontarPdfComImagens(List<string> listaDeImagens, string nomeArquivoPdf);

        string MontarPdfPesquisavel(List<Pagina> listaDePaginas, string nomeArquivoPdf, IList<PalavraReconhecida> palavrasReconhecidas);

        string GerarPdfFolhaRosto(Processo processo);

        string GerarPdfTermoAutuacao(Processo processo);

        string GerarPdfCertificadoGarantia(LoteCef lotecef, string nomeArquivo);

        int ContarPaginas(string pdf);
    }
}
