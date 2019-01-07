namespace Veros.Paperless.Model.Servicos.Identificacao
{
    using Entidades;
    using Repositorios;

    public class RankingReclassificacaoService : IRankingReclassificacaoService
    {
        private readonly ITipoDocumentoRepositorio tipoDocumentoRepositorio;
        private readonly IRankingReclassificacaoRepositorio rankingReclassificacaoRepositorio;

        public RankingReclassificacaoService(ITipoDocumentoRepositorio tipoDocumentoRepositorio, 
            IRankingReclassificacaoRepositorio rankingReclassificacaoRepositorio)
        {
            this.tipoDocumentoRepositorio = tipoDocumentoRepositorio;
            this.rankingReclassificacaoRepositorio = rankingReclassificacaoRepositorio;
        }

        public RankingReclassificacao IncrementarRankDeReclassificacao(TipoDocumento tipoDocumentoNovo, Documento documento)
        {
            RankingReclassificacao ranking = null;

            if (documento.TipoDocumento.TypeDocCode != tipoDocumentoNovo.TypeDocCode)
            {
                ranking = this.rankingReclassificacaoRepositorio.ObterPorTipoDeDocumento(tipoDocumentoNovo.TypeDocCode);

                if (ranking == null)
                {
                    ranking = new RankingReclassificacao()
                    {
                        TipoDocumento = this.tipoDocumentoRepositorio.ObterPorTypeDoc(tipoDocumentoNovo.TypeDocCode)
                    };
                }

                ranking.IncrementarRank(1);

                this.rankingReclassificacaoRepositorio.Salvar(ranking);
            }

            return ranking;
        }
    }
}
