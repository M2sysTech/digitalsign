namespace Veros.Paperless.Model.Servicos.Indexacoes
{
    using Veros.Paperless.Model.Entidades;

    public interface IFixaIndexacaoDocumentoServico
    {
        void Execute(Documento documento); 
    }
}