namespace Veros.Paperless.Model.Servicos.Reconhecimento
{
    using System;
    using Veros.Data;
    using System.IO;
    using Entidades;
    using Framework;
    using Framework.IO;

    public class ObterImagemParaReconhecimentoServico : IObterImagemParaReconhecimentoServico
    {
        private readonly IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico;
        private readonly IFileSystem fileSystem;
        private readonly IUnitOfWork unitOfWork;

        public ObterImagemParaReconhecimentoServico(
            IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico, 
            IFileSystem fileSystem, 
            IUnitOfWork unitOfWork)
        {
            this.baixaArquivoFileTransferServico = baixaArquivoFileTransferServico;
            this.fileSystem = fileSystem;
            this.unitOfWork = unitOfWork;
        }

        public Imagem Executar(Pagina pagina)
        {
            var imagem = new Imagem();

            string caminhoImagem;

            try
            {
                caminhoImagem = this.baixaArquivoFileTransferServico
                    .BaixarArquivo(pagina.Id, pagina.TipoArquivo, false, true);
                
                if (this.fileSystem.Exists(caminhoImagem))
                {
                    imagem.Caminho = caminhoImagem;
                    imagem.Original = true;
                    imagem.Pagina = pagina;
                    return imagem;
                }
            }
            catch (FileNotFoundException)
            {
                Log.Application.DebugFormat("Imagem original da pagina {0} não existe", pagina.Id);
            }

            caminhoImagem = this.baixaArquivoFileTransferServico
                .BaixarArquivo(pagina.Id, pagina.TipoArquivo);

            if (this.fileSystem.Exists(caminhoImagem))
            {
                imagem.Caminho = caminhoImagem;
                imagem.Original = false;
                imagem.Pagina = pagina;
                return imagem;
            }

            return null;
        }

        public Imagem ExecutarUsandoCache(Pagina pagina, string extensaoPreferida)
        {
            var imagem = new Imagem();

            string caminhoImagem;

            try
            {
                caminhoImagem = this.baixaArquivoFileTransferServico
                    .BaixarArquivo(pagina.Id, extensaoPreferida, false, true);

                if (this.fileSystem.Exists(caminhoImagem))
                {
                    imagem.Caminho = caminhoImagem;
                    imagem.Original = false;
                    imagem.Pagina = pagina;
                    imagem.Pagina.TipoArquivo = extensaoPreferida.ToUpper();
                    return imagem;
                }
            }
            catch (FileNotFoundException)
            {
                Log.Application.DebugFormat("Imagem com ext {0} da pagina {1} não existe", extensaoPreferida, pagina.Id);
            }

            var procuraCache = Path.Combine(
                Contexto.PastaArquivosDossie,
                pagina.Lote.Id.ToString(),
                pagina.NomeDoArquivo);

            ////if (File.Exists(procuraCache))
            ////{
            ////    var imagemCache = new Imagem { Caminho = procuraCache, Original = false, Pagina = pagina };
            ////    return imagemCache;
            ////}

            ////Log.Application.DebugFormat("Imagem da pagina {0} não localizada no cache. Baixando arquivo do FT", pagina.Id);

            caminhoImagem = this.baixaArquivoFileTransferServico.BaixarArquivo(pagina.Id, pagina.TipoArquivo, false, true, false);

            if (this.fileSystem.Exists(caminhoImagem))
            {
                imagem.Caminho = caminhoImagem;
                imagem.Original = false;
                imagem.Pagina = pagina;
                return imagem;
            }

            return null;            
        }
    }
}