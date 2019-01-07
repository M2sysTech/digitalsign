namespace Veros.Paperless.Model.Servicos.Reconhecimento
{
    using Entidades;
    using Image.Reconhecimento;

    public interface IPostaImagemReconhecidaServico
    {
        void Executar(int paginaId, Imagem imagem, string imagemProcessada, ResultadoReconhecimento resultadoReconhecimento);

        void SubstituirImagemMantendoOriginal(int paginaId, string caminhoImgOriginal, string imagemProcessada);
    }
}