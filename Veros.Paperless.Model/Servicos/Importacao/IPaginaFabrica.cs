namespace Veros.Paperless.Model.Servicos.Importacao
{
    using Veros.Paperless.Model.Entidades;

    public interface IPaginaFabrica
    {
        Pagina Criar(Documento documento, int ordem, string caminhoArquivo, string versao = "0");
    }
}
