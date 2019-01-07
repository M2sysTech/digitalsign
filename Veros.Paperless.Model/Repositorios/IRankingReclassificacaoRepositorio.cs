namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Framework.Modelo;
    using Veros.Paperless.Model.Entidades;

    public interface IRankingReclassificacaoRepositorio : IRepositorio<RankingReclassificacao>
    {
        RankingReclassificacao ObterPorTipoDeDocumento(int tipoDeDocumentoId);
    }
}