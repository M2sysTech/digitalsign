namespace Veros.Paperless.Model.Servicos.Campos
{
    using Documentos;

    public interface IObtemDetalhamentoDoCampoServico
    {
        DetalhamentoDoDocumentoCampo Obter(int indexacaoId);
    }
}