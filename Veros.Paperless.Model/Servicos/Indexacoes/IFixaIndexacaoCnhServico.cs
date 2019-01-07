namespace Veros.Paperless.Model.Servicos.Indexacoes
{
    using Veros.Paperless.Model.Entidades;

    public interface IFixaIndexacaoCnhServico
    {
        void Executar(Documento documento);
    }
}