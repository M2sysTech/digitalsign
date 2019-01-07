namespace Veros.Paperless.Model.Servicos.SeparacaoServiceEngine
{
    using System.Collections.Generic;
    using Entidades;

    public interface ITesseractServico
    {
        void SetarCacheLocalImagens(string path);

        void SetarDiretorioTessdata(string path);

        void CorrigirOrientacao(Pagina pagina);

        string ReconhecerPalavrasTopo(string arquivo, int porcentagemTopo);
    }
}
