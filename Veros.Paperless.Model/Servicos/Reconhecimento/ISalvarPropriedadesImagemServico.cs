namespace Veros.Paperless.Model.Servicos.Reconhecimento
{
    using Entidades;

    public interface ISalvarPropriedadesImagemServico
    {
        void Executar(string imagem, Pagina pagina);
    }
}