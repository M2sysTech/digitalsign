namespace Veros.Paperless.Model.Servicos.Directory.FluxoNormal
{
    using Entidades;

    public interface IRealizaGiroServico
    {
        void Executar(Documento documento, string caminhoDestinoTemporario);
    }
}
