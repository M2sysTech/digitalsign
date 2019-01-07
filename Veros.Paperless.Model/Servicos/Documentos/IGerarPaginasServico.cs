namespace Veros.Paperless.Model.Servicos.Documentos
{
    using System.Collections.Generic;
    using Entidades;

    public interface IGerarPaginasServico
    {
        void Executar(Documento documento, string caminhoImagens, bool apagarArquivosLocais = true);

        IEnumerable<Pagina> AdicionarPaginas(Documento documento, string caminhoImagens, string nomeArquivos, int ordem, bool apagarArquivosLocais = true);

        void InserirArquivoUnico(Documento documentoNovo, string caminhoArquivo, bool cloudOk);
    }
}
