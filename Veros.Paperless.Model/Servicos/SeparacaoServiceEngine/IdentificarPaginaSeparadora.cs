namespace Veros.Paperless.Model.Servicos.SeparacaoServiceEngine
{
    using System;
    using System.Collections.Generic;
    using Entidades;
    using Framework;
    using Image.Barcodes;
    using Validacao;

    public class IdentificarPaginaSeparadora : IIdentificarPaginaSeparadora
    {
        private readonly IBarcodeRecognizer barcodeRecognize;
        private readonly IManipulaImagemServico manipulaImagemServico;

        public IdentificarPaginaSeparadora(
            IBarcodeRecognizer barcodeRecognize,
            IManipulaImagemServico manipulaImagemServico)
        {
            this.barcodeRecognize = barcodeRecognize;
            this.manipulaImagemServico = manipulaImagemServico;
        }

        public ItemParaSeparacao Executar(IList<ItemParaSeparacao> todasAsPaginas, ItemParaSeparacao item)
        {
            var barcode = string.Empty;

            var pagina = item.Pagina;
            var bitmapProcessado = item.ImagemProcessada.BitmapProcessado;
            var retangulos = item.ImagemProcessada.Retangulos;
            var arquivo = item.ArquivoBaixado;

            if (pagina.EmBranco)
            {
                bitmapProcessado.Dispose();
                return item;
            }

            //// se ja indentificou separador antes (carimbo)...
            if (pagina.Separadora)
            {
                this.MarcarContraParteDaFolhaSeparadoraComoBranco(pagina, todasAsPaginas);
                bitmapProcessado.Dispose();
                return item;
            }

            try
            {
                barcode = this.barcodeRecognize.Recognize(arquivo);

                if (barcode == CodigoDeBarras.CapaSeparadora)
                {
                    pagina.Separadora = true;
                    pagina.AnaliseConteudoOcr = PaginaTipoOcr.TipoSeparadorSoftek;
                    this.MarcarContraParteDaFolhaSeparadoraComoBranco(pagina, todasAsPaginas);
                    bitmapProcessado.Dispose();
                    return item;
                }
            }
            catch (ArgumentException ex)
            {
                Log.Application.Warn("Não conseguiu ler codigo de barras");
            }

            if (this.manipulaImagemServico.EhSeparador(bitmapProcessado, retangulos, arquivo))
            {
                pagina.Separadora = true;
                pagina.AnaliseConteudoOcr = PaginaTipoOcr.TipoSeparadorBarcode;
                this.MarcarContraParteDaFolhaSeparadoraComoBranco(pagina, todasAsPaginas);
            }

            bitmapProcessado.Dispose();
            return item;
        }

        private void MarcarContraParteDaFolhaSeparadoraComoBranco(
            Pagina paginaSeparador,
            IList<ItemParaSeparacao> itensComBrancosIdentificados)
        {
            var nomeArquivoSeparador = paginaSeparador
                .NomeArquivoSemExtensao
                .Replace("F", string.Empty)
                .Replace("V", string.Empty);

            foreach (var item in itensComBrancosIdentificados)
            {
                var pagina = item.Pagina;

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
                        pagina.AnaliseConteudoOcr = PaginaTipoOcr.TipoSeparadorContraparte;
                    }
                }
            }
        }
    }
}