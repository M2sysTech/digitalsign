namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IFaceRepositorio : IRepositorio<Face>
    {
        IList<int> ObterIdsParaCompararExceto(int faceId);

        Face ObterComPagina(int faceId);

        void ComparacaoFaceFinalizada(int faceId, bool faceComum);

        IList<Face> ObterFacesPorPaginaId(int paginaId);

        string ObterNomeArquivoPorId(int faceId);
    }
}
