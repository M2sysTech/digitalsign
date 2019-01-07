namespace Veros.Paperless.Model.Servicos.SeparacaoServiceEngine
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using AForge.Imaging.Filters;
    using AForge.Imaging.Formats;
    using Entidades;
    using Framework;
    using Framework.IO;

    public class CorrigeOrientacaoServico : ICorrigeOrientacaoServico
    {
        private readonly IPostaArquivoFileTransferServico postaArquivoFileTransferServico;
        private readonly IGravaLogDaPaginaServico gravaLogDaPaginaServico;
        private string cacheLocalImagens;
        private ITesseractServico tesseractServico;

        public CorrigeOrientacaoServico(ITesseractServico tesseractServico, 
            IPostaArquivoFileTransferServico postaArquivoFileTransferServico, 
            IGravaLogDaPaginaServico gravaLogDaPaginaServico)
        {
            this.tesseractServico = tesseractServico;
            this.postaArquivoFileTransferServico = postaArquivoFileTransferServico;
            this.gravaLogDaPaginaServico = gravaLogDaPaginaServico;
        }

        public void Executar(List<List<Pagina>> paginasDivididasPorSeparadora, string cacheLocalImagens)
        {
            this.cacheLocalImagens = cacheLocalImagens;
            this.tesseractServico.SetarDiretorioTessdata(Contexto.DiretorioTessdata);
            this.tesseractServico.SetarCacheLocalImagens(cacheLocalImagens);
            foreach (var documentoGrupo in paginasDivididasPorSeparadora)
            {
                foreach (var pagina in documentoGrupo.Where(x => x.Status != PaginaStatus.StatusExcluida && x.Separadora == false && x.ContrapartidaDeSeparadora == false && x.EmBranco == false))
                {
                    try
                    {
                        this.tesseractServico.CorrigirOrientacao(pagina);
                    }
                    catch (Exception exception)
                    {
                        pagina.OrientacaoTesseract = -1;
                        Log.Application.Error("Erro desconhecido ao tentar corrigir orientação na pagina:" + pagina.Id, exception);
                    }
                }

                this.AvaliarItemDocumental(documentoGrupo);
            }
        }

        //// Depois que as paginas foram analisadas individualmente, faz uma passada pra ver se alguma pagina está diferente das demais
        public void AvaliarItemDocumental(List<Pagina> documentoGrupo)
        {
            if (documentoGrupo.Count > 1)
            {
                var totalOrientacoes = new[] { 0, 0, 0, 0, 0 };
                for (int i = 0; i < documentoGrupo.Count; i++)
                {
                    var paginaAtual = documentoGrupo[i];
                    if (paginaAtual.Status != PaginaStatus.StatusExcluida && paginaAtual.Separadora == false && paginaAtual.ContrapartidaDeSeparadora == false && paginaAtual.EmBranco == false)
                    {
                        totalOrientacoes[paginaAtual.OrientacaoTesseract + 1] += 1;
                    }
                }

                var campeao = this.EncontrarMaisComum(totalOrientacoes);
                if (campeao > 1 && totalOrientacoes[campeao] > 0)
                {
                    //// so vai fazer alguma ação se for diferente da posicao retrato tradicional 
                    foreach (var pagina in documentoGrupo.Where(x => x.Status != PaginaStatus.StatusExcluida && x.Separadora == false && x.ContrapartidaDeSeparadora == false && x.EmBranco == false))
                    {
                        var arquivo = this.GirarPagina(pagina, campeao - 1);
                        this.PostarImagem(pagina, arquivo);
                        this.gravaLogDaPaginaServico.Executar(LogPagina.AcaoGiroTesseract, pagina.Id, pagina.Documento.Id, "Pagina girada pelo Separador. Direção:" + (campeao - 1));
                    }
                }
            }
        }

        private void PostarImagem(Pagina pagina, string arquivo)
        {
            var extensao = Path.GetExtension(arquivo).ToUpper().Replace(".", string.Empty);
            var caminhoNoFileTransfer = string.Format(
                    @"/{0}/F/{1}.{2}",
                    Files.GetEcmPath(pagina.Id),
                    pagina.Id.ToString("000000000"),
                    extensao)
                    .Replace("\\", "/").ToUpper();
            this.postaArquivoFileTransferServico.PostarArquivo(pagina.Id, arquivo, caminhoNoFileTransfer);
        }

        private string GirarPagina(Pagina pagina, int orientacao)
        {
            var caminhoArquivoBaixado = Path.Combine(this.cacheLocalImagens, pagina.Id.ToString("000000000") + "." + pagina.TipoArquivo);
            var nomeGirado = Path.Combine(Path.GetDirectoryName(caminhoArquivoBaixado), Path.GetFileNameWithoutExtension(caminhoArquivoBaixado) + "_Girada.JPG");
            switch (orientacao)
            {
                case 0:
                    //// faz nada
                    return caminhoArquivoBaixado;
                    break;
                case 1:
                    this.GirarArquivo(caminhoArquivoBaixado, nomeGirado, 90);
                    break;
                case 2:
                    this.GirarArquivo(caminhoArquivoBaixado, nomeGirado, -90);
                    break;
                case 3:
                    this.GirarArquivo(caminhoArquivoBaixado, nomeGirado, 180);
                    break;
            }

            return nomeGirado;
        }

        private int EncontrarMaisComum(int[] arrayInts)
        {
            var max = 0;
            var posicao = 0;
            //// descartando os que deram erro, localizar os giros mais comuns
            for (int i = 1; i < arrayInts.Length; i++)
            {
                if (arrayInts[i] >= max)
                {
                    max = arrayInts[i];
                    posicao = i;
                }
            }

            return posicao;
        }

        private void GirarArquivo(string arquivoOrigem, string arquivoGirado, double angulo)
        {
            var rotationPaisagem = new RotateBilinear(angulo)
            {
                FillColor = Color.White
            };

            var bitmap = ImageDecoder.DecodeFromFile(arquivoOrigem);
            var novaImagem = rotationPaisagem.Apply(bitmap);
            bitmap.Dispose();

            this.SalvarComo(novaImagem, arquivoGirado, System.Drawing.Imaging.ImageFormat.Jpeg, 8);
            novaImagem.Dispose();
        }

        private void SalvarComo(Bitmap imagemAtual, string nomeAlterado, System.Drawing.Imaging.ImageFormat imageFormat, long bpp)
        {
            var codecAtual = ImageCodecInfo.GetImageEncoders().Where(codec => codec.FormatID.Equals(imageFormat.Guid)).FirstOrDefault();
            if (codecAtual != null)
            {
                var parameters = new EncoderParameters(1);
                parameters.Param[0] = new EncoderParameter(Encoder.ColorDepth, bpp);
                imagemAtual.Save(nomeAlterado, codecAtual, parameters);
            }
            else
            {
                throw new Exception(string.Format("Codec informado não foi localizado:{0}", imageFormat.Guid));
            }
        }
    }
}