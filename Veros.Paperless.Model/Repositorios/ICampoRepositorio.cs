namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface ICampoRepositorio : IRepositorio<Campo>
    {
        Campo ObterPorNome(string name);

        IList<Campo> ObterPorTipoDocumento(TipoDocumento tipoDocumento);

        IList<Campo> ObterMarcadosParaValidacaoPorCodigoTipoDocumento(int codigoTipoDocumento);

        Campo ObterPorReferenciaDeArquivo(TipoDocumento tipoDocumento,
            string referenciaArquivo);

        IList<Campo> ObterCamposComGrupo();

        IList<Campo> ObterPorCodigoTipoDocumento(int id);

        IList<Campo> ObterPorTipoDocumentoComMapeamentoOcr(int tipoDocumentoId);
        
        IList<Campo> ObterTodosReconheciveis();
    }
}
