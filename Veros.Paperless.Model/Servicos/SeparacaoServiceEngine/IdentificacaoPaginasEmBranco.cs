namespace Veros.Paperless.Model.Servicos.SeparacaoServiceEngine
{
    using System;
    using System.Collections.Generic;
    using Entidades;
    using Framework;
    using Framework.IO;

    public class IdentificacaoPaginasEmBranco : IIdentificacaoPaginasEmBranco
    {
        private readonly IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico;
        private readonly IManipulaImagemServico manipulaImagemServico;
        private readonly IFileSystem fileSystem;

        public IdentificacaoPaginasEmBranco(
            IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico,
            IManipulaImagemServico manipulaImagemServico, 
            IFileSystem fileSystem)
        {
            this.baixaArquivoFileTransferServico = baixaArquivoFileTransferServico;
            this.manipulaImagemServico = manipulaImagemServico;
            this.fileSystem = fileSystem;
        }

        public IList<Pagina> Executar(Documento documento, IList<Pagina> paginas)
        {
            var deleteSchedulle = new List<string>();

            foreach (var pagina in paginas)
            {
                Log.Application.DebugFormat("Processando pagina #{0}", pagina.Id);

                var arquivo = this.baixaArquivoFileTransferServico
                    .BaixarArquivo(pagina.Id, pagina.TipoArquivo);
                
                deleteSchedulle.Add(arquivo);

                try
                {
                    if (this.manipulaImagemServico.ImagemEmBranco(
                        arquivo,
                        Contexto.MinWidthPixel,
                        Contexto.MinHeightPixel,
                        Contexto.MinMargemPixel))
                    {
                        pagina.EmBranco = true;
                    }
                }
                catch (Exception exception)
                {
                    Log.Application.Error(exception);
                    throw;
                }
            }

            this.ExcluirImagensTemporarias(deleteSchedulle);

            return paginas;
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