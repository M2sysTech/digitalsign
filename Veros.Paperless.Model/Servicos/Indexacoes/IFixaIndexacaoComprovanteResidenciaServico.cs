namespace Veros.Paperless.Model.Servicos.Indexacoes
{
    using Veros.Paperless.Model.Entidades;

    public interface IFixaIndexacaoComprovanteResidenciaServico
    {
        void Executar(Documento comprovanteResidencia);
    }
}