namespace Veros.Paperless.Model.Servicos.Importacao
{
    using System;
    using System.IO;
    using Veros.Framework;
    using Veros.Framework.IO;
    using Veros.Paperless.Model.Entidades;

    public class PaginaFabrica : IPaginaFabrica
    {
        private readonly IFileSystem fileSystem;

        public PaginaFabrica(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public Pagina Criar(Documento documento, int ordem, string caminhoArquivo, string versao = "0")
        {
            Log.Application.DebugFormat("Criando pagina para imagem [{0}]", caminhoArquivo);
            
            return new Pagina
            {
                AgenciaRemetente = documento.Lote.Agencia,
                ImagemVersoEmBranco = "S",
                ImagemFrenteEmBranco = "N",
                DataCriacao = DateTime.Now,
                TamanhoImagemFrente = (int)this.fileSystem.GetFileSize(caminhoArquivo),
                TipoArquivo = Path.GetExtension(caminhoArquivo).ToUpper().Replace(".", string.Empty),
                NomeArquivoSemExtensao = Path.GetFileNameWithoutExtension(caminhoArquivo).ToUpper(),
                ImagemFront = "S",
                Info = "1",
                Ordem = ordem,
                Status = PaginaStatus.StatusTransmissaoOk,
                Lote = documento.Lote,
                Documento = documento,
                CaminhoCompletoDoArquivo = caminhoArquivo,
                Versao = versao
            };
        }
    }
}
