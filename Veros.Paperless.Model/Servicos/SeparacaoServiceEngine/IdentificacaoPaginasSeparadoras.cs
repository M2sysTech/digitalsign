namespace Veros.Paperless.Model.Servicos.SeparacaoServiceEngine
{
    using System;
    using System.Collections.Generic;
    using Entidades;
    using Framework;
    using Framework.IO;
    using Image.Barcodes;
    using Validacao;

    public class IdentificacaoPaginasSeparadoras : IIdentificacaoPaginasSeparadoras
    {
        private readonly IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico;
        private readonly IBarcodeRecognizer barcodeRecognize;
        private readonly IManipulaImagemServico manipulaImagemServico;
        private readonly IFileSystem fileSystem;

        public IdentificacaoPaginasSeparadoras(
            IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico,
            IBarcodeRecognizer barcodeRecognize, 
            IManipulaImagemServico manipulaImagemServico, 
            IFileSystem fileSystem)
        {
            this.baixaArquivoFileTransferServico = baixaArquivoFileTransferServico;
            this.barcodeRecognize = barcodeRecognize;
            this.manipulaImagemServico = manipulaImagemServico;
            this.fileSystem = fileSystem;
        }

        public IList<Pagina> Executar(Documento documento, IList<Pagina> paginasJpeg)
        {
            var deleteSchedulle = new List<string>();

            foreach (var pagina in paginasJpeg)
            {
                Log.Application.DebugFormat("Processando pagina #{0}", pagina.Id);

                var arquivo = this.baixaArquivoFileTransferServico.BaixarArquivo(pagina.Id, pagina.TipoArquivo);
                deleteSchedulle.Add(arquivo);

                string barcode;

                try
                {
                    if (pagina.EmBranco)
                    {
                        continue;
                    }

                    barcode = this.barcodeRecognize.Recognize(arquivo);

                    if (barcode == CodigoDeBarras.CapaSeparadora ||
                        this.manipulaImagemServico.EhSeparador(arquivo))
                    {
                        pagina.Separadora = true;
                        this.MarcarContraParteDaFolhaSeparadoraComoBranco(pagina, paginasJpeg);
                    }
                }
                catch (Exception exception)
                {
                    Log.Application.Error(exception);
                    throw;
                }
            }

            this.ExcluirImagensTemporarias(deleteSchedulle);

            return paginasJpeg;
        }

        private void MarcarContraParteDaFolhaSeparadoraComoBranco(
            Pagina paginaSeparador,
            IEnumerable<Pagina> paginasJpeg)
        {
            var nomeArquivoSeparador = paginaSeparador
                .NomeArquivoSemExtensao
                .Replace("F", string.Empty)
                .Replace("V", string.Empty);

            foreach (var pagina in paginasJpeg)
            {
                var nomeArquivoParSeparador = pagina
                    .NomeArquivoSemExtensao
                    .Replace("F", string.Empty)
                    .Replace("V", string.Empty);

                if (nomeArquivoSeparador == nomeArquivoParSeparador)
                {
                    if (pagina.Separadora == false)
                    {
                        pagina.EmBranco = true;
                        pagina.ContrapartidaDeSeparadora = true;
                    }
                }
            }
        }

        private void ExcluirImagensTemporarias(IEnumerable<string> deleteSchedulle)
        {
            foreach (var arquivo in deleteSchedulle)
            {
                try
                {
                    this.fileSystem.DeleteFile(arquivo);
                }
                catch (Exception exception)
                {
                    Log.Application.ErrorFormat("Erro ao tentar excluir imagem: {0} : erro:{1} ", arquivo, exception);
                }
            }
        }
    }
}