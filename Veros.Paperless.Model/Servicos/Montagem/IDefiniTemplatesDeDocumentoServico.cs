namespace Veros.Paperless.Model.Servicos.Montagem
{
    using Entidades;

    public interface IDefiniTemplatesDeDocumentoServico
    {
        void Executar(Documento documento);
    }
}