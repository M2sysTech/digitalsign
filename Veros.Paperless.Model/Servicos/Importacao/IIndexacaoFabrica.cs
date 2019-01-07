namespace Veros.Paperless.Model.Servicos.Importacao
{
    using Veros.Paperless.Model.Entidades;

    public interface IIndexacaoFabrica
    {
        Indexacao Criar(Documento documento,
            int campoId,
            string valor);

        Indexacao Criar(Documento documento,
            string campoRefArquivo,
            string valor);
    }
}
