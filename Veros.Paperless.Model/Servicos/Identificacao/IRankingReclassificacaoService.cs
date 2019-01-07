namespace Veros.Paperless.Model.Servicos.Identificacao
{
    using Entidades;

    public interface IRankingReclassificacaoService
    {
        RankingReclassificacao IncrementarRankDeReclassificacao(TipoDocumento tipoDocumentoNovo, Documento documento);
    }
}
