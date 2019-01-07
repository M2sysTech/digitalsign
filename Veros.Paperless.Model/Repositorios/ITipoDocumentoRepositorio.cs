namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface ITipoDocumentoRepositorio : IRepositorio<TipoDocumento>
    {
        TipoDocumento ObterPorCodigo(int codigo);

        IList<TipoDocumento> ObterDocumentosNaoMestres();

        IList<TipoDocumento> ObterDocumentosMestres();

        IList<TipoDocumento> ObterParaIdentificacao();

        TipoDocumento ObterPorTypeDoc(int tipoDocumento);

        int ObterUltimoTypedocCode();

        void ExcluirPorId(int id);

        IList<TipoDocumento> ObterPorDescricao(string removeAcentuacao);

        IList<TipoDocumento> ObterGruposDosItensDocumentais();

        IList<TipoDocumento> ObterPorRangeTypedoc(int inicial, int final);
    }
}
