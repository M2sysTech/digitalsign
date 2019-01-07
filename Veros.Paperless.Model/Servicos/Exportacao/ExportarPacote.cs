namespace Veros.Paperless.Model.Servicos.Exportacao
{
    using System.IO;
    using System.Xml.Serialization;
    using Comparacao;
    using Entidades;
    using Framework.IO;
    using Repositorios;

    public class ExportarPacote
    {
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico;
        private readonly IFileSystem fileSystem;

        public ExportarPacote(
            IPaginaRepositorio paginaRepositorio, 
            IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico, 
            IFileSystem fileSystem)
        {
            this.paginaRepositorio = paginaRepositorio;
            this.baixaArquivoFileTransferServico = baixaArquivoFileTransferServico;
            this.fileSystem = fileSystem;
        }

        public void Executar(PacoteParaExportar pacoteParaExportar)
        {
            var nomePacote = string.Format(
                "{0}_{1}_{2}", 
                pacoteParaExportar.Identificacao.RemoverDiacritico(),
                pacoteParaExportar.Data.ToString("ddMMyyyy"),
                pacoteParaExportar.Regiao);

            var caminhoExportacaoDoPacote = Path.Combine(
                Configuracao.CaminhoDeArquivosExportacao, 
                nomePacote);

            Directories.CreateIfNotExist(caminhoExportacaoDoPacote);

            var caminhoPacoteXml = Path.Combine(caminhoExportacaoDoPacote, nomePacote + ".xml");
            this.CriarXml(pacoteParaExportar, caminhoPacoteXml);

            foreach (var dossie in pacoteParaExportar.Dossies)
            {
                this.GerarXmlDossie(caminhoExportacaoDoPacote, dossie);
                this.GerarItensDocumentais(caminhoExportacaoDoPacote, dossie);
            }
        }

        private void GerarItensDocumentais(string caminhoExportacaoDoPacote, Dossie dossie)
        {
            foreach (var item in dossie.ItensDocumentais)
            {
                var caminhoItemDocumental = Path.Combine(
                    caminhoExportacaoDoPacote,
                    dossie.NumeroContrato.RemoverDiacritico().Replace(@"/", string.Empty) + "_" + dossie.TipoDossieId,
                    item.TipoDocumento.RemoverDiacritico().RemoverEspacosEntreAsPalavras());

                Directories.CreateIfNotExist(caminhoItemDocumental);

                var caminhoItemDocumentalXml = Path.Combine(
                   caminhoItemDocumental,
                   item.NomeParaArquivo + ".xml");

                this.CriarXmlItemDocumental(caminhoItemDocumentalXml, item);
                this.BaixarPdfItemDocumental(item, caminhoItemDocumental);
            }
        }

        private void CriarXmlItemDocumental(string caminhoItemDocumental, ItemDocumental item)
        {
            var serializer = new XmlSerializer(typeof(ItemDocumental));

            Files.DeleteIfExist(caminhoItemDocumental);
            
            using (var filestream = new FileStream(caminhoItemDocumental, FileMode.CreateNew))
            {
                serializer.Serialize(filestream, item);
            }
        }

        private void BaixarPdfItemDocumental(ItemDocumental item, string caminhoDoDossie)
        {
            var pagina = this.paginaRepositorio.ObterPdfDocumento(item.DocumentoId);
            var arquivo = this.baixaArquivoFileTransferServico.BaixarArquivo(pagina.Id, pagina.TipoArquivo);

            var caminhoItemDocumental = Path.Combine(
                    caminhoDoDossie,
                    item.NomeParaArquivo + ".pdf");

            this.fileSystem.Copy(arquivo, caminhoItemDocumental);
            this.fileSystem.DeleteFile(arquivo);
        }

        private void GerarXmlDossie(string caminhoExportacaoDoPacote, Dossie dossie)
        {
            var nomeDossie = string.Format(
                "{0}_{1}",
                dossie.NumeroContrato.RemoverDiacritico().Replace(@"/", string.Empty),
                dossie.TipoDossieId);

            var caminhoDossie = Path.Combine(caminhoExportacaoDoPacote, nomeDossie);
            Directories.CreateIfNotExist(caminhoDossie);

            caminhoDossie = Path.Combine(caminhoDossie, nomeDossie + ".xml");

            this.CriarXmlDossie(dossie, caminhoDossie);
        }

        private void CriarXml(PacoteParaExportar pacote, string nomeArquivoXml)
        {
            var serializer = new XmlSerializer(typeof(PacoteParaExportar));

            Files.DeleteIfExist(nomeArquivoXml);

            using (var filestream = new FileStream(nomeArquivoXml, FileMode.CreateNew))
            {
                serializer.Serialize(filestream, pacote);
            }
        }

        private void CriarXmlDossie(Dossie dossie, string nomeArquivoXml)
        {
            var serializer = new XmlSerializer(typeof(Dossie));

            Files.DeleteIfExist(nomeArquivoXml);

            using (var filestream = new FileStream(nomeArquivoXml, FileMode.CreateNew))
            {
                serializer.Serialize(filestream, dossie);
            }
        }
    }
}