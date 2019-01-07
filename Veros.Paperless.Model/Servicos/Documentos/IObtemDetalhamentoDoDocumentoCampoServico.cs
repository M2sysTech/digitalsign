namespace Veros.Paperless.Model.Servicos.Documentos
{
    using Entidades;

    public interface IObtemDetalhamentoDoDocumentoCampoServico
    {
        DetalhamentoDoDocumentoCampo Obter(Indexacao indexacao);
    }
}