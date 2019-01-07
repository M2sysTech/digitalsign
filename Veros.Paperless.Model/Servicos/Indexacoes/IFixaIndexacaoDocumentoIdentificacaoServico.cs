namespace Veros.Paperless.Model.Servicos.Indexacoes
{
    using Entidades;

    public interface IFixaIndexacaoDocumentoIdentificacaoServico
    {
        void Executar(Documento documento);
    }
}