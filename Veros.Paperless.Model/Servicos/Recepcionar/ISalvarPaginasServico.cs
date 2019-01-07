namespace Veros.Paperless.Model.Servicos.Recepcionar
{
    using Entidades;

    public interface ISalvarPaginasServico
    {
        /// <summary>
        /// Salva varias paginas
        /// </summary>
        /// <param name="imagens">caminho de cada imagem que se tornará uma pagina</param>
        /// <param name="documento">documento para inserção das paginas</param>
        void Executar(string[] imagens, Documento documento);

        /// <summary>
        /// Salva uma imagem
        /// </summary>
        /// <param name="imagem">Caminho da imagem</param>
        /// <param name="documento">documento para inserção da pagina</param>
        void Executar(string imagem, Documento documento, int contador = 1);
    }
}