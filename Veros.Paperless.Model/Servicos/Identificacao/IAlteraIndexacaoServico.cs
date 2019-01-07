namespace Veros.Paperless.Model.Servicos.Identificacao
{
    using Entidades;

    public interface IAlteraIndexacaoServico
    {
        void Alterar(Documento documento, TipoDocumento tipoDocumentoNovo);
    }
}