namespace Veros.Paperless.Model.Servicos.SeparacaoServiceEngine
{
    using System.Collections.Generic;
    using Entidades;

    public interface IIdentificaTipoDocumentoServico
    {
        void Executar(List<List<Pagina>> todasAsPaginas, string cacheLocalImagens);

        TipoDocumento CruzarPalarasComReconhecimento(Pagina pagina, List<PalavraTipo> listaPalavras);

        string LimparTextoBasico(string palavrasRaw);
    }
}